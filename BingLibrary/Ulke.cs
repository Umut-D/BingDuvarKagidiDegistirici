namespace BingLibrary
{
    public class Ulke
    {
        public string BingWebAdresi { get; }

        public Ulke(string secilenUlke)
        {
            string market = UlkeMarketleri.MarketKoduBul(secilenUlke);
            BingWebAdresi = $"https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt={Uri.EscapeDataString(market)}";
        }
    }
}
