using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CheckInApp.Forms; 
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckInApp.Forms
{
    public partial class LoginForm: Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            cmbRole.Items.AddRange(new string[] { "Clerk", "Passenger", "System" });
            cmbRole.SelectedIndex = 0; // анхны утга
        }

        // Нууц үгний талбарыг нууцлах
       // txtPassword.UseSystemPasswordChar = true;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string role = cmbRole.SelectedItem?.ToString();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("⛔ Role болон Password-ийг бөглөнө үү", "Анхааруулга");
                return;
            }

            // Жишээ хэрэглэгчийн мэдээлэл (энэ хэсгийг хүсвэл DB-р холбож болно)
            if (IsValidLogin(role, password))
            {
                MessageBox.Show("✅ Нэвтрэлт амжилттай!", "Login");

                // Тохирох form-ыг нээж, энэ LoginForm-ыг хаах
                switch (role)
                {
                    case "Clerk":
                        new FlightsForm(role).Show();
                        break;
                    case "Passenger":
                        new FlightsForm(role).Show();
                        break;
                    case "System":
                        new FlightsForm(role).Show();
                        break;
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show("❌ Нууц үг буруу байна!", "Login error");
            }
        }

        private bool IsValidLogin(string role, string password)
        {
            return (role == "Clerk" && password == "clerk123") ||
                   (role == "Passenger" && password == "pass123") ||
                   (role == "System" && password == "sysadmin");
        }
    }
}
