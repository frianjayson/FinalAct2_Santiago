using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventorySystem1
{
    public class DashboardForm : Form
    {
        public DashboardForm(string fullName)
        {
            Text = "Inventory Management System 1 - Dashboard";
            Size = new Size(850, 550);
            MinimumSize = new Size(650, 420);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(8, 16, 32);
            Font = new Font("Segoe UI", 10);

            Panel top = new Panel {
                Dock = DockStyle.Top,
                Height = 75,
                BackColor = Color.FromArgb(18, 31, 55)
            };
            Controls.Add(top);

            Label brand = new Label {
                Text = "INVENTORY MANAGEMENT SYSTEM",
                ForeColor = Color.DeepSkyBlue,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(25, 20)
            };
            top.Controls.Add(brand);

            Label welcome = new Label {
                Text = "Welcome, " + fullName + "!",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 25, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(40, 150)
            };
            Controls.Add(welcome);

            Label message = new Label {
                Text = "You have successfully logged in.",
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 12),
                AutoSize = true,
                Location = new Point(43, 210)
            };
            Controls.Add(message);

            Button logout = new Button {
                Text = "LOGOUT",
                Size = new Size(150, 45),
                Location = new Point(43, 275),
                BackColor = Color.FromArgb(180, 45, 45),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            logout.FlatAppearance.BorderSize = 0;
            logout.Click += (s, e) =>
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to logout?",
                    "Confirm Logout",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    Close();
            };
            Controls.Add(logout);
        }
    }
}
