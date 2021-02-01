using System.Collections.Generic;

namespace BingDuvarKagidi.Siniflar
{
    public class Ulke
    {
        private readonly string _secilenUlke;
        private string _bingWebAdresi;

        public string BingWebAdresi
        {
            get => _bingWebAdresi;
            set => _bingWebAdresi = @"http://www.bing.com/?cc=" + value;
        }

        public Ulke(string secilenUlke)
        {
            _secilenUlke = secilenUlke;
            
            SeciliUlkeyiAdreseEkle();
        }

        private void SeciliUlkeyiAdreseEkle()
        {
            foreach (var ulke in UlkeEkleri())
            {
                if (ulke.Key == _secilenUlke){
                    BingWebAdresi += ulke.Value;
                    return;
                }
            }
        }

        private Dictionary<string, string> UlkeEkleri()
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
    }
}