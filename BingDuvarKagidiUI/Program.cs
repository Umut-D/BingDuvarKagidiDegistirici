using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BingDuvarKagidiUI
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            // Yüksek DPI'a sahip ekranlar için gerekli ayarlamalar (I)
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmBing());
        }

        // Yüksek DPI'a sahip ekranlar için gerekli ayarlamalar (II)
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}