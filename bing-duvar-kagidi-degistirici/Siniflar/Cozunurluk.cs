using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BingDuvarKagidi.Siniflar
{
    public class Cozunurluk
    {
        private string MevcutCozunurluk { get; set; }

        private Dictionary<int, string> Cozunurlukler()
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

        public string Bul()
        {
            Rectangle ekran = Screen.PrimaryScreen.Bounds;
            foreach (KeyValuePair<int, string> cozunurluk in Cozunurlukler())
            {
                if (cozunurluk.Key == ekran.Width)
                    MevcutCozunurluk = cozunurluk.Value;
            }

            return MevcutCozunurluk;
        }
    }
}