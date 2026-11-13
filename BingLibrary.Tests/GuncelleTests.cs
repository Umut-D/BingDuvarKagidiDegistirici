using System.Net;
using System.Text;
using Xunit;

namespace BingLibrary.Tests
{
    public class GuncelleTests
    {
        [Theory]
        [InlineData("3.0.0", "3.0.0")]
        [InlineData("3.0.0", "2.9.9")]
        [InlineData("2.10", "2.9")]
        public async Task SunucuSurumuAyniVeyaEskiIse_ProgramGuncelDoner(string mevcutSurum, string sunucuSurumu)
        {
            var handler = XmlYanitHandler($"<?xml version=\"1.0\" encoding=\"utf-8\"?><bing version=\"{sunucuSurumu}\" />");

            var guncelle = new Guncelle(
                baglanti: null,
                new HttpClient(handler),
                Version.Parse(mevcutSurum));

            GuncellemeSonucu sonuc = await guncelle.VersiyonKontroluYapAsync();

            Assert.Equal(GuncellemeDurumu.Guncel, sonuc.Durum);
            Assert.Null(sonuc.IndirmeAdresi);
        }

        [Fact]
        public async Task SunucuSurumuDahaYeniIse_IndirmeAdresiniDoner()
        {
            var handler = XmlYanitHandler("<?xml version=\"1.0\" encoding=\"utf-8\"?><bing version=\"3.1.0\" />");

            var guncelle = new Guncelle(
                baglanti: null,
                new HttpClient(handler),
                new Version(3, 0, 0));

            GuncellemeSonucu sonuc = await guncelle.VersiyonKontroluYapAsync();

            Assert.Equal(GuncellemeDurumu.YeniSurumMevcut, sonuc.Durum);
            Assert.Equal("3.1.0", sonuc.SunucuSurumu);
            Assert.Equal(Guncelle.VarsayilanProgramSayfasiLinki, sonuc.IndirmeAdresi);
        }

        [Fact]
        public async Task AgHatasiVeyaBozukXml_UygulamayiCokertmedenHataDurumuDoner()
        {
            var handler = new SahteHttpHandler((_, _) =>
                throw new HttpRequestException("Sunucuya erişilemedi."));

            var guncelle = new Guncelle(
                baglanti: null,
                new HttpClient(handler),
                new Version(3, 0, 0));

            GuncellemeSonucu sonuc = await guncelle.VersiyonKontroluYapAsync();

            Assert.Equal(GuncellemeDurumu.HataOlustu, sonuc.Durum);
            Assert.False(string.IsNullOrWhiteSpace(sonuc.Mesaj));
        }

        [Fact]
        public async Task DtdIcerenZararliXml_Engellenir()
        {
            string zararliXml = "<!DOCTYPE bing [<!ENTITY test 'deneme'>]><bing version=\"3.1.0\" />";
            var handler = XmlYanitHandler(zararliXml);

            var guncelle = new Guncelle(
                baglanti: null,
                new HttpClient(handler),
                new Version(3, 0, 0));

            GuncellemeSonucu sonuc = await guncelle.VersiyonKontroluYapAsync();

            Assert.Equal(GuncellemeDurumu.HataOlustu, sonuc.Durum);
        }

        private static SahteHttpHandler XmlYanitHandler(string xmlIcerik)
        {
            return new SahteHttpHandler((_, _) =>
            {
                var yanit = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(xmlIcerik, Encoding.UTF8, "application/xml")
                };
                return Task.FromResult(yanit);
            });
        }

        private sealed class SahteHttpHandler : HttpMessageHandler
        {
            private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _yanitUretici;

            public SahteHttpHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> yanitUretici)
            {
                _yanitUretici = yanitUretici;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
                => _yanitUretici(request, cancellationToken);
        }
    }
}