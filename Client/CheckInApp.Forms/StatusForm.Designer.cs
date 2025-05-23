namespace CheckInApp.Forms
{
    partial class StatusForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusForm));
            label1 = new Label();
            pictureBox1 = new PictureBox();
            btnSearch = new Button();
            txtFlightNumber = new TextBox();
            dgvFlights = new DataGridView();
            label2 = new Label();
            label3 = new Label();
            lblFlightNumber = new Label();
            cmbStatus = new ComboBox();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFlights).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(108, 25);
            label1.Name = "label1";
            label1.Size = new Size(126, 23);
            label1.TabIndex = 1;
            label1.Text = "Flight Number:";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(49, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(53, 49);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.LightSkyBlue;
            btnSearch.Location = new Point(566, 24);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(84, 28);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtFlightNumber
            // 
            txtFlightNumber.Location = new Point(240, 24);
            txtFlightNumber.Name = "txtFlightNumber";
            txtFlightNumber.Size = new Size(320, 27);
            txtFlightNumber.TabIndex = 5;
            txtFlightNumber.KeyDown += txtFlightNumber_KeyDown;
            // 
            // dgvFlights
            // 
            dgvFlights.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFlights.Location = new Point(25, 78);
            dgvFlights.Name = "dgvFlights";
            dgvFlights.RowHeadersWidth = 51;
            dgvFlights.Size = new Size(679, 428);
            dgvFlights.TabIndex = 6;
            dgvFlights.CellClick += dgvFlights_CellClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(775, 78);
            label2.Name = "label2";
            label2.Size = new Size(126, 23);
            label2.TabIndex = 7;
            label2.Text = "Flight Number:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(775, 149);
            label3.Name = "label3";
            label3.Size = new Size(61, 23);
            label3.TabIndex = 8;
            label3.Text = "Status:";
            // 
            // lblFlightNumber
            // 
            lblFlightNumber.AutoSize = true;
            lblFlightNumber.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFlightNumber.Location = new Point(907, 78);
            lblFlightNumber.Name = "lblFlightNumber";
            lblFlightNumber.Size = new Size(134, 23);
            lblFlightNumber.TabIndex = 9;
            lblFlightNumber.Text = "lblFlightNumber";
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(907, 144);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(221, 28);
            cmbStatus.TabIndex = 10;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.LightBlue;
            btnSave.Location = new Point(775, 223);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(93, 29);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // StatusForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1254, 534);
            Controls.Add(btnSave);
            Controls.Add(cmbStatus);
            Controls.Add(lblFlightNumber);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dgvFlights);
            Controls.Add(txtFlightNumber);
            Controls.Add(btnSearch);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Name = "StatusForm";
            Text = "StatusForm";
            Load += StatusForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFlights).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private PictureBox pictureBox1;
        private Button btnSearch;
        private TextBox txtFlightNumber;
        private DataGridView dgvFlights;
        private Label label2;
        private Label label3;
        private Label lblFlightNumber;
        private ComboBox cmbStatus;
        private Button btnSave;
    }
}