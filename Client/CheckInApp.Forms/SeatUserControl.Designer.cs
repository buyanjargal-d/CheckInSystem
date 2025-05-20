namespace CheckInApp.Forms
{
    partial class SeatUserControl
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
            btnSeat = new Button();
            SuspendLayout();
            // 
            // btnSeat
            // 
            btnSeat.Location = new Point(0, 0);
            btnSeat.Name = "btnSeat";
            btnSeat.Size = new Size(75, 75);
            btnSeat.TabIndex = 0;
            btnSeat.UseVisualStyleBackColor = true;
            // 
            // SeatUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnSeat);
            Name = "SeatUserControl";
            Size = new Size(78, 84);
            ResumeLayout(false);
        }

        #endregion

        private Button btnSeat;
    }
}
