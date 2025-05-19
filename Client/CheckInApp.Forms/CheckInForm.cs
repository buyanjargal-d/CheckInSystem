using System.Text.Json;
using System.Net.Http;
using System.Windows.Forms;
using System.Text;
using CheckInSystem.DTO; // Import DTO namespace
using CheckInApp;

namespace CheckInApp.Forms;

public partial class CheckInForm : Form
{
    private readonly HttpClient client = new();
    private const string baseUrl = "http://localhost:5052";

    private readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public CheckInForm() => InitializeComponent();

    private async void CheckInForm_Load(object sender, EventArgs e)
    {
        await LoadAllPassengers();
    }

    private async void btnSearchPassenger_Click(object sender, EventArgs e)
    {
        var passportId = txtPassportId.Text.Trim();
        if (string.IsNullOrWhiteSpace(passportId))
        {
            lblStatusMessage.Text = "❗ Пасспортын дугаар оруулна уу";
            return;
        }

        try
        {
            var json = await client.GetStringAsync($"{baseUrl}/api/passengers/{passportId}");
            var passenger = JsonSerializer.Deserialize<PassengerDto>(json, jsonOptions);

            if (passenger == null)
            {
                lblStatusMessage.Text = "❌ Зорчигч олдсонгүй";
                return;
            }

            lblPassengerFullName.Text = passenger.FullName;
            lblStatusMessage.Text = "✅ Зорчигч олдлоо";

            await LoadFlights(passenger.FlightId);
            await LoadSeats(passenger.FlightId);
            await LoadSeatLayout(passenger.FlightId);
            await LoadFlightStatus(passenger.FlightId);
        }
        catch (Exception ex)
        {
            lblStatusMessage.Text = $"❌ Алдаа гарлаа: {ex.Message}";
        }
    }

    private async Task LoadAllPassengers()
    {
        var json = await client.GetStringAsync($"{baseUrl}/api/passengers");
        var passengers = JsonSerializer.Deserialize<List<PassengerDto>>(json, jsonOptions);
        dgvPassengers.AutoGenerateColumns = true;
        dgvPassengers.DataSource = passengers;
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

    private async Task LoadFlights(int selectedFlightId)
    {
        var json = await client.GetStringAsync($"{baseUrl}/api/flights");
        var flights = JsonSerializer.Deserialize<List<FlightDto>>(json, jsonOptions);

        if (flights == null)
        {
            lblStatusMessage.Text = "❌ Нислэгүүд олдсонгүй";
            return;
        }

        comboFlightCode.SelectedIndexChanged -= comboFlightCode_SelectedIndexChanged;
        comboFlightCode.DataSource = flights;
        comboFlightCode.DisplayMember = "FlightNumber";
        comboFlightCode.ValueMember = "Id";
        comboFlightCode.SelectedValue = selectedFlightId;
        comboFlightCode.SelectedIndexChanged += comboFlightCode_SelectedIndexChanged;
    }

    private async void comboFlightCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboFlightCode.SelectedValue is int flightId)
        {
            await LoadSeats(flightId);
            await LoadSeatLayout(flightId);
            await LoadFlightStatus(flightId);
        }
    }

    private async Task LoadSeats(int flightId)
    {
        var json = await client.GetStringAsync($"{baseUrl}/api/seats/available?flightId={flightId}");
        var seats = JsonSerializer.Deserialize<List<SeatDto>>(json, jsonOptions);

        comboSeatSelection.DataSource = seats;
        comboSeatSelection.DisplayMember = "SeatNumber";
        comboSeatSelection.ValueMember = "SeatNumber";
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
                comboSeatSelection.SelectedValue = seat.SeatNumber;

                // Бүх товчны BorderStyle-г цэвэрлэх
                foreach (Control ctrl in flwLayoutPanelSeat.Controls)
                {
                    if (ctrl is SeatUserControl sc)
                        sc.BorderStyle = BorderStyle.None;
                }

                // Дарсан товчны BorderStyle-г идэвхтэй болгох
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
        var request = new AssignSeatRequestDto
        {
            PassportNumber = txtPassportId.Text.Trim(),
            FlightId = comboFlightCode.SelectedValue is int fid ? fid : 0,
            SeatNumber = comboSeatSelection.SelectedValue?.ToString() ?? ""
        };

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{baseUrl}/api/seats/assign", content);

        lblStatusMessage.Text = response.IsSuccessStatusCode
            ? "✅ Суудал амжилттай оноогдлоо!"
            : "❌ Суудал оноох үед алдаа гарлаа!";

        if (response.IsSuccessStatusCode)
            await LoadSeatLayout(request.FlightId);
    }

    private void comboSeatSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (Control ctrl in flwLayoutPanelSeat.Controls)
        {
            if (ctrl is SeatUserControl seatCtrl)
            {
                seatCtrl.BorderStyle = seatCtrl.SeatNumber == comboSeatSelection.SelectedValue?.ToString()
                    ? BorderStyle.Fixed3D
                    : BorderStyle.None;
            }
        }
    }


    private async void btnPrintBoardingPass_Click(object sender, EventArgs e)
    {
        var passportId = txtPassportId.Text.Trim();
        try
        {
            var json = await client.GetStringAsync($"{baseUrl}/api/passengers/boarding-pass/{passportId}");
            MessageBox.Show(json, "🛫 Boarding Pass", MessageBoxButtons.OK);
            lblStatusMessage.Text = "🖨 Boarding pass амжилттай гарлаа!";
        }
        catch (Exception ex)
        {
            lblStatusMessage.Text = $"❌ Boarding pass хэвлэхэд алдаа: {ex.Message}";
        }
    }
}
