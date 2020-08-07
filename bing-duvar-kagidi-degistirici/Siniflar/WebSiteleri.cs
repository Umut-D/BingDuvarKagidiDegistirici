using System.Collections.Generic;

namespace BingDuvarKagidi.Siniflar
{
    public class WebSiteleri
    {
        private string _bingWeb = @"http://www.bing.com/?cc=";
        
        private Dictionary<string, string> Ulkeler()
        {
            Dictionary<string, string> ulkeler = new Dictionary<string, string>
            {
                {"Almanya", "de"},
                {"Amerika", "us"},
                {"Avustralya", "au"},
                {"Brezilya", "br"},
                {"Britanya", "gb"},
                {"Fransa", "fr"},
                {"Hindistan", "in"},
                {"Japonya", "jp"},
                {"Kanada", "ca"},
                {"Turkiye", "tr"}
            };

            return ulkeler;
        }

        public string Adres(string seciliUlke)
        {
            string adres = "";
            foreach (KeyValuePair<string, string> ulke in Ulkeler())
            {
                if (ulke.Key == seciliUlke)
                    adres = _bingWeb + ulke.Value;
            }

            return adres;
        }
    }
}