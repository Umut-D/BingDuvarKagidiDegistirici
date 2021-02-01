using System;
using System.Text;
using System.Text.RegularExpressions;

namespace BingDuvarKagidi.Siniflar
{
    public class Link
    {
        private readonly Gorsel _gorsel;

        public Link(Gorsel gorsel)
        {
            _gorsel = gorsel;
        }

        public void DuzenlenmisLink(string cozunurluk, string htmlOku)
        {
            Match[] linkEslestir = HtmldenGorselLinkiniAl(htmlOku);
            string gorselLink = linkEslestir[0].Groups[1].ToString().Replace("content=\"", "");
            _gorsel.Adres = gorselLink.Substring(0, gorselLink.LastIndexOf("_", StringComparison.Ordinal)) + cozunurluk;
            
            int bilgiBaslangic = htmlOku.LastIndexOf("\"Title\":", StringComparison.Ordinal);
            int bilgiBitis = htmlOku.LastIndexOf("\"SocialGood\"", StringComparison.Ordinal);
            _gorsel.Bilgi = htmlOku.Substring(bilgiBaslangic + 9, bilgiBitis - bilgiBaslangic - 11);
            
            KarakterKodlamasiniDuzelt(_gorsel.Bilgi);
        }

        private Match[] HtmldenGorselLinkiniAl(string htmlOku)
        {
            Match[] eslesenLink = new Match[1];
            eslesenLink[0] = Regex.Match(htmlOku, "(\\S+?)\\.(jpg)");

            return eslesenLink;
        }

        private void KarakterKodlamasiniDuzelt(string duzenliBilgi)
        {
            byte[] bytes = Encoding.Default.GetBytes(duzenliBilgi.Replace("\",\"Copyright\":\"", " "));
            _gorsel.Bilgi = Encoding.UTF8.GetString(bytes);
        }
    }
}