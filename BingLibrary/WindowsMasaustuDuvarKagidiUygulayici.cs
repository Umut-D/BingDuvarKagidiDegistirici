using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BingLibrary
{
    /// <summary>
    /// user32.dll SystemParametersInfo çağrısı ile duvar kağıdını uygular.
    /// </summary>
    public sealed class WindowsMasaustuDuvarKagidiUygulayici : IDuvarKagidiUygulayici
    {
        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public int Uygula(string tamDosyaYolu)
        {
            int sonuc = SystemParametersInfo(
                SPI_SETDESKWALLPAPER,
                0,
                tamDosyaYolu,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

            if (sonuc == 0)
            {
                int hataKodu = Marshal.GetLastWin32Error();
                throw new Win32Exception(hataKodu, "Windows duvar kağıdı uygulanamadı.");
            }

            return sonuc;
        }
    }
}
