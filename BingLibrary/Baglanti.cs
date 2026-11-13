using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BingLibrary
{
    public class Baglanti : IInternetDenetleyici
    {
        internal static readonly HttpClient PaylasilanHttpIstemcisi = new(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(5)
        })
        {
            Timeout = TimeSpan.FromSeconds(15)
        };

        private static readonly Uri BingKontrolAdresi =
            new("https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=tr-TR");

        public string? SonHataMesaji { get; private set; }

        // Kullanıcı mesajını çıkarmak arayüz katmanının sorumluluğudur; hata metni SonHataMesaji'nde tutulur.
        public async Task<bool> InternetVarMiAsync(CancellationToken cancellationToken = default)
        {
            SonHataMesaji = null;

            try
            {
                using var istek = new HttpRequestMessage(HttpMethod.Get, BingKontrolAdresi);
                using var yanit = await PaylasilanHttpIstemcisi
                    .SendAsync(istek, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);

                if (yanit.IsSuccessStatusCode)
                    return true;

                SonHataMesaji = $"Bing servisine ulaşılamadı (HTTP {(int)yanit.StatusCode}).";
                return false;
            }
            catch (Exception ex)
            {
                SonHataMesaji = $"İnternet bağlantınız aktif değil veya Bing sunucusuna erişilemiyor. ({ex.Message})";
                return false;
            }
        }
    }
}