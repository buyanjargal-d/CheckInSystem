using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CheckInSystem.DTO;

namespace CheckInApp.Forms
{
    public partial class FlightUserControl : UserControl
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FlightId { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UserRole { get; set; } = string.Empty;

        public FlightUserControl()
        {
            InitializeComponent();

            // Энэ UserControl дээр дарахад CheckInForm руу орно
            this.Click += FlightUserControl_Click;

            // Доторх бүх control-ууд дээр click хийсэн ч мөн адил
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Click += (s, e) => FlightUserControl_Click(s, e);
            }

            btnStatus.Click += BtnStatus_Click;
        }

        private void BtnStatus_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Төлөв: {lblStatus.Text}\nFlight ID: {FlightId}", "Flight Status");
        }

        private void FlightUserControl_Click(object sender, EventArgs e)
        {
            CheckInForm checkInForm = new CheckInForm
            {
                SelectedFlightId = this.FlightId
            };
            checkInForm.Show();
        }

        public void SetFlightInfo(FlightDto flight, string role)
        {
            UserRole = role;

            // "System" биш бол Status товчийг нууна
            btnStatus.Visible = (UserRole == "System");

            lblFlightNumber.Text = !string.IsNullOrWhiteSpace(flight.FlightNumber)
                ? flight.FlightNumber
                : "Нислэг тодорхойгүй";

            lblDepartureTime.Text = flight.DepartureTime != default
                ? flight.DepartureTime.ToString("yyyy-MM-dd HH:mm")
                : "0000-00-00 00:00";

            lblStatus.Text = !string.IsNullOrWhiteSpace(flight.Status)
                ? flight.Status
                : "Төлөв тодорхойгүй";

            FlightId = flight.Id;
        }
    }
}
