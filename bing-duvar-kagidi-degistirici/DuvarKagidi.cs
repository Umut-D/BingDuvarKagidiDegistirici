using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace bing_duvar_kagidi_degistirici
{
    class DuvarKagidi
    {
        public enum Konumlandirma
        {
            Dose,
            Merkezi,
            Uzat
        }

        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public static void DuvarKagidiOlustur(Uri uri, Konumlandirma stil)
        {
            var webAkis = new WebClient().OpenRead(uri.ToString());

            if (webAkis != null)
            {
                var img = Image.FromStream(webAkis);
                var geciciDizin = Path.Combine(Path.GetTempPath(), "BingWP.bmp");
                img.Save(geciciDizin, ImageFormat.Bmp);

                var anahtar = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
                if (stil == Konumlandirma.Uzat)
                {
                    if (anahtar != null)
                    {
                        anahtar.SetValue(@"WallpaperStyle", 2.ToString());
                        anahtar.SetValue(@"TileWallpaper", 0.ToString());
                    }
                }

                if (stil == Konumlandirma.Merkezi)
                {
                    if (anahtar != null)
                    {
                        anahtar.SetValue(@"WallpaperStyle", 1.ToString());
                        anahtar.SetValue(@"TileWallpaper", 0.ToString());
                    }
                }

                if (stil == Konumlandirma.Dose)
                {
                    if (anahtar != null)
                    {
                        anahtar.SetValue(@"WallpaperStyle", 1.ToString());
                        anahtar.SetValue(@"TileWallpaper", 1.ToString());
                    }
                }

                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, geciciDizin,
                    SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }
        }
    }
}