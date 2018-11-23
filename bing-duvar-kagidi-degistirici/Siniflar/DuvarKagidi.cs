using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace BingDuvarKagidi.Siniflar
{
    // Ekranın duvar kağıdını oluşturmak için bu sınıf kullanılıyor 
    public class DuvarKagidi
    {
        // Duvar kağıdında değişiklik yapmak için user32.dll'yi kullanmak ve işletim sistemi kaynaklarına erişmek gerek 
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

        // Ekran konumlandırma için gerekli Enumeration'lar
        public enum Konumlandirma
        {
            Dose,
            Merkezi,
            Uzat
        }

        // Ekranın duvar kağıdını oluştur ve ekrana yerleştir
        public static void DuvarKagidiOlustur(Uri uri, Konumlandirma stil)
        {
            var webAkis = new WebClient().OpenRead(uri.ToString());

            if (webAkis != null)
            {
                var bingGorsel = Image.FromStream(webAkis);
                var geciciDizin = Path.Combine(Path.GetTempPath(), "BingWP.bmp");
                bingGorsel.Save(geciciDizin, ImageFormat.Bmp);

                // Regedit'teki ilgili ayarı açarak okuma yap
                RegistryKey anahtar = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);

                // Konumlandırma türüne göre duvar kağıdını yerleştir
                if (stil == Konumlandirma.Uzat && anahtar != null)
                {
                    anahtar.SetValue(@"WallpaperStyle", 2.ToString());
                    anahtar.SetValue(@"TileWallpaper", 0.ToString());
                }

                if (stil == Konumlandirma.Merkezi && anahtar != null)
                {
                    anahtar.SetValue(@"WallpaperStyle", 1.ToString());
                    anahtar.SetValue(@"TileWallpaper", 0.ToString());
                }

                if (stil == Konumlandirma.Dose && anahtar != null)
                {
                    anahtar.SetValue(@"WallpaperStyle", 1.ToString());
                    anahtar.SetValue(@"TileWallpaper", 1.ToString());
                }

                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, geciciDizin, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }
        }
    }

    public class DuvarKagidiIndir : BingWebSiteleri
    {
        // Değişken, sınıflar, diziler
        public string HtmlOku, DuzenliHtmlLink, DuzenliBilgi;
        private int _bilgiBaslangic, _bilgiBitis;
        public DateTime Tarih = DateTime.Now;
        public readonly Match[] EslestirilenLink = new Match[1];

        public void DuvarKagidiIndirVeYukle(PictureBox pbox, string ekranCozunurlugu)
        {
            try
            {
                pbox.InitialImage = null;

                // 1. HTML sayfa kodunu (seçilen web sitesine göre) indir
                WebClient webIstemicisi = new WebClient();
                HtmlOku = webIstemicisi.DownloadString(SecilenSiteAdresi);

                //Regex regex = new Regex(": \"(.*\\.jpe?g|.*\\.JPE?G)");
                //var link = regex.Match(HtmlOku);

                // 3. HTML'den duvar kağıdı linkini ayıkla 
                EslestirilenLink[0] = Regex.Match(HtmlOku, "(\\S+?)\\.(jpg)");
             
                // 4. Gelen düzenli ifadeye gerekli eklemeleri yaparak tam linki oluştur
                DuzenliHtmlLink = EslestirilenLink[0].Groups[1].ToString().Substring(1).Replace(@"\", "");
                DuzenliHtmlLink = BingWebAdresi + DuzenliHtmlLink.Substring(0, DuzenliHtmlLink.LastIndexOf("_", StringComparison.Ordinal)) + ekranCozunurlugu;

                // 5. HTML'den duvar kağıdı bilgisini ayıkla (Bir sonraki versiyonda Regex ile alsam iyi olur)
                _bilgiBaslangic = HtmlOku.IndexOf("copyright\":\"", StringComparison.Ordinal) +
                                  "copyright\":\" = ".Length;
                _bilgiBitis = HtmlOku.IndexOf(",\"copyrightlink", StringComparison.Ordinal);
                DuzenliBilgi = HtmlOku.Substring(_bilgiBaslangic - 3, (_bilgiBitis) - _bilgiBaslangic + 2);

                // 6. Farklı karakterlerin kodlamasını düzelt (UTF8)
                var bytes = Encoding.Default.GetBytes(DuzenliBilgi);
                DuzenliBilgi = Encoding.UTF8.GetString(bytes);

                // 7. Duvar kağıdını images klasörünün içine at, klasör yoksa oluştur
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "images"))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "images");

                // 8. Duvar kağıdını images klasörünün içine indir
                webIstemicisi.DownloadFile(DuzenliHtmlLink,
                    AppDomain.CurrentDomain.BaseDirectory + "images\\" + Tarih.ToString("MM.dd.yyyy") + @"-Bing" +
                    UlkeAdi + ".bmp");

                // 9. Duvar kağıdını görüntüle
                pbox.Image = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "images\\" +
                                        Tarih.ToString("MM.dd.yyyy") + @"-Bing" + UlkeAdi + ".bmp");

                webIstemicisi.Dispose();
            }
            catch (WebException gorselUzerineYazmaHataMesaji)
            {
                HataMesajlari.UzerineYazmaHataMesaji(gorselUzerineYazmaHataMesaji.Message);
            }
            catch (Exception genelHataMesaji)
            {
                HataMesajlari.GenelHataMesaji(genelHataMesaji.Message);
            }
        }
    }
}