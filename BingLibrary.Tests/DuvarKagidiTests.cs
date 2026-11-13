using System;
using System.IO;
using Xunit;

namespace BingLibrary.Tests
{
    public class DuvarKagidiTests : IDisposable
    {
        private readonly string _geciciDosya;

        public DuvarKagidiTests()
        {
            _geciciDosya = Path.Combine(Path.GetTempPath(), $"wallpaper_{Guid.NewGuid():N}.jpg");
            File.WriteAllBytes(_geciciDosya, new byte[] { 0xFF, 0xD8, 0xFF, 0xD9 });
        }

        public void Dispose()
        {
            if (File.Exists(_geciciDosya))
                File.Delete(_geciciDosya);
        }

        [Fact]
        public void DosyaYoksa_KayitDefteriVeUygulayiciCagrilmadanHataFirlatir()
        {
            var kayitDefteri = new SahteKayitDefteri();
            var uygulayici = new SahteUygulayici();
            var duvarKagidi = new DuvarKagidi(kayitDefteri, uygulayici);

            string olmayanYol = Path.Combine(Path.GetTempPath(), $"yok_{Guid.NewGuid():N}.jpg");

            Assert.Throws<FileNotFoundException>(() => duvarKagidi.Olustur(olmayanYol, DuvarKagidi.EkranKonumu.Uzat));
            Assert.Equal(0, kayitDefteri.YazmaSayisi);
            Assert.Equal(0, uygulayici.CagriSayisi);
        }

        [Fact]
        public void BosDosyaYolu_ArgumentExceptionFirlatir()
        {
            var duvarKagidi = new DuvarKagidi(new SahteKayitDefteri(), new SahteUygulayici());

            Assert.Throws<ArgumentException>(() => duvarKagidi.Olustur("   ", DuvarKagidi.EkranKonumu.Merkezi));
        }

        [Theory]
        [InlineData(DuvarKagidi.EkranKonumu.Uzat, "2", "0")]
        [InlineData(DuvarKagidi.EkranKonumu.Merkezi, "1", "0")]
        [InlineData(DuvarKagidi.EkranKonumu.Dose, "1", "1")]
        public void GecerliDosya_DogruRegistryDegerleriniYazarVeTamYoluIletir(
            DuvarKagidi.EkranKonumu konum,
            string beklenenStyle,
            string beklenenTile)
        {
            var kayitDefteri = new SahteKayitDefteri();
            var uygulayici = new SahteUygulayici();
            var duvarKagidi = new DuvarKagidi(kayitDefteri, uygulayici);

            duvarKagidi.Olustur(_geciciDosya, konum);

            Assert.Equal(beklenenStyle, kayitDefteri.SonStyle);
            Assert.Equal(beklenenTile, kayitDefteri.SonTile);
            Assert.Equal(Path.GetFullPath(_geciciDosya), uygulayici.SonYol);
        }

        [Fact]
        public void Win32ApiSifirDonerse_Win32ExceptionFirlatirVeEskiAyariGeriYukler()
        {
            var kayitDefteri = new SahteKayitDefteri { OkunanStyle = "0", OkunanTile = "0" };
            var uygulayici = new SahteUygulayici { Donus = 0 };
            var duvarKagidi = new DuvarKagidi(kayitDefteri, uygulayici);

            var ex = Assert.Throws<System.ComponentModel.Win32Exception>(() =>
                duvarKagidi.Olustur(_geciciDosya, DuvarKagidi.EkranKonumu.Uzat));

            Assert.Contains("duvar kağıdı uygulanamadı", ex.Message, StringComparison.OrdinalIgnoreCase);
            Assert.Equal("0", kayitDefteri.SonStyle);
            Assert.Equal("0", kayitDefteri.SonTile);
        }

        private sealed class SahteKayitDefteri : IDuvarKagidiKayitDefteri
        {
            public string? OkunanStyle { get; init; } = "0";
            public string? OkunanTile { get; init; } = "0";
            public int YazmaSayisi { get; private set; }
            public string? SonStyle { get; private set; }
            public string? SonTile { get; private set; }

            public (string? Style, string? Tile) Oku() => (OkunanStyle, OkunanTile);

            public void Yaz(string wallpaperStyle, string tileWallpaper)
            {
                YazmaSayisi++;
                SonStyle = wallpaperStyle;
                SonTile = tileWallpaper;
            }
        }

        private sealed class SahteUygulayici : IDuvarKagidiUygulayici
        {
            public int Donus { get; init; } = 1;
            public int CagriSayisi { get; private set; }
            public string? SonYol { get; private set; }

            public int Uygula(string tamDosyaYolu)
            {
                CagriSayisi++;
                SonYol = tamDosyaYolu;
                return Donus;
            }
        }
    }
}
