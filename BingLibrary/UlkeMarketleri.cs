using System;
using System.Collections.Generic;

namespace BingLibrary
{
    /// <summary>
    /// Ülke adından Bing market kodu bulan eşleme tablosu. URL oluşturma sorumluluğu yoktur.
    /// </summary>
    internal static class UlkeMarketleri
    {
        public const string VarsayilanMarket = "tr-TR";

        private static readonly Dictionary<string, string> Tablo = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Almanya", "de-DE" },
            { "Amerika", "en-US" },
            { "Avustralya", "en-AU" },
            { "Avusturalya", "en-AU" },
            { "Brezilya", "pt-BR" },
            { "Britanya", "en-GB" },
            { "Fransa", "fr-FR" },
            { "Hindistan", "en-IN" },
            { "Japonya", "ja-JP" },
            { "Kanada", "en-CA" },
            { "Turkiye", "tr-TR" },
            { "Türkiye", "tr-TR" },
            { "Yeni Zelanda", "en-NZ" }
        };

        public static string MarketKoduBul(string? ulkeAdi)
        {
            if (!string.IsNullOrWhiteSpace(ulkeAdi) && Tablo.TryGetValue(ulkeAdi, out string? market))
                return market;

            // Bilinmeyen veya boş değer gelirse varsayılan Türkiye marketi kullanılır
            return VarsayilanMarket;
        }
    }
}
