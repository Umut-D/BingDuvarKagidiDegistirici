using System;
using System.Text.Json;

namespace BingLibrary
{
    public class Link
    {
        private readonly Gorsel _gorsel;

        public Link(Gorsel gorsel)
        {
            _gorsel = gorsel;
        }

        public void DuzenlenmisLink(string cozunurluk, string jsonYaniti)
        {
            if (string.IsNullOrWhiteSpace(jsonYaniti))
                throw new InvalidOperationException("Bing sunucusundan boş yanıt alındı.");

            using JsonDocument belge = JsonDocument.Parse(jsonYaniti);

            if (!belge.RootElement.TryGetProperty("images", out JsonElement gorseller) ||
                gorseller.ValueKind != JsonValueKind.Array ||
                gorseller.GetArrayLength() == 0)
            {
                throw new InvalidOperationException("Bing JSON yanıtında 'images' verisi bulunamadı.");
            }

            JsonElement ilkGorsel = gorseller[0];

            string? urlBase = ilkGorsel.TryGetProperty("urlbase", out JsonElement urlBaseEl)
                ? urlBaseEl.GetString()
                : null;

            string? url = ilkGorsel.TryGetProperty("url", out JsonElement urlEl)
                ? urlEl.GetString()
                : null;

            string secilenCozunurluk = string.IsNullOrWhiteSpace(cozunurluk) ? "_1920x1080.jpg" : cozunurluk;

            if (!string.IsNullOrWhiteSpace(urlBase))
            {
                _gorsel.Adres = $"https://www.bing.com{urlBase}{secilenCozunurluk}";
            }
            else if (!string.IsNullOrWhiteSpace(url))
            {
                _gorsel.Adres = url.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                    ? url
                    : $"https://www.bing.com{url}";
            }
            else
            {
                throw new InvalidOperationException("Bing JSON yanıtında görsel URL bilgisi eksik.");
            }

            string title = ilkGorsel.TryGetProperty("title", out JsonElement titleEl)
                ? titleEl.GetString() ?? string.Empty
                : string.Empty;

            string copyright = ilkGorsel.TryGetProperty("copyright", out JsonElement copyrightEl)
                ? copyrightEl.GetString() ?? string.Empty
                : string.Empty;

            _gorsel.Bilgi = string.IsNullOrWhiteSpace(title)
                ? copyright
                : $"{title} — {copyright}".Trim(' ', '—');
        }
    }
}