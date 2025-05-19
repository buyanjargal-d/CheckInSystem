using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckInApp.Forms
{
    public partial class SeatUserControl : UserControl
    {
        public SeatUserControl()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string SeatNumber
        {
            get => btnSeat.Text;
            set => btnSeat.Text = value;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsOccupied
        {
            get => btnSeat.BackColor == Color.SkyBlue;
            set => btnSeat.BackColor = value ? Color.SkyBlue : Color.LightGray;
        }
    }
}
