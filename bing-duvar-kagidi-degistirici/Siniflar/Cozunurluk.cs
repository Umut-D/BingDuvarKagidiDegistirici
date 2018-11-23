using System.Drawing;
using System.Windows.Forms;

namespace BingDuvarKagidi.Siniflar
{
    public class Cozunurluk
    {
        // Değişkenler
        private readonly Rectangle _cozunurluk = Screen.PrimaryScreen.Bounds;
        public string KullaniciCozunurluk { get; set; }

        public string CozunurlukBul()
        {
            // Ekran çözünürlüğünü belirle ve bunu geri döndür
            switch (_cozunurluk.Width.ToString())
            {
                case "640":
                    KullaniciCozunurluk = "_640x480.jpg";
                    break;
                case "800":
                    KullaniciCozunurluk = "_800x600.jpg";
                    break;
                case "1024":
                    KullaniciCozunurluk = "_1024x768.jpg";
                    break;
                case "1280":
                    KullaniciCozunurluk = "_1280x720.jpg";
                    break;
                case "1366":
                    KullaniciCozunurluk = "_1366x768.jpg";
                    break;
                default:
                    KullaniciCozunurluk = "_1920x1080.jpg";
                    break;
            }

            return KullaniciCozunurluk;
        }
    }
}