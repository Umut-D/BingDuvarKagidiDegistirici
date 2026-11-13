using System.Drawing.Imaging;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace BingLibrary.Tests
{
    public class DosyaTests : IDisposable
    {
        private readonly string _geciciKokDizin;

        public DosyaTests()
        {
            _geciciKokDizin = Path.Combine(Path.GetTempPath(), "BingTest_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_geciciKokDizin);
        }

        public void Dispose()
        {
            if (Directory.Exists(_geciciKokDizin))
                Directory.Delete(_geciciKokDizin, recursive: true);
        }

        [Fact]
        public async Task GecerliJpegIndirme_JpgUzantisiIleKaydeder()
        {
            byte[] ornekJpeg = OrnekJpegBaytlariUret();
            var handler = new SahteHttpHandler((_, _) =>
            {
                var yanit = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(ornekJpeg)
                };
                yanit.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                return Task.FromResult(yanit);
            });

            var gorsel = new Gorsel { Adres = "https://www.bing.com/th?id=OHR.Test_1920x1080.jpg" };
            var dosya = new Dosya(gorsel, _geciciKokDizin, new HttpClient(handler));

            await dosya.KaydetAsync();

            Assert.EndsWith(".jpg", dosya.DosyaYoluVeAdi, StringComparison.OrdinalIgnoreCase);
            Assert.True(File.Exists(dosya.DosyaYoluVeAdi));
            Assert.Equal(ornekJpeg.Length, new FileInfo(dosya.DosyaYoluVeAdi).Length);
        }

        [Fact]
        public async Task BozukIndirme_MevcutGecerliDosyayiSilmez()
        {
            byte[] ornekJpeg = OrnekJpegBaytlariUret();
            int istekSayisi = 0;

            var handler = new SahteHttpHandler((_, _) =>
            {
                istekSayisi++;
                if (istekSayisi == 1)
                {
                    var gecerliYanit = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(ornekJpeg)
                    };
                    gecerliYanit.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    return Task.FromResult(gecerliYanit);
                }

                var bozukYanit = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("bu bir resim degil", Encoding.UTF8, "text/html")
                };
                return Task.FromResult(bozukYanit);
            });

            var gorsel = new Gorsel { Adres = "https://www.bing.com/th?id=OHR.Test_1920x1080.jpg" };
            var dosya = new Dosya(gorsel, _geciciKokDizin, new HttpClient(handler));

            await dosya.KaydetAsync();
            await Assert.ThrowsAsync<InvalidOperationException>(() => dosya.KaydetAsync(zorlaYenidenIndir: true));

            Assert.True(File.Exists(dosya.DosyaYoluVeAdi));
            Assert.Equal(ornekJpeg.Length, new FileInfo(dosya.DosyaYoluVeAdi).Length);
        }

        [Fact]
        public async Task AyniGunGecerliDosyaVarsa_TekrarAgIstegiAtmaz()
        {
            byte[] ornekJpeg = OrnekJpegBaytlariUret();
            int agCagrisiSayisi = 0;

            var handler = new SahteHttpHandler((_, _) =>
            {
                agCagrisiSayisi++;
                var yanit = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(ornekJpeg)
                };
                yanit.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                return Task.FromResult(yanit);
            });

            var gorsel = new Gorsel { Adres = "https://www.bing.com/th?id=OHR.Test_1920x1080.jpg" };
            var dosya = new Dosya(gorsel, _geciciKokDizin, new HttpClient(handler));

            await dosya.KaydetAsync();
            await dosya.KaydetAsync();

            Assert.Equal(1, agCagrisiSayisi);
        }

        private static byte[] OrnekJpegBaytlariUret()
        {
            using var bmp = new Bitmap(16, 16);
            using var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
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