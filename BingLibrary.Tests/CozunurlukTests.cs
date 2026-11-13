using Xunit;

namespace BingLibrary.Tests
{
    public class CozunurlukTests
    {
        [Theory]
        [InlineData(1366, 768, "_1366x768.jpg")]
        [InlineData(1920, 1080, "_1920x1080.jpg")]
        [InlineData(1920, 1200, "_1920x1200.jpg")]
        [InlineData(2560, 1440, "_UHD.jpg")]
        [InlineData(3440, 1440, "_UHD.jpg")]
        [InlineData(3840, 2160, "_UHD.jpg")]
        public void BilinenCozunurlukler_DogruEkiSecer(int genislik, int yukseklik, string beklenenEk)
        {
            var cozunurluk = new Cozunurluk(new Rectangle(0, 0, genislik, yukseklik));

            Assert.Equal(beklenenEk, cozunurluk.EkranCozunurlukEki);
        }

        [Fact]
        public void AyniGenislikFarkliYukseklik_16_9_Ve_16_10_AyirtEdilir()
        {
            var fullHd = new Cozunurluk(new Rectangle(0, 0, 1920, 1080));
            var wuxga = new Cozunurluk(new Rectangle(0, 0, 1920, 1200));

            Assert.NotEqual(fullHd.EkranCozunurlukEki, wuxga.EkranCozunurlukEki);
            Assert.Equal("_1920x1080.jpg", fullHd.EkranCozunurlukEki);
            Assert.Equal("_1920x1200.jpg", wuxga.EkranCozunurlukEki);
        }

        [Fact]
        public void EkranNullVeyaSifirGelirse_VarsayilanCozunurlugeDoner()
        {
            var nullEkran = new Cozunurluk(null);
            var bosEkran = new Cozunurluk(Rectangle.Empty);

            Assert.False(string.IsNullOrWhiteSpace(nullEkran.EkranCozunurlukEki));
            Assert.Equal(Cozunurluk.VarsayilanCozunurlukEki, nullEkran.EkranCozunurlukEki);
            Assert.Equal(Cozunurluk.VarsayilanCozunurlukEki, bosEkran.EkranCozunurlukEki);
        }

        [Fact]
        public void BilinmeyenAraCozunurluk_EnBoyOraninaGoreGuvenliFallbackVerir()
        {
            var onAltiOn = new Cozunurluk(new Rectangle(0, 0, 1680, 1050));
            var ozelGenis = new Cozunurluk(new Rectangle(0, 0, 1600, 900));

            Assert.Equal("_1920x1200.jpg", onAltiOn.EkranCozunurlukEki);
            Assert.Equal("_1920x1080.jpg", ozelGenis.EkranCozunurlukEki);
        }
    }
}