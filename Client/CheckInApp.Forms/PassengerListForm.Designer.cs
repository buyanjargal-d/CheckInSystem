namespace CheckInApp.Forms
{
    partial class PassengerListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PassengerListForm));
            txtPassport = new TextBox();
            lblPassport = new Label();
            pictureBox1 = new PictureBox();
            btnSearch = new Button();
            dgvPassengers = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPassengers).BeginInit();
            SuspendLayout();
            // 
            // txtPassport
            // 
            txtPassport.Location = new Point(230, 30);
            txtPassport.Name = "txtPassport";
            txtPassport.Size = new Size(349, 27);
            txtPassport.TabIndex = 0;
            // 
            // lblPassport
            // 
            lblPassport.AutoSize = true;
            lblPassport.Font = new Font("Segoe UI", 10F);
            lblPassport.Location = new Point(25, 31);
            lblPassport.Name = "lblPassport";
            lblPassport.Size = new Size(146, 23);
            lblPassport.TabIndex = 1;
            lblPassport.Text = "Passport Number:";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(188, 30);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(36, 26);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.LightSkyBlue;
            btnSearch.Location = new Point(585, 30);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // dgvPassengers
            // 
            dgvPassengers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPassengers.Location = new Point(25, 82);
            dgvPassengers.Name = "dgvPassengers";
            dgvPassengers.RowHeadersWidth = 51;
            dgvPassengers.Size = new Size(659, 459);
            dgvPassengers.TabIndex = 4;
            // 
            // PassengerListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(776, 616);
            Controls.Add(dgvPassengers);
            Controls.Add(btnSearch);
            Controls.Add(pictureBox1);
            Controls.Add(lblPassport);
            Controls.Add(txtPassport);
            Name = "PassengerListForm";
            Text = "PassengersListForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPassengers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPassport;
        private Label lblPassport;
        private PictureBox pictureBox1;
        private Button btnSearch;
        private DataGridView dgvPassengers;
    }
}