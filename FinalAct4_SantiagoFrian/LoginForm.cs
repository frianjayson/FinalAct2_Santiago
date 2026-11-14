
using System;
using System.Drawing;
using System.Windows.Forms;

namespace frianlogin
{
    public partial class LoginForm : Form
    {
        private TextBox txtUsername = new TextBox();
        private TextBox txtPassword = new TextBox();

        public LoginForm()
        {
            InitializeComponent();

            Controls.Clear();

            // FORM SETTINGS
            Text = "frianlogin - Login";
            Size = new Size(430, 480);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10);

            // TITLE
            Label title = new Label();
            title.Text = "frianlogin";
            title.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(2, 51, 54);
            title.Location = new Point(65, 35);
            title.Size = new Size(300, 50);
            title.TextAlign = ContentAlignment.MiddleCenter;

            // SUBTITLE
            Label subtitle = new Label();
            subtitle.Text = "Welcome back!";
            subtitle.Font = new Font("Segoe UI", 11);
            subtitle.ForeColor = Color.Gray;
            subtitle.Location = new Point(65, 90);
            subtitle.Size = new Size(300, 30);
            subtitle.TextAlign = ContentAlignment.MiddleCenter;

            // USERNAME
            Label lblUsername = new Label();
            lblUsername.Text = "Username";
            lblUsername.Location = new Point(45, 145);
            lblUsername.Size = new Size(320, 25);

            txtUsername.Location = new Point(45, 170);
            txtUsername.Size = new Size(320, 32);
            txtUsername.Font = new Font("Segoe UI", 11);

            // PASSWORD
            Label lblPassword = new Label();
            lblPassword.Text = "Password";
            lblPassword.Location = new Point(45, 225);
            lblPassword.Size = new Size(320, 25);

            txtPassword.Location = new Point(45, 250);
            txtPassword.Size = new Size(320, 32);
            txtPassword.Font = new Font("Segoe UI", 11);
            txtPassword.UseSystemPasswordChar = true;

            // LOGIN BUTTON
            Button btnLogin = new Button();
            btnLogin.Text = "LOGIN";
            btnLogin.Location = new Point(100, 310);
            btnLogin.Size = new Size(220, 40);
            btnLogin.BackColor = Color.FromArgb(2, 51, 54);
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Click += Login_Click;

            // REGISTER BUTTON
            Button btnRegister = new Button();
            btnRegister.Text = "CREATE ACCOUNT";
            btnRegister.Location = new Point(100, 360);
            btnRegister.Size = new Size(220, 40);
            btnRegister.BackColor = Color.FromArgb(77, 166, 116);
            btnRegister.ForeColor = Color.White;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.Click += Register_Click;

            // ADD CONTROLS
            Controls.Add(title);
            Controls.Add(subtitle);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Controls.Add(btnRegister);

            AcceptButton = btnLogin;
        }

        private void Login_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username == "" || password == "")
            {
                MessageBox.Show(
                    "Enter your username and password.",
                    "Login Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                var user = DatabaseHelper.FindUser(username);

                if (user.HasValue &&
                    PasswordHasher.VerifyPassword(
                        password,
                        user.Value.PasswordHash))
                {
                    MessageBox.Show(
                        "Login successful!",
                        "frianlogin",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    Hide();

                    using (DashboardForm dashboard =
                        new DashboardForm(user.Value.FullName))
                    {
                        dashboard.ShowDialog();
                    }

                    txtPassword.Clear();
                    Show();
                }
                else
                {
                    MessageBox.Show(
                        "Incorrect username or password.",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Application Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void Register_Click(object? sender, EventArgs e)
        {
            using (RegistrationForm registration =
                new RegistrationForm())
            {
                registration.ShowDialog();
            }
        }
    }
}
