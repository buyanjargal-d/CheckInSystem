using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using CheckInSystem.DTO;

namespace CheckInApp.Forms
{
    public partial class CheckInForm : Form
    {
        private readonly HttpClient client = new();
        private const string baseUrl = "http://localhost:5052";

        private readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedFlightId { get; internal set; }

        public CheckInForm() => InitializeComponent();

        private async void CheckInForm_Load(object sender, EventArgs e)
        {
            if (SelectedFlightId > 0)
            {
                await LoadPassengersByFlight(SelectedFlightId);
                await LoadFlightStatus(SelectedFlightId);
                await LoadSeats(SelectedFlightId);
                await LoadSeatLayout(SelectedFlightId);
                await LoadFlightCode(SelectedFlightId);
            }
            else
            {
                await LoadAllPassengers();
            }
        }

        private async void btnSearchPassenger_Click(object sender, EventArgs e)
        {
            var passportId = txtPassportId.Text.Trim();
            if (string.IsNullOrWhiteSpace(passportId))
            {
                lblStatusMessage.Text = "Пасспортын дугаар оруулна уу";
                return;
            }

            try
            {
                var json = await client.GetStringAsync($"{baseUrl}/api/passengers/{passportId}");
                var passenger = JsonSerializer.Deserialize<PassengerDto>(json, jsonOptions);

                if (passenger == null)
                {
                    lblStatusMessage.Text = "Зорчигч олдсонгүй";
                    return;
                }

                lblPassengerFullName.Text = passenger.FullName;
                lblStatusMessage.Text = "Зорчигч олдлоо";

                SelectedFlightId = passenger.FlightId;
                await LoadFlightCode(passenger.FlightId);
                await LoadSeats(passenger.FlightId);
                await LoadSeatLayout(passenger.FlightId);
                await LoadFlightStatus(passenger.FlightId);
            }
            catch (Exception ex)
            {
                lblStatusMessage.Text = $"Алдаа гарлаа: {ex.Message}";
            }
        }

        private async Task LoadAllPassengers()
        {
            var json = await client.GetStringAsync($"{baseUrl}/api/passengers");
            var passengers = JsonSerializer.Deserialize<List<PassengerDto>>(json, jsonOptions);
            dgvPassengers.AutoGenerateColumns = true;
            dgvPassengers.DataSource = passengers;
        }

        private async Task LoadPassengersByFlight(int flightId)
        {
            try
            {
                var response = await client.GetAsync($"{baseUrl}/api/passengers/by-flight/{flightId}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var passengers = JsonSerializer.Deserialize<List<PassengerDto>>(json, jsonOptions);
                dgvPassengers.DataSource = passengers;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Алдаа: {ex.Message}", "Татах үед алдаа гарлаа");
            }
        }

        private void dgvPassengers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvPassengers.Rows[e.RowIndex];
                txtPassportId.Text = row.Cells["PassportNumber"].Value?.ToString();
                btnSearchPassenger.PerformClick();
            }
        }

        private async Task LoadFlightCode(int flightId)
        {
            try
            {
                var json = await client.GetStringAsync($"{baseUrl}/api/flights/{flightId}");
                var flight = JsonSerializer.Deserialize<FlightDto>(json, jsonOptions);
                lblFlightCode.Text = flight?.FlightNumber ?? "N/A";
            }
            catch
            {
                lblFlightCode.Text = "N/A";
            }
        }

        private async Task LoadSeats(int flightId)
        {
            var json = await client.GetStringAsync($"{baseUrl}/api/seats/available?flightId={flightId}");
            var seats = JsonSerializer.Deserialize<List<SeatDto>>(json, jsonOptions);

            comboSeatSelection.DataSource = seats;
            comboSeatSelection.DisplayMember = "SeatNumber";
            comboSeatSelection.ValueMember = "SeatId";
        }

        private async Task LoadSeatLayout(int flightId)
        {
            flwLayoutPanelSeat.Controls.Clear();
            var json = await client.GetStringAsync($"{baseUrl}/api/seats/all?flightId={flightId}");
            var seats = JsonSerializer.Deserialize<List<SeatDto>>(json, jsonOptions);

            foreach (var seat in seats ?? new())
            {
                var seatControl = new SeatUserControl
                {
                    SeatNumber = seat.SeatNumber,
                    IsOccupied = seat.IsOccupied,
                    Margin = new Padding(3),
                    Width = 50,
                    Height = 50
                };

                seatControl.Click += (s, e) =>
                {
                    comboSeatSelection.SelectedValue = seat.SeatId;
                    foreach (Control ctrl in flwLayoutPanelSeat.Controls)
                        if (ctrl is SeatUserControl sc) sc.BorderStyle = BorderStyle.None;
                    seatControl.BorderStyle = BorderStyle.Fixed3D;
                };

                flwLayoutPanelSeat.Controls.Add(seatControl);
            }
        }

        private async Task LoadFlightStatus(int flightId)
        {
            var json = await client.GetStringAsync($"{baseUrl}/api/flights/{flightId}");
            var flight = JsonSerializer.Deserialize<FlightDto>(json, jsonOptions);
            lblFlightStatus.Text = GetFlightStatusText(flight?.Status ?? "");
        }

        private string GetFlightStatusText(string status) => status.ToLower() switch
        {
            "checkingin" => "Бүртгэж байна",
            "boarding" => "Онгоцонд сууж байна",
            "departed" => "Ниссэн",
            "delayed" => "Хойшилсон",
            "cancelled" => "Цуцалсан",
            _ => "Тодорхойгүй"
        };

        private async void btnAssignSeat_Click(object sender, EventArgs e)
        {
            var passportId = txtPassportId.Text.Trim();
            if (string.IsNullOrWhiteSpace(passportId) || comboSeatSelection.SelectedValue == null)
            {
                lblStatusMessage.Text = "Пасспорт эсвэл суудал сонгоно уу";
                return;
            }

            var jsonP = await client.GetStringAsync($"{baseUrl}/api/passengers/{passportId}");
            var passenger = JsonSerializer.Deserialize<PassengerDto>(jsonP, jsonOptions);
            if (passenger?.Id == 0)
            {
                lblStatusMessage.Text = "Зорчигч олдсонгүй";
                return;
            }

            var seatId = (int)comboSeatSelection.SelectedValue;
            var seatNumber = ((SeatDto)comboSeatSelection.SelectedItem).SeatNumber;

            var request = new AssignSeatRequestDto
            {
                PassportNumber = passportId,
                FlightId = SelectedFlightId,
                PassengerId = passenger.Id,
                SeatId = seatId,
                SeatNumber = seatNumber
            };

            var payload = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{baseUrl}/api/seats/assign", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var apiResult = JsonSerializer.Deserialize<ApiResponseDto>(jsonResponse, jsonOptions);

            switch (apiResult?.Status)
            {
                case "seat_taken":
                    lblStatusMessage.Text = "Энэ суудал аль хэдийнээ эзэлсэн байна. Өөр суудал сонгоно уу!";
                    break;
                case "assigned":
                    lblStatusMessage.Text = "Суудал амжилттай оноогдлоо!";
                    await LoadSeats(SelectedFlightId);
                    await LoadSeatLayout(SelectedFlightId);
                    try
                    {
                        var bpJson = await client.GetStringAsync(
                            $"{baseUrl}/api/passengers/boarding-pass/{Uri.EscapeDataString(passportId)}");
                        var bp = JsonSerializer.Deserialize<BoardingPassDto>(bpJson, jsonOptions);

                        var doc = new PrintDocument();
                        doc.DefaultPageSettings.PaperSize = new PaperSize("BoardingPass", 413, 583);
                        doc.DefaultPageSettings.Margins = new Margins(30, 30, 30, 30);
                        doc.PrintPage += (s, ev) =>
                        {
                            var g = ev.Graphics;
                            var r = ev.MarginBounds;

                            using var pen = new Pen(Color.Black, 2);
                            g.DrawRectangle(pen, r);

                            var header = new Rectangle(r.X, r.Y, r.Width, 60);
                            using var hdrBrush = new SolidBrush(Color.LightGray);
                            g.FillRectangle(hdrBrush, header);
                            using var hdrFont = new Font("Arial", 18, FontStyle.Bold);
                            var title = "BOARDING PASS";
                            var titleWidth = g.MeasureString(title, hdrFont).Width;
                            g.DrawString(
                                title,
                                hdrFont,
                                Brushes.Black,
                                r.X + (r.Width - titleWidth) / 2,
                                r.Y + 15);

                            g.DrawLine(Pens.Black, r.X, r.Y + 60, r.Right, r.Y + 60);

                            using var lblFont = new Font("Arial", 12, FontStyle.Bold);
                            using var valFont = new Font("Arial", 12);
                            float x = r.X + 10;
                            float y = r.Y + 80;
                            float lineHeight = valFont.GetHeight(g) + 6;

                            void DrawRow(string label, string value)
                            {
                                g.DrawString(label, lblFont, Brushes.Black, x, y);
                                g.DrawString(value, valFont, Brushes.Black, x + 120, y);
                                y += lineHeight;
                            }

                            DrawRow("Passenger:", bp.FullName);
                            DrawRow("Passport No:", bp.PassportNumber);
                            DrawRow("Flight No:", bp.FlightNumber);
                            DrawRow("Status:", bp.FlightStatus);
                            DrawRow("Seat:", bp.SeatNumber);
                            DrawRow("Departure:", bp.DepartureTime);

                            var pixelWriter = new BarcodeWriterPixelData
                            {
                                Format = BarcodeFormat.CODE_128,
                                Options = new EncodingOptions
                                {
                                    Width = r.Width - 20,
                                    Height = 60,
                                    Margin = 2
                                }
                            };
                            var pixelData = pixelWriter.Write(bp.PassportNumber);
                            using var bmp = new Bitmap(
                                pixelData.Width,
                                pixelData.Height,
                                System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                            var bmpData = bmp.LockBits(
                                new Rectangle(0, 0, bmp.Width, bmp.Height),
                                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                                System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                            System.Runtime.InteropServices.Marshal.Copy(
                                pixelData.Pixels, 0, bmpData.Scan0, pixelData.Pixels.Length);
                            bmp.UnlockBits(bmpData);
                            g.DrawImage(bmp, r.X + 10, r.Bottom - 80);
                        };

                        using var preview = new PrintPreviewDialog
                        {
                            Document = doc,
                            Width = 800,
                            Height = 600
                        };
                        preview.ShowDialog();
                    }
                    catch (HttpRequestException ex) when (ex.Message.Contains("404"))
                    {
                        MessageBox.Show(
                            "Боард пас олдсонгүй.",
                            "Анхааруулга",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    break;
                case "passenger_already_assigned":
                    lblStatusMessage.Text = apiResult.Message ?? "Зорчигчийн суудал аль хэдийнээ оноогдсон байна";
                    break;
                default:
                    lblStatusMessage.Text = $"Алдаа гарлаа: {jsonResponse}";
                    break;
            }
        }

        private void comboSeatSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control ctrl in flwLayoutPanelSeat.Controls)
            {
                if (ctrl is SeatUserControl sc)
                {
                    sc.BorderStyle = sc.SeatNumber == (comboSeatSelection.SelectedItem as SeatDto)?.SeatNumber
                        ? BorderStyle.Fixed3D
                        : BorderStyle.None;
                }
            }
        }
    }
}