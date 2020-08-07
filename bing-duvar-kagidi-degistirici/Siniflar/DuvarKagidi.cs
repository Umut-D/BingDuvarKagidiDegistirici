using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace BingDuvarKagidi.Siniflar
{
    public class DuvarKagidi
    {
        // Duvar kağıdında değişiklik yapmak için user32.dll'yi kullan ve işletim sistemi kaynaklarına eriş
        [DllImport("user32.dll")]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

        public enum EkranKonumu
        {
            Dose,
            Merkezi,
            Uzat
        }

        private void EkranRegistry(EkranKonumu ekranKonumu)
        {
            // Regedit'te Ekran Konumu ile ilgili ayarı açıp okuma yap
            RegistryKey anahtar = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);

            switch (ekranKonumu)
            {
                case EkranKonumu.Uzat when anahtar != null:
                    anahtar.SetValue(@"WallpaperStyle", 2.ToString());
                    anahtar.SetValue(@"TileWallpaper", 0.ToString());
                    break;
                case EkranKonumu.Merkezi when anahtar != null:
                    anahtar.SetValue(@"WallpaperStyle", 1.ToString());
                    anahtar.SetValue(@"TileWallpaper", 0.ToString());
                    break;
                case EkranKonumu.Dose when anahtar != null:
                    anahtar.SetValue(@"WallpaperStyle", 1.ToString());
                    anahtar.SetValue(@"TileWallpaper", 1.ToString());
                    break;
            }
        }

        private string GeciciGorsel(Stream webAkis)
        {
            Image bingGorsel = Image.FromStream(webAkis);
            string geciciDizin = Path.Combine(Path.GetTempPath(), "BingWP.bmp");
            bingGorsel.Save(geciciDizin, ImageFormat.Bmp);

            return geciciDizin;
        }

        public void Olustur(Uri adres, EkranKonumu ekranKonumu)
        {
            using (WebClient webBaglanti = new WebClient())
            {
                Stream webAkis = webBaglanti.OpenRead(adres);

                EkranRegistry(ekranKonumu);
                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, GeciciGorsel(webAkis), SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

                webAkis?.Dispose();
            }
        }
    }
}