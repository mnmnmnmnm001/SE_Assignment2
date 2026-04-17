// ============================================================
//  Program.cs  –  Application Entry Point
// ============================================================
using System;
using System.Windows.Forms;
using WinformApp.UI;

namespace WinformApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
