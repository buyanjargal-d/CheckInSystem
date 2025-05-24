namespace CheckInApp.Forms
{
    partial class FlightsForm
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


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlightsForm));
            txtFlightNumber = new TextBox();
            btnSearch = new Button();
            pictureBox1 = new PictureBox();
            flwLayoutPanelFlights = new FlowLayoutPanel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtFlightNumber
            // 
            txtFlightNumber.Location = new Point(238, 26);
            txtFlightNumber.Name = "txtFlightNumber";
            txtFlightNumber.Size = new Size(496, 27);
            txtFlightNumber.TabIndex = 0;
            txtFlightNumber.KeyDown += txtFlightNumber_KeyDown;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.PowderBlue;
            btnSearch.Location = new Point(740, 25);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(97, 29);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(50, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 47);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // flwLayoutPanelFlights
            // 
            flwLayoutPanelFlights.AutoScroll = true;
            flwLayoutPanelFlights.Location = new Point(12, 81);
            flwLayoutPanelFlights.Name = "flwLayoutPanelFlights";
            flwLayoutPanelFlights.Size = new Size(895, 393);
            flwLayoutPanelFlights.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(106, 27);
            label1.Name = "label1";
            label1.Size = new Size(126, 23);
            label1.TabIndex = 4;
            label1.Text = "Flight Number:";
            // 
            // FlightsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(941, 505);
            Controls.Add(label1);
            Controls.Add(flwLayoutPanelFlights);
            Controls.Add(pictureBox1);
            Controls.Add(btnSearch);
            Controls.Add(txtFlightNumber);
            Name = "FlightsForm";
            Text = "FlightsForm";
            Load += FlightsForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion


        private TextBox txtFlightNumber;
        private Button btnSearch;
        private PictureBox pictureBox1;
        private FlowLayoutPanel flwLayoutPanelFlights;
        private Label label1;
    }
}