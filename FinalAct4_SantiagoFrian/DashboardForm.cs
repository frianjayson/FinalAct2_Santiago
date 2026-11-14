
using System;
using System.Drawing;
using System.Windows.Forms;

namespace frianlogin
{
    public partial class DashboardForm : Form
    {
        public DashboardForm(string fullName)
        {
            InitializeComponent();
            Controls.Clear();

            Text = "frianlogin - Dashboard";
            Size = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Theme.ApplyForm(this);

            Label heading = new Label
            {
                Text = "FRIANLOGIN",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                Location = new Point(100, 60),
                Size = new Size(400, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Theme.StyleLabel(heading, true);

            Label welcome = new Label
            {
                Text = "Welcome, " + fullName + "!",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(80, 145),
                Size = new Size(440, 55),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Theme.StyleLabel(welcome, true);

            Label message = new Label
            {
                Text = "You have successfully logged in.",
                Font = new Font("Segoe UI", 11),
                Location = new Point(100, 205),
                Size = new Size(400, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Theme.StyleLabel(message);

            Button btnLogout = new Button
            {
                Text = "LOG OUT",
                Location = new Point(190, 275),
                Size = new Size(200, 45)
            };

            Theme.StyleButton(btnLogout);

            btnLogout.Click += (sender, e) =>
            {
                Close();
            };

            Controls.Add(heading);
            Controls.Add(welcome);
            Controls.Add(message);
            Controls.Add(btnLogout);
        }
    }
}
