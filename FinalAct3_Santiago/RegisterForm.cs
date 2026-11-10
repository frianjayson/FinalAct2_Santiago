using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventorySystem1
{
    public class RegisterForm : Form
    {
        private TextBox txtName = new TextBox();
        private TextBox txtEmail = new TextBox();
        private TextBox txtPassword = new TextBox();
        private TextBox txtConfirm = new TextBox();

        public RegisterForm()
        {
            Text = "Inventory Management System 1 - Register";
            Size = new Size(430, 540);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.FromArgb(14, 25, 43);
            Font = new Font("Segoe UI", 10);

            Label heading = new Label {
                Text = "CREATE ACCOUNT",
                ForeColor = Color.DeepSkyBlue,
                Font = new Font("Segoe UI", 19, FontStyle.Bold),
                AutoSize = false, TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(25, 20), Size = new Size(365, 45)
            };
            Controls.Add(heading);

            txtName = AddField("FULL NAME", 80);
            txtEmail = AddField("EMAIL ADDRESS", 155);
            txtPassword = AddField("PASSWORD", 230);
            txtPassword.UseSystemPasswordChar = true;
            txtConfirm = AddField("CONFIRM PASSWORD", 305);
            txtConfirm.UseSystemPasswordChar = true;

            Button btnRegister = new Button {
                Text = "REGISTER", Location = new Point(55, 390),
                Size = new Size(300, 42),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White, FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += Register;
            Controls.Add(btnRegister);
        }

        private TextBox AddField(string label, int y)
        {
            Controls.Add(new Label {
                Text = label, ForeColor = Color.White,
                Location = new Point(55, y), AutoSize = true
            });

            TextBox box = new TextBox {
                Location = new Point(55, y + 25),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 11)
            };
            Controls.Add(box);
            return box;
        }

        private void Register(object? sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (name == "" || email == "" || password == "" || txtConfirm.Text == "")
            {
                MessageBox.Show("Please complete all fields.");
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.");
                return;
            }

            if (password != txtConfirm.Text)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                if (Database.EmailExists(email))
                {
                    MessageBox.Show("Email is already registered.");
                    return;
                }

                Database.Register(name, email, password);
                MessageBox.Show("Account created successfully!",
                    "Registration Complete", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registration Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
