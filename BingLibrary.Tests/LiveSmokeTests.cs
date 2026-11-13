using System.Threading.Tasks;
using BingLibrary;
using Xunit;

namespace BingLibrary.Tests
{
    public class LiveSmokeTests
    {
        [Fact]
        [Trait("Category", "LiveSmoke")]
        public async Task CanliBingServisi_GecerliGorselAdresiDondurur()
        {
            var gorsel = new Gorsel();
            await gorsel.WebSayfasiniIndirAsync("_1920x1080.jpg", "Turkiye");

            Assert.StartsWith("https://www.bing.com/", gorsel.Adres);
            Assert.False(string.IsNullOrWhiteSpace(gorsel.Bilgi));
        }
    }
}