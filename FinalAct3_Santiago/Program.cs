using System;
using System.Windows.Forms;

namespace InventorySystem1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Database.Initialize();

            Application.Run(new LoginForm());
        }
    }
}