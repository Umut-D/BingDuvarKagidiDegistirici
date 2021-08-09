using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BingDuvarKagidiLibrary
{
    public class Cozunurluk
    {
        public string EkranCozunurlukEki { get; private set; }

        public Cozunurluk()
        {
            Bul();
        }

        private void Bul()
        {
            Rectangle ekranSinirlari = Screen.PrimaryScreen.Bounds;
            foreach (var cozunurluk in Olculer())
            {
                if (cozunurluk.Key == ekranSinirlari.Width){
                    EkranCozunurlukEki = cozunurluk.Value;
                    return;
                }
            }
        }

        private Dictionary<int, string> Olculer()
        {
            Dictionary<int, string> cozunurlukler = new Dictionary<int, string>
            {
                {640, "_640x480.jpg"},
                {800, "_800x600.jpg"},
                {1024, "_1024x768.jpg"},
                {1280, "_1280x720.jpg"},
                {1366, "_1366x768.jpg"},
                {1920, "_1920x1080.jpg"}
            };

            return cozunurlukler;
        }
    }
}