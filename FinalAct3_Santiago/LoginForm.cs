using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventorySystem1
{
    public class LoginForm : Form
    {
        private readonly TextBox txtEmail = new TextBox();
        private readonly TextBox txtPassword = new TextBox();

        public LoginForm()
        {
            Text = "Inventory Management System 1 - Login";
            Size = new Size(430, 420);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.FromArgb(14, 25, 43);
            Font = new Font("Segoe UI", 10);

            Label heading = new Label {
                Text = "Inventory Management System 1",
                ForeColor = Color.DeepSkyBlue,
                Font = new Font("Segoe UI", 19, FontStyle.Bold),
                AutoSize = false, TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(25, 25), Size = new Size(365, 45)
            };
            Controls.Add(heading);

            AddLabel("EMAIL ADDRESS", 55, 100);
            txtEmail.Location = new Point(55, 128);
            txtEmail.Size = new Size(300, 30);
            Controls.Add(txtEmail);

            AddLabel("PASSWORD", 55, 185);
            txtPassword.Location = new Point(55, 213);
            txtPassword.Size = new Size(300, 30);
            txtPassword.UseSystemPasswordChar = true;
            Controls.Add(txtPassword);

            Button btnLogin = MakeButton("LOGIN", 55, 270);
            btnLogin.Click += Login;
            Controls.Add(btnLogin);

            Button btnRegister = MakeButton("CREATE ACCOUNT", 55, 320);
            btnRegister.BackColor = Color.FromArgb(45, 65, 90);
            btnRegister.Click += (s, e) =>
            {
                using (RegisterForm form = new RegisterForm())
                    form.ShowDialog(this);
            };
            Controls.Add(btnRegister);
        }

        private void AddLabel(string text, int x, int y)
        {
            Controls.Add(new Label {
                Text = text, ForeColor = Color.White,
                Location = new Point(x, y), AutoSize = true
            });
        }

        private Button MakeButton(string text, int x, int y)
        {
            Button button = new Button {
                Text = text, Location = new Point(x, y),
                Size = new Size(300, 40),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White, FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        private void Login(object? sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (email == "" || password == "")
            {
                MessageBox.Show("Please enter your email and password.");
                return;
            }

            try
            {
                if (Database.Login(email, password, out string fullName))
                {
                    Hide();
                    using (DashboardForm dashboard = new DashboardForm(fullName))
                        dashboard.ShowDialog();
                    txtPassword.Clear();
                    Show();
                }
                else
                {
                    MessageBox.Show("Incorrect email or password.",
                        "Login Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
