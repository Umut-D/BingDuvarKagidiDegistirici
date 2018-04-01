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

namespace bing_duvar_kagidi_degistirici.Siniflar
{
    // Bing web siteleri
    internal class BingWebSiteleri
    {
        // Bing web sitesi adresleri ve değişkenler
        public const string BingWebAdresi = @"http://www.bing.com";
        public const string BingAlmanya = @"http://www.bing.com/?cc=de";
        public const string BingAmerika = @"http://www.bing.com/?cc=us";
        public const string BingAvustralya = @"http://www.bing.com/?cc=au";
        public const string BingBrezilya = @"http://www.bing.com/?cc=br";
        public const string BingBritanya = @"http://www.bing.com/?cc=gb";
        public const string BingFransa = @"http://www.bing.com/?cc=fr";
        public const string BingHindistan = @"http://www.bing.com/?cc=in";
        public const string BingJaponya = @"http://www.bing.com/?cc=jp";
        public const string BingKanada = @"http://www.bing.com/?cc=ca";
        public const string BingTurkiye = @"https://www.bing.com/?cc=tr";
        public const string BingYeniZelanda = @"http://www.bing.com/?cc=nz";
        public string SecilenSiteAdresi, UlkeAdi;

        // cBox'tan seçili olan web sitesinin adresini döndür (Enum mu kullansaydım ki?)
        public string SecilenBingWebSitesi(string secilenSite)
        {
            switch (secilenSite)
            {
                case "Almanya":
                    SecilenSiteAdresi = BingAlmanya;
                    UlkeAdi = @" (Almanya)";
                    break;
                case "Amerika":
                    SecilenSiteAdresi = BingAmerika;
                    UlkeAdi = @" (Amerika)";
                    break;
                case "Avustralya":
                    SecilenSiteAdresi = BingAvustralya;
                    UlkeAdi = @" (Avustralya)";
                    break;
                case "Brezilya":
                    SecilenSiteAdresi = BingBrezilya;
                    UlkeAdi = @" (Brezilya)";
                    break;
                case "Britanya":
                    SecilenSiteAdresi = BingBritanya;
                    UlkeAdi = @" (Britanya)";
                    break;
                case "Fransa":
                    SecilenSiteAdresi = BingFransa;
                    UlkeAdi = @" (Fransa)";
                    break;
                case "Hindistan":
                    SecilenSiteAdresi = BingHindistan;
                    UlkeAdi = @" (Hindistan)";
                    break;
                case "Japonya":
                    SecilenSiteAdresi = BingJaponya;
                    UlkeAdi = @" (Japonya)";
                    break;
                case "Kanada":
                    SecilenSiteAdresi = BingKanada;
                    UlkeAdi = @" (Kanada)";
                    break;
                case "Türkiye":
                    SecilenSiteAdresi = BingTurkiye;
                    UlkeAdi = @" (Türkiye)";
                    break;
                case "Yeni Zelanda":
                    SecilenSiteAdresi = BingYeniZelanda;
                    UlkeAdi = @" (Yeni Zelanda)";
                    break;
            }

            return SecilenSiteAdresi;
        }
    }

    // Mevcut bilgisayarın ekran çözünürlüğünü bulmak için bu sınıf kullanılıyor
    internal class Cozunurluk
    {
        // Değişkenler
        private Rectangle _cozunurluk = Screen.PrimaryScreen.Bounds;
        private string _kullaniciCozunurluk;

        public string CozunurlukBul()
        {
            // Ekran çözünürlüğünü belirle ve bunu geri döndür
            switch (_cozunurluk.Width.ToString())
            {
                case "640":
                    _kullaniciCozunurluk = "_640x480.jpg";
                    break;
                case "800":
                    _kullaniciCozunurluk = "_800x600.jpg";
                    break;
                case "1024":
                    _kullaniciCozunurluk = "_1024x768.jpg";
                    break;
                case "1280":
                    _kullaniciCozunurluk = "_1280x720.jpg";
                    break;
                case "1366":
                    _kullaniciCozunurluk = "_1366x768.jpg";
                    break;
                default:
                    _kullaniciCozunurluk = "_1920x1080.jpg";
                    break;
            }

            return _kullaniciCozunurluk;
        }
    }

    // Ekranın duvar kağıdını oluşturmak için bu sınıf kullanılıyor 
    internal class DuvarKagidi
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
            Dose, Merkezi, Uzat
        }

        // Ekranın duvar kağıdını oluştur ve ekrana yerleştir
        public static void DuvarKagidiOlustur(Uri uri, Konumlandirma stil)
        {
            var webAkis = new WebClient().OpenRead(uri.ToString());
                        
            // Gelen veri boş değilse eyleme geç
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

    // Duvar kağıdını indirmek için bu sınıf kullanılıyor (BingWebSiteleri sınıfından kalıtım al)
    internal class DuvarKagidiIndir : BingWebSiteleri
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

                // 3. HTML'den duvar kağıdı linkini ayıkla 
                EslestirilenLink[0] = Regex.Match(HtmlOku, "(\\S+?)\\.(jpg)");

                // 4. Gelen düzenli ifadeye gerekli eklemeleri yaparak tam linki oluştur
                DuzenliHtmlLink = EslestirilenLink[0].Groups[1].ToString().Substring(1);
                DuzenliHtmlLink = BingWebAdresi + DuzenliHtmlLink.Substring(0, DuzenliHtmlLink.LastIndexOf("_", StringComparison.Ordinal)) + ekranCozunurlugu;

                // 5. HTML'den duvar kağıdı bilgisini ayıkla (Bir sonraki versiyonda Regex ile alsam iyi olur)
                _bilgiBaslangic = HtmlOku.IndexOf("copyright\":\"", StringComparison.Ordinal) + "copyright\":\" = ".Length;
                _bilgiBitis = HtmlOku.IndexOf(",\"copyrightlink", StringComparison.Ordinal);

                DuzenliBilgi = HtmlOku.Substring(_bilgiBaslangic -3, (_bilgiBitis) - _bilgiBaslangic+2);

                // 6. Farklı karakterlerin kodlamasını düzelt (UTF8)
                var bytes = Encoding.Default.GetBytes(DuzenliBilgi);
                DuzenliBilgi = Encoding.UTF8.GetString(bytes);
                
                // 7. Duvar kağıdını images klasörünün içine at, klasör yoksa oluştur
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "images"))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "images");

                // 8. Duvar kağıdını images klasörünün içine indir
                webIstemicisi.DownloadFile(DuzenliHtmlLink, AppDomain.CurrentDomain.BaseDirectory + "images\\" + Tarih.ToString("MM.dd.yyyy") + @"-Bing" + UlkeAdi + ".bmp");

                // 9. Duvar kağıdını görüntüle
                pbox.Image = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "images\\" + Tarih.ToString("MM.dd.yyyy") + @"-Bing" + UlkeAdi + ".bmp");

                webIstemicisi.Dispose();
            }
            catch (WebException gorselUzerineYazmaHataMesaji)
            {
                HataMesajlari.GorselUzerineYazmaHataMesaji(gorselUzerineYazmaHataMesaji.Message);
            }
            catch (Exception genelHataMesaji)
            {
                HataMesajlari.GenelHataMesaji(genelHataMesaji.Message);
            }
        }
    }

    // Farklı Bing web sitelerindeki görsellerin önizleme görüntülerini indirmek için bu sınıf kullanılıyor
    internal class ThumbnailIndir : DuvarKagidiIndir
    {
        // Sınıflar
        private static readonly Cozunurluk CozunurlukBul = new Cozunurluk();
        private readonly WebClient _webIstemicisi = new WebClient();
        private readonly string _mevcutCozunurluk = CozunurlukBul.CozunurlukBul();

        public void ThumbnailIndirveYukle(PictureBox pbox, string secilenUlke)
        {
            // 1. HTML sayfa kodunu (seçilen web sitesine göre) indir
            HtmlOku = _webIstemicisi.DownloadString(secilenUlke);

            // 2. HTML'den duvar kağıdı linkini ayıkla 
            EslestirilenLink[0] = Regex.Match(HtmlOku, "(\\S+?)\\.(jpg)");

            // 3. Gelen düzenli ifadeye gerekli eklemeleri yaparak tam linki oluştur
            DuzenliHtmlLink = EslestirilenLink[0].Groups[1].ToString().Substring(1);
            DuzenliHtmlLink = BingWebAdresi + DuzenliHtmlLink.Substring(0, DuzenliHtmlLink.LastIndexOf("_", StringComparison.Ordinal)) + _mevcutCozunurluk;

            // 4. Düzenlenmiş resmi HTML linkine göre istem yap
            WebRequest webIstemi = WebRequest.Create(DuzenliHtmlLink);

            // 5. İstem sonucu dönen değeri akış haline çevir
            WebResponse webDonenDeger = webIstemi.GetResponse();
            Stream gelenAkis = webDonenDeger.GetResponseStream();

            // Gelen akışta sorun yoksa görseli belirlenen boyuta (16:9) dönüştür  
            if (gelenAkis != null)
            {
                Image gorsel = Image.FromStream(gelenAkis);
                Image thumbnail = gorsel.GetThumbnailImage(200, 113, () => false, IntPtr.Zero);
                pbox.Image = thumbnail;
            }            
        }
    }
}