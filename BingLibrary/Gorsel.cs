using System.Drawing;
using System.IO;
using System.Net;

namespace BingLibrary
{
    public class Gorsel
    {
        public string Bilgi { get; set; }
        public string Adres { get; set; }

        public void WebSayfasiniIndir(string cozunurluk, string ayarlardakiSeciliUlke)
        {
            Link link = new Link(this);
            link.DuzenlenmisLink(cozunurluk, HtmlSayfasi(ayarlardakiSeciliUlke));
        }

        public string HtmlSayfasi(string seciliUlke)
        {
            Ulke ulke = new Ulke(seciliUlke);

            using (WebClient webSayfasi = new WebClient())
            {
                return webSayfasi.DownloadString(ulke.BingWebAdresi);
            }
        }

        public Image Yukle()
        {
            Dosya dosya = new Dosya(this);
            using (FileStream gorsel = new FileStream(dosya.DosyaYoluVeAdi, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                return Image.FromStream(gorsel);
            }
        }
    }
}