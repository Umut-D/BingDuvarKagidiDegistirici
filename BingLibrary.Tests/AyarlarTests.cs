using System;
using System.IO;
using BingLibrary;
using Xunit;

namespace BingLibrary.Tests
{
    public class AyarlarTests : IDisposable
    {
        private readonly string _geciciKlasor;
        private readonly string _ayarDosyasi;

        public AyarlarTests()
        {
            _geciciKlasor = Path.Combine(Path.GetTempPath(), "BingAyarTest_" + Guid.NewGuid().ToString("N"));
            _ayarDosyasi = Path.Combine(_geciciKlasor, "settings.json");
        }

        public void Dispose()
        {
            if (Directory.Exists(_geciciKlasor))
                Directory.Delete(_geciciKlasor, recursive: true);
        }

        [Fact]
        public void DosyaYoksa_VarsayilanAyarlariDoner()
        {
            var ayarlar = new Ayarlar(_ayarDosyasi);
            KullaniciAyarlari sonuc = ayarlar.Yukle();

            Assert.Equal("Turkiye", sonuc.SeciliUlke);
            Assert.Equal(9, sonuc.UlkeIndeksi);
        }

        [Fact]
        public void KaydedilenAyarlar_TekrarYuklendigindeKorunur()
        {
            var ayarlar = new Ayarlar(_ayarDosyasi);
            ayarlar.Kaydet(new KullaniciAyarlari
            {
                SeciliUlke = "Japonya",
                UlkeIndeksi = 7
            });

            KullaniciAyarlari yuklenen = ayarlar.Yukle();

            Assert.True(File.Exists(_ayarDosyasi));
            Assert.Equal("Japonya", yuklenen.SeciliUlke);
            Assert.Equal(7, yuklenen.UlkeIndeksi);
        }

        [Fact]
        public void BozukJsonDosyasi_UygulamayiCokertmedenVarsayilanaDoner()
        {
            Directory.CreateDirectory(_geciciKlasor);
            File.WriteAllText(_ayarDosyasi, "{ bozuk-json-icerigi !!!");

            var ayarlar = new Ayarlar(_ayarDosyasi);
            KullaniciAyarlari sonuc = ayarlar.Yukle();

            Assert.Equal("Turkiye", sonuc.SeciliUlke);
            Assert.Equal(9, sonuc.UlkeIndeksi);
        }
    }
}