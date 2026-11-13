using System.Threading;
using System.Threading.Tasks;

namespace BingLibrary
{
    public class Gorsel
    {
        public string Bilgi { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;

        public async Task WebSayfasiniIndirAsync(
            string cozunurluk,
            string ayarlardakiSeciliUlke,
            CancellationToken cancellationToken = default)
        {
            string jsonIcerik = await JsonYanitiAlAsync(ayarlardakiSeciliUlke, cancellationToken).ConfigureAwait(false);
            Link link = new Link(this);
            link.DuzenlenmisLink(cozunurluk, jsonIcerik);
        }

        // Bing'in JSON arşiv yanıtını döndürür (HTML değil)
        public async Task<string> JsonYanitiAlAsync(string seciliUlke, CancellationToken cancellationToken = default)
        {
            Ulke ulke = new Ulke(seciliUlke);
            using var yanit = await Baglanti.PaylasilanHttpIstemcisi
                .GetAsync(ulke.BingWebAdresi, cancellationToken)
                .ConfigureAwait(false);

            yanit.EnsureSuccessStatusCode();
            return await yanit.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}