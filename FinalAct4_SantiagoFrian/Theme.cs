
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace frianlogin
{
    public static class Theme
    {
        public static readonly Color Background = Color.FromArgb(23, 23, 23);
        public static readonly Color Panel = Color.FromArgb(38, 38, 38);
        public static readonly Color Button = Color.FromArgb(64, 64, 64);
        public static readonly Color ButtonHover = Color.FromArgb(85, 85, 85);
        public static readonly Color PrimaryText = Color.White;
        public static readonly Color SecondaryText = Color.FromArgb(190, 190, 190);
        public static readonly Color InputBackground = Color.White;
        public static readonly Color InputText = Color.Black;

        public static void ApplyForm(Form form)
        {
            form.BackColor = Background;
            form.ForeColor = PrimaryText;
            form.Font = new Font("Segoe UI", 10);
        }


        public static void StyleLabel(System.Windows.Forms.Label label, bool primary = true)
        {
            label.ForeColor = primary ? PrimaryText : SecondaryText;
            label.BackColor = Color.Transparent;
            label.Font = new Font("Segoe UI", 10, FontStyle.Regular);
        }


        public static void StyleTextBox(TextBox textBox)
        {
            textBox.BackColor = InputBackground;
            textBox.ForeColor = InputText;
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void StyleButton(Button button)
        {
            button.BackColor = Button;
            button.ForeColor = PrimaryText;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.FromArgb(100, 100, 100);
            button.FlatAppearance.MouseOverBackColor = ButtonHover;
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(45, 45, 45);
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }
    }
}
