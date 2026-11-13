using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BingLibrary;
using Xunit;

namespace BingLibrary.Tests
{
    public class ConnectionAndTlsDiagnosticsTests
    {
        private static readonly Uri TestUri =
            new("https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=tr-TR");

        [Fact]
        public async Task GecerliJsonYaniti_BasariliOlarakAyrilir()
        {
            var handler = new SahteHttpHandler((_, _) =>
            {
                var yanit = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"images\":[]}", Encoding.UTF8, "application/json")
                };
                return Task.FromResult(yanit);
            });

            var teshis = new AgTeshis(handler);
            var sonuc = await teshis.IstegiTeshisEtAsync(TestUri);

            Assert.True(sonuc.Basarili);
            Assert.Equal(TeshisHataTuru.Yok, sonuc.HataTuru);
            Assert.Equal(HttpStatusCode.OK, sonuc.DurumKodu);
        }

        [Fact]
        public async Task BaglantiHatasi_BaglantiHatasiOlarakSiniflandirilir()
        {
            var handler = new SahteHttpHandler((_, _) =>
                throw new HttpRequestException("Ana bilgisayar bulunamadı.", new SocketException((int)SocketError.HostNotFound)));

            var teshis = new AgTeshis(handler);
            var sonuc = await teshis.IstegiTeshisEtAsync(TestUri);

            Assert.False(sonuc.Basarili);
            Assert.Equal(TeshisHataTuru.BaglantiHatasi, sonuc.HataTuru);
            Assert.NotNull(sonuc.IcHataMesaji);
        }

        [Fact]
        public async Task GecersizSertifika_TlsSertifikaHatasiOlarakSiniflandirilir()
        {
            var handler = new SahteHttpHandler((_, _) =>
                throw new HttpRequestException(
                    "SSL bağlantısı kurulamadı.",
                    new AuthenticationException("Uzak sertifika doğrulama yordamına göre geçersiz.")));

            var teshis = new AgTeshis(handler);
            var sonuc = await teshis.IstegiTeshisEtAsync(TestUri);

            Assert.False(sonuc.Basarili);
            Assert.Equal(TeshisHataTuru.TlsSertifikaHatasi, sonuc.HataTuru);
            Assert.Contains("sertifika", sonuc.IcHataMesaji, StringComparison.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData((HttpStatusCode)429)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        public async Task HttpHataKodlari_HttpDurumHatasiOlarakSiniflandirilir(HttpStatusCode durumKodu)
        {
            var handler = new SahteHttpHandler((_, _) =>
                Task.FromResult(new HttpResponseMessage(durumKodu)));

            var teshis = new AgTeshis(handler);
            var sonuc = await teshis.IstegiTeshisEtAsync(TestUri);

            Assert.False(sonuc.Basarili);
            Assert.Equal(TeshisHataTuru.HttpDurumHatasi, sonuc.HataTuru);
            Assert.Equal(durumKodu, sonuc.DurumKodu);
        }

        [Fact]
        public async Task ZamanAsimi_ZamanAsimiOlarakSiniflandirilir()
        {
            var handler = new SahteHttpHandler(async (_, ct) =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5), ct);
                return new HttpResponseMessage(HttpStatusCode.OK);
            });

            var teshis = new AgTeshis(handler, zamanAsimi: TimeSpan.FromMilliseconds(50));
            var sonuc = await teshis.IstegiTeshisEtAsync(TestUri);

            Assert.False(sonuc.Basarili);
            Assert.Equal(TeshisHataTuru.ZamanAsimi, sonuc.HataTuru);
        }

        [Fact]
        public async Task BeklenmeyenIcerikTuru_AyriOlarakYakalanir()
        {
            var handler = new SahteHttpHandler((_, _) =>
            {
                var yanit = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("<html>Engellendi</html>", Encoding.UTF8)
                };
                yanit.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return Task.FromResult(yanit);
            });

            var teshis = new AgTeshis(handler);
            var sonuc = await teshis.IstegiTeshisEtAsync(TestUri, "application/json");

            Assert.False(sonuc.Basarili);
            Assert.Equal(TeshisHataTuru.BeklenmeyenIcerikTuru, sonuc.HataTuru);
            Assert.Equal("text/html", sonuc.IcerikTuru);
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