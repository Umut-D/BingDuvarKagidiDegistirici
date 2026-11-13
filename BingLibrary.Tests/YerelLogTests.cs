using System;
using System.IO;
using System.Net;
using System.Net.Http;
using BingLibrary;
using Xunit;

namespace BingLibrary.Tests
{
    public class YerelLogTests : IDisposable
    {
        private readonly string _geciciLogDizini;

        public YerelLogTests()
        {
            _geciciLogDizini = Path.Combine(Path.GetTempPath(), "BingLogTest_" + Guid.NewGuid().ToString("N"));
        }

        public void Dispose()
        {
            if (Directory.Exists(_geciciLogDizini))
                Directory.Delete(_geciciLogDizini, recursive: true);
        }

        [Fact]
        public void HataYaz_DosyaOlustururVeAsamayiKaydeder()
        {
            var log = new YerelLog(_geciciLogDizini);
            var httpEx = new HttpRequestException("Sunucu hatası", null, HttpStatusCode.ServiceUnavailable);

            log.HataYaz("TestIslem", httpEx);

            string[] dosyalar = Directory.GetFiles(_geciciLogDizini, "bing-*.log");
            Assert.Single(dosyalar);

            string icerik = File.ReadAllText(dosyalar[0]);
            Assert.Contains("ERROR/HttpDurumKodu", icerik);
            Assert.Contains("[TestIslem]", icerik);
        }

        [Fact]
        public void AzamiLogSayisiAsilinca_EskiDosyalarTemizlenir()
        {
            var log = new YerelLog(_geciciLogDizini);
            string uzunMetin = new('X', 200 * 1024);

            for (int i = 0; i < 15; i++)
            {
                log.BilgiYaz("RotasyonTesti", uzunMetin);
            }

            string[] dosyalar = Directory.GetFiles(_geciciLogDizini, "bing-*.log");
            Assert.True(dosyalar.Length <= 5);
        }
    }
}