using CheckInSystem.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckInApp.Forms
{
    /// <summary>
    /// StatusForm нь тухайн нислэгийн төлөвийг харуулах, өөрчлөх зориулалттай Form юм.
    /// Админ эсвэл систем хэрэглэгч энэ Form-оор дамжуулан нислэгийн status-ийг шинэчилж болно.
    /// </summary>
    public partial class StatusForm : Form
    {
        private readonly HttpClient _client = new();
        private const string baseUrl = "http://localhost:5052";
        private int selectedFlightId = 0;

        /// <summary>
        /// StatusForm-ын үндсэн конструктор
        /// </summary>
        /// <param name="flightId">Хэрэв шууд нэг нислэгийг сонгоод нээж буй бол Id-г дамжуулна</param>
        public StatusForm(int flightId = 0)
        {
            InitializeComponent();
            selectedFlightId = flightId;
        }

        /// <summary>
        /// Form ачаалагдах үед төлвийн жагсаалтыг бэлтгэх
        /// </summary>
        private void StatusForm_Load(object sender, EventArgs e)
        {
            cmbStatus.Items.AddRange(new[] { "CheckingIn", "Boarding", "Departed", "Delayed", "Cancelled" });

            if (selectedFlightId > 0)
                _ = LoadFlights(""); // бүх нислэгийг ачаалах
        }

        /// <summary>
        /// Хайлтын товч дарсан үед нислэгүүдийг хайх
        /// </summary>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadFlights(txtFlightNumber.Text.Trim());
        }

        /// <summary>
        /// Enter дарсан үед хайлтыг хийх
        /// </summary>
        private async void txtFlightNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                await LoadFlights(txtFlightNumber.Text.Trim());
        }

        /// <summary>
        /// Нислэгүүдийг серверээс татаж DataGridView-д харуулах
        /// </summary>
        private async Task LoadFlights(string flightNumber)
        {
            string url = string.IsNullOrWhiteSpace(flightNumber)
     ? $"{baseUrl}/api/flights"
     : $"{baseUrl}/api/flights/search?number={flightNumber}";

            try
            {
                var json = await _client.GetStringAsync(url);
                var flights = JsonSerializer.Deserialize<List<FlightDto>>(json, JsonSettings.Options);

                dgvFlights.DataSource = flights;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Алдаа: {ex.Message}");
            }
        }

        /// <summary>
        /// Нислэг дээр дарсан үед Form баруун талд харуулах
        /// </summary>
        private void dgvFlights_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvFlights.Rows[e.RowIndex].DataBoundItem as FlightDto;
                if (row != null)
                {
                    selectedFlightId = row.Id;
                    lblFlightNumber.Text = row.FlightNumber;
                    cmbStatus.SelectedItem = row.Status;
                }
            }
        }

        /// <summary>
        /// Төлвийг шинэчлэх товч дарсан үед сервер лүү илгээж шинэчилнэ
        /// </summary>
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (selectedFlightId == 0 || cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Нислэг болон төлвийг сонгоно уу.");
                return;
            }

            var update = new FlightStatusUpdateDto
            {
                FlightId = selectedFlightId,
                NewStatus = cmbStatus.SelectedItem.ToString()
            };

            var json = JsonSerializer.Serialize(update);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{baseUrl}/api/flights/status", content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("🛫 Төлөв амжилттай шинэчлэгдлээ!");
                await LoadFlights(txtFlightNumber.Text.Trim()); // update grid
            }
            else
            {
                MessageBox.Show("❌ Төлөв шинэчлэх үед алдаа гарлаа!");
            }
        }
    }
}
