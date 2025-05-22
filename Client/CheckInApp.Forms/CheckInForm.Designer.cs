namespace CheckInApp.Forms;

partial class CheckInForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        label1 = new Label();
        txtPassportId = new TextBox();
        btnSearchPassenger = new Button();
        comboSeatSelection = new ComboBox();
        label2 = new Label();
        lblPassengerFullName = new Label();
        label3 = new Label();
        label4 = new Label();
        btnAssignSeat = new Button();
        label5 = new Label();
        lblFlightStatus = new Label();
        lblStatusMessage = new Label();
        dgvPassengers = new DataGridView();
        flwLayoutPanelSeat = new FlowLayoutPanel();
        lblFlightCode = new Label();
        ((System.ComponentModel.ISupportInitialize)dgvPassengers).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        label1.Location = new Point(37, 36);
        label1.Name = "label1";
        label1.Size = new Size(88, 20);
        label1.TabIndex = 0;
        label1.Text = "Passport Id:";
        // 
        // txtPassportId
        // 
        txtPassportId.Location = new Point(180, 32);
        txtPassportId.Name = "txtPassportId";
        txtPassportId.Size = new Size(401, 27);
        txtPassportId.TabIndex = 1;
        // 
        // btnSearchPassenger
        // 
        btnSearchPassenger.BackColor = Color.PowderBlue;
        btnSearchPassenger.Location = new Point(604, 32);
        btnSearchPassenger.Name = "btnSearchPassenger";
        btnSearchPassenger.Size = new Size(95, 29);
        btnSearchPassenger.TabIndex = 2;
        btnSearchPassenger.Text = "Search";
        btnSearchPassenger.UseVisualStyleBackColor = false;
        btnSearchPassenger.Click += btnSearchPassenger_Click;
        // 
        // comboSeatSelection
        // 
        comboSeatSelection.DisplayMember = "SeatNumber";
        comboSeatSelection.DropDownStyle = ComboBoxStyle.DropDownList;
        comboSeatSelection.FormattingEnabled = true;
        comboSeatSelection.Location = new Point(887, 120);
        comboSeatSelection.Name = "comboSeatSelection";
        comboSeatSelection.Size = new Size(397, 28);
        comboSeatSelection.TabIndex = 3;
        comboSeatSelection.ValueMember = "SeatNumber";
        comboSeatSelection.SelectedIndexChanged += comboSeatSelection_SelectedIndexChanged;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        label2.Location = new Point(741, 32);
        label2.Name = "label2";
        label2.Size = new Size(127, 20);
        label2.TabIndex = 4;
        label2.Text = "Passenger Name:";
        // 
        // lblPassengerFullName
        // 
        lblPassengerFullName.AutoSize = true;
        lblPassengerFullName.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblPassengerFullName.Location = new Point(887, 32);
        lblPassengerFullName.Name = "lblPassengerFullName";
        lblPassengerFullName.Size = new Size(152, 20);
        lblPassengerFullName.TabIndex = 5;
        lblPassengerFullName.Text = "Passenger Full Name";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        label3.Location = new Point(743, 78);
        label3.Name = "label3";
        label3.Size = new Size(52, 20);
        label3.TabIndex = 6;
        label3.Text = "Flight:";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        label4.Location = new Point(743, 128);
        label4.Name = "label4";
        label4.Size = new Size(42, 20);
        label4.TabIndex = 7;
        label4.Text = "Seat:";
        // 
        // btnAssignSeat
        // 
        btnAssignSeat.BackColor = Color.LightSkyBlue;
        btnAssignSeat.Location = new Point(743, 413);
        btnAssignSeat.Name = "btnAssignSeat";
        btnAssignSeat.Size = new Size(96, 38);
        btnAssignSeat.TabIndex = 9;
        btnAssignSeat.Text = "Assign";
        btnAssignSeat.UseVisualStyleBackColor = false;
        btnAssignSeat.Click += btnAssignSeat_Click;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        label5.Location = new Point(743, 480);
        label5.Name = "label5";
        label5.Size = new Size(97, 20);
        label5.TabIndex = 11;
        label5.Text = "Flight Status:";
        // 
        // lblFlightStatus
        // 
        lblFlightStatus.AutoSize = true;
        lblFlightStatus.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblFlightStatus.Location = new Point(889, 480);
        lblFlightStatus.Name = "lblFlightStatus";
        lblFlightStatus.Size = new Size(93, 20);
        lblFlightStatus.TabIndex = 12;
        lblFlightStatus.Text = "Flight Status";
        // 
        // lblStatusMessage
        // 
        lblStatusMessage.AutoSize = true;
        lblStatusMessage.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblStatusMessage.Location = new Point(743, 534);
        lblStatusMessage.Name = "lblStatusMessage";
        lblStatusMessage.Size = new Size(126, 20);
        lblStatusMessage.TabIndex = 13;
        lblStatusMessage.Text = "lblStatusMessage";
        // 
        // dgvPassengers
        // 
        dgvPassengers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvPassengers.Location = new Point(37, 79);
        dgvPassengers.Name = "dgvPassengers";
        dgvPassengers.RowHeadersWidth = 51;
        dgvPassengers.Size = new Size(662, 476);
        dgvPassengers.TabIndex = 14;
        dgvPassengers.CellClick += dgvPassengers_CellClick;
        // 
        // flwLayoutPanelSeat
        // 
        flwLayoutPanelSeat.Location = new Point(743, 191);
        flwLayoutPanelSeat.Name = "flwLayoutPanelSeat";
        flwLayoutPanelSeat.Size = new Size(550, 202);
        flwLayoutPanelSeat.TabIndex = 15;
        // 
        // lblFlightCode
        // 
        lblFlightCode.AutoSize = true;
        lblFlightCode.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblFlightCode.Location = new Point(889, 78);
        lblFlightCode.Name = "lblFlightCode";
        lblFlightCode.Size = new Size(106, 20);
        lblFlightCode.TabIndex = 16;
        lblFlightCode.Text = "Flight number";
        // 
        // CheckInForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1333, 595);
        Controls.Add(lblFlightCode);
        Controls.Add(flwLayoutPanelSeat);
        Controls.Add(dgvPassengers);
        Controls.Add(lblStatusMessage);
        Controls.Add(lblFlightStatus);
        Controls.Add(label5);
        Controls.Add(btnAssignSeat);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(lblPassengerFullName);
        Controls.Add(label2);
        Controls.Add(comboSeatSelection);
        Controls.Add(btnSearchPassenger);
        Controls.Add(txtPassportId);
        Controls.Add(label1);
        Name = "CheckInForm";
        Text = "CheckInForm";
        Load += CheckInForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvPassengers).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private TextBox txtPassportId;
    private Button btnSearchPassenger;
    private ComboBox comboSeatSelection;
    private Label label2;
    private Label lblPassengerFullName;
    private Label label3;
    private Label label4;
    private Button btnAssignSeat;
    private Label label5;
    private Label lblFlightStatus;
    private Label lblStatusMessage;
    private DataGridView dgvPassengers;
    private FlowLayoutPanel flwLayoutPanelSeat;
    private Label lblFlightCode;
}
