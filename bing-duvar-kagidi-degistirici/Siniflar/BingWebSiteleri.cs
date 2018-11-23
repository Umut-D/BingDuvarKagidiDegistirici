namespace BingDuvarKagidi.Siniflar
{
    public class BingWebSiteleri
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

        // cBox'tan seçili olan web sitesinin adresini döndür
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
}