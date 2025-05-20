using CheckInSystem.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckInApp.Forms
{
    public partial class FlightsForm : Form
    {
        private readonly string userRole;

        public FlightsForm(string role)
        {
            InitializeComponent();
            userRole = role;
        }

        public FlightsForm()
        {
        }

        private readonly HttpClient client = new();
        private const string baseUrl = "http://localhost:5052";
        private List<FlightDto> allFlights = new();

        private async void FlightsForm_Load(object sender, EventArgs e)
        {
            await LoadAllFlightsAsync();
        }

        private async Task LoadAllFlightsAsync()
        {
            try
            {
                var json = await client.GetStringAsync($"{baseUrl}/api/flights");

                var flights = JsonSerializer.Deserialize<List<FlightDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = false
                });

                allFlights = flights ?? new();
                DisplayFlights(allFlights);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нислэг ачаалахад алдаа гарлаа: " + ex.Message);
            }
        }

        private void DisplayFlights(IEnumerable<FlightDto> flights)
        {
            flwLayoutPanelFlights.Controls.Clear();

            foreach (var flight in flights)
            {
                var flightControl = new FlightUserControl();
                flightControl.SetFlightInfo(flight, userRole); // role дамжуулна
                flightControl.Margin = new Padding(10);
                flightControl.Width = (flwLayoutPanelFlights.Width / 2) - 30;
                flwLayoutPanelFlights.Controls.Add(flightControl);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FilterFlights();
        }

        private void txtFlightNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FilterFlights();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void FilterFlights()
        {
            var keyword = txtFlightNumber.Text.Trim().ToLower();
            var filtered = allFlights
                .Where(f => f.FlightNumber.ToLower().Contains(keyword))
                .ToList();

            DisplayFlights(filtered);
        }
    }
}
