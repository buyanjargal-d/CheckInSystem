namespace CheckInApp.Forms
{
    partial class FlightUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlightUserControl));
            lblFlightNumber = new Label();
            lblDepartureTime = new Label();
            lblStatus = new Label();
            pictureBox1 = new PictureBox();
            btnStatus = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblFlightNumber
            // 
            lblFlightNumber.AutoSize = true;
            lblFlightNumber.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFlightNumber.Location = new Point(83, 21);
            lblFlightNumber.Name = "lblFlightNumber";
            lblFlightNumber.Size = new Size(121, 23);
            lblFlightNumber.TabIndex = 0;
            lblFlightNumber.Text = "Flight Number";
            // 
            // lblDepartureTime
            // 
            lblDepartureTime.AutoSize = true;
            lblDepartureTime.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblDepartureTime.Location = new Point(83, 54);
            lblDepartureTime.Name = "lblDepartureTime";
            lblDepartureTime.Size = new Size(105, 20);
            lblDepartureTime.TabIndex = 1;
            lblDepartureTime.Text = "DepartureTime";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(83, 74);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(49, 20);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Status";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(6, 16);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(71, 75);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // btnStatus
            // 
            btnStatus.BackColor = Color.LightSkyBlue;
            btnStatus.Location = new Point(296, 19);
            btnStatus.Name = "btnStatus";
            btnStatus.Size = new Size(83, 29);
            btnStatus.TabIndex = 4;
            btnStatus.Text = "Status";
            btnStatus.UseVisualStyleBackColor = false;
            // 
            // FlightUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnStatus);
            Controls.Add(pictureBox1);
            Controls.Add(lblStatus);
            Controls.Add(lblDepartureTime);
            Controls.Add(lblFlightNumber);
            Name = "FlightUserControl";
            Size = new Size(385, 107);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFlightNumber;
        private Label lblDepartureTime;
        private Label lblStatus;
        private PictureBox pictureBox1;
        private Button btnStatus;
    }
}
