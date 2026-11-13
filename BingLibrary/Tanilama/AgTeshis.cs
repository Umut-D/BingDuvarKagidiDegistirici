using System.Net;
using System.Security.Authentication;

namespace BingLibrary
{
    public enum TeshisHataTuru
    {
        Yok,
        BaglantiHatasi,
        TlsSertifikaHatasi,
        ZamanAsimi,
        HttpDurumHatasi,
        BeklenmeyenIcerikTuru,
        BilinmeyenHata
    }

    public sealed class TeshisSonucu
    {
        public bool Basarili { get; init; }
        public TeshisHataTuru HataTuru { get; init; }
        public HttpStatusCode? DurumKodu { get; init; }
        public string? IcerikTuru { get; init; }
        public string? ExceptionTuru { get; init; }
        public string? Mesaj { get; init; }
        public string? IcHataMesaji { get; init; }
    }

    public sealed class AgTeshis
    {
        private readonly HttpMessageInvoker _invoker;
        private readonly TimeSpan _zamanAsimi;

        public AgTeshis(HttpMessageHandler? handler = null, TimeSpan? zamanAsimi = null)
        {
            // Sertifika doğrulaması kapatılmaz; işletim sisteminin varsayılan TLS politikası kullanılır.
            _invoker = handler != null
                ? new HttpMessageInvoker(handler, disposeHandler: false)
                : new HttpMessageInvoker(new SocketsHttpHandler(), disposeHandler: true);

            _zamanAsimi = zamanAsimi ?? TimeSpan.FromSeconds(10);
        }

        public async Task<TeshisSonucu> IstegiTeshisEtAsync(
            Uri hedefUri,
            string beklenenIcerikTuruOnEki = "application/json",
            CancellationToken cancellationToken = default)
        {
            using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutCts.CancelAfter(_zamanAsimi);

            try
            {
                using var istek = new HttpRequestMessage(HttpMethod.Get, hedefUri);
                using var yanit = await _invoker.SendAsync(istek, timeoutCts.Token).ConfigureAwait(false);

                string? icerikTuru = yanit.Content.Headers.ContentType?.MediaType;

                if (!yanit.IsSuccessStatusCode)
                {
                    return new TeshisSonucu
                    {
                        Basarili = false,
                        HataTuru = TeshisHataTuru.HttpDurumHatasi,
                        DurumKodu = yanit.StatusCode,
                        IcerikTuru = icerikTuru,
                        Mesaj = $"HTTP {(int)yanit.StatusCode} ({yanit.ReasonPhrase})"
                    };
                }

                if (!string.IsNullOrWhiteSpace(beklenenIcerikTuruOnEki) &&
                    (string.IsNullOrWhiteSpace(icerikTuru) ||
                     !icerikTuru.StartsWith(beklenenIcerikTuruOnEki, StringComparison.OrdinalIgnoreCase)))
                {
                    return new TeshisSonucu
                    {
                        Basarili = false,
                        HataTuru = TeshisHataTuru.BeklenmeyenIcerikTuru,
                        DurumKodu = yanit.StatusCode,
                        IcerikTuru = icerikTuru,
                        Mesaj = $"Beklenen içerik türü '{beklenenIcerikTuruOnEki}', gelen '{icerikTuru ?? "yok"}'."
                    };
                }

                return new TeshisSonucu
                {
                    Basarili = true,
                    HataTuru = TeshisHataTuru.Yok,
                    DurumKodu = yanit.StatusCode,
                    IcerikTuru = icerikTuru,
                    Mesaj = "İstek ve içerik türü doğrulandı."
                };
            }
            catch (OperationCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                return new TeshisSonucu
                {
                    Basarili = false,
                    HataTuru = TeshisHataTuru.ZamanAsimi,
                    ExceptionTuru = ex.GetType().FullName,
                    Mesaj = "İstek zaman aşımına uğradı.",
                    IcHataMesaji = ex.InnerException?.Message
                };
            }
            catch (HttpRequestException ex) when (TlsHatasiMi(ex))
            {
                return new TeshisSonucu
                {
                    Basarili = false,
                    HataTuru = TeshisHataTuru.TlsSertifikaHatasi,
                    ExceptionTuru = ex.GetType().FullName,
                    Mesaj = ex.Message,
                    IcHataMesaji = ex.InnerException?.Message
                };
            }
            catch (HttpRequestException ex)
            {
                return new TeshisSonucu
                {
                    Basarili = false,
                    HataTuru = TeshisHataTuru.BaglantiHatasi,
                    ExceptionTuru = ex.GetType().FullName,
                    Mesaj = ex.Message,
                    IcHataMesaji = ex.InnerException?.Message
                };
            }
            catch (Exception ex)
            {
                return new TeshisSonucu
                {
                    Basarili = false,
                    HataTuru = TeshisHataTuru.BilinmeyenHata,
                    ExceptionTuru = ex.GetType().FullName,
                    Mesaj = ex.Message,
                    IcHataMesaji = ex.InnerException?.Message
                };
            }
        }

        private static bool TlsHatasiMi(Exception ex)
        {
            for (Exception? mevcut = ex; mevcut != null; mevcut = mevcut.InnerException)
            {
                if (mevcut is AuthenticationException)
                    return true;
            }

            return false;
        }
    }
}