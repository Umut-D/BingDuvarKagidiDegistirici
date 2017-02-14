using System.Drawing;
using System.Windows.Forms;

namespace bing_duvar_kagidi_degistirici
{
    internal class CozunurlukBul
    {
        // Değişkenler
        private Rectangle _cozunurluk = Screen.PrimaryScreen.Bounds;
        private string _kullaniciCozunurluk;

        public string Cozunurluk()
        {
            // Kullanıcının çözünürlüğünü belirle ve bunu döndür
            switch (_cozunurluk.Width.ToString())
            {
                case "640":
                    _kullaniciCozunurluk = "640x480.jpg";
                    break;
                case "800":
                    _kullaniciCozunurluk = "800x600.jpg";
                    break;
                case "1024":
                    _kullaniciCozunurluk = "1024x768.jpg";
                    break;
                case "1280":
                    _kullaniciCozunurluk = "1280x720.jpg";
                    break;
                case "1366":
                    _kullaniciCozunurluk = "1366x768.jpg";
                    break;
                default:
                    _kullaniciCozunurluk = "1920x1080.jpg";
                    break;
            }
            return _kullaniciCozunurluk;
        }
    }
}