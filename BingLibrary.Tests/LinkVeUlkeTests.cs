using System;
using System.Text.Json;
using BingLibrary;
using Xunit;

namespace BingLibrary.Tests
{
    public class LinkAndUlkeTests
    {
        [Fact]
        public void GecerliBingJson_AdresVeBilgiyiDogruAyiklar()
        {
            string json = """
            {
              "images": [
                {
                  "url": "/th?id=OHR.TestGorsel_TR-TR1234567890_1920x1080.jpg",
                  "urlbase": "/th?id=OHR.TestGorsel_TR-TR1234567890",
                  "title": "Kapadokya Sabahı",
                  "copyright": "Göreme Vadisi (© Test Fotoğrafçı)"
                }
              ]
            }
            """;

            var gorsel = new Gorsel();
            var link = new Link(gorsel);

            link.DuzenlenmisLink("_2560x1440.jpg", json);

            Assert.Equal("https://www.bing.com/th?id=OHR.TestGorsel_TR-TR1234567890_2560x1440.jpg", gorsel.Adres);
            Assert.Equal("Kapadokya Sabahı — Göreme Vadisi (© Test Fotoğrafçı)", gorsel.Bilgi);
        }

        [Fact]
        public void BosImagesDizisi_InvalidOperationExceptionFirlatir()
        {
            var link = new Link(new Gorsel());
            Assert.Throws<InvalidOperationException>(() => link.DuzenlenmisLink("_1920x1080.jpg", "{\"images\":[]}"));
        }

        [Fact]
        public void BozukJson_JsonExceptionFirlatir()
        {
            var link = new Link(new Gorsel());
            Assert.ThrowsAny<JsonException>(() => link.DuzenlenmisLink("_1920x1080.jpg", "{ bozuk json "));
        }

        [Theory]
        [InlineData("Turkiye", "mkt=tr-TR")]
        [InlineData("Japonya", "mkt=ja-JP")]
        [InlineData("Almanya", "mkt=de-DE")]
        [InlineData("BilinmeyenUlke", "mkt=tr-TR")]
        public void UlkeSecimi_DogruMarketUrlOlusturur(string ulkeAdi, string beklenenMarket)
        {
            var ulke = new Ulke(ulkeAdi);
            Assert.Contains(beklenenMarket, ulke.BingWebAdresi);
        }
    }
}