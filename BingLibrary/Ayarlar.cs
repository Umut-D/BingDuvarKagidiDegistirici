using System;
using System.IO;
using System.Text.Json;

namespace BingLibrary
{
    public sealed class KullaniciAyarlari
    {
        public string SeciliUlke { get; set; } = "Turkiye";
        public int UlkeIndeksi { get; set; } = 9;
    }

    public class Ayarlar : IAyarDeposu
    {
        private static readonly JsonSerializerOptions JsonAyarlari = new()
        {
            WriteIndented = true
        };

        public string AyarDosyaYolu { get; }

        public Ayarlar()
            : this(Path.Combine(Dosya.VarsayilanVeriDizini(), "settings.json"))
        {
        }

        // Birim testlerinde geçici dosya yolu verebilmek için
        public Ayarlar(string ayarDosyaYolu)
        {
            if (string.IsNullOrWhiteSpace(ayarDosyaYolu))
                throw new ArgumentException("Ayar dosya yolu boş olamaz.", nameof(ayarDosyaYolu));

            AyarDosyaYolu = ayarDosyaYolu;
        }

        public KullaniciAyarlari Yukle()
        {
            try
            {
                if (!File.Exists(AyarDosyaYolu))
                    return new KullaniciAyarlari();

                string json = File.ReadAllText(AyarDosyaYolu);
                if (string.IsNullOrWhiteSpace(json))
                    return new KullaniciAyarlari();

                KullaniciAyarlari? okunan = JsonSerializer.Deserialize<KullaniciAyarlari>(json, JsonAyarlari);
                if (okunan == null)
                    return new KullaniciAyarlari();

                if (string.IsNullOrWhiteSpace(okunan.SeciliUlke))
                    okunan.SeciliUlke = "Turkiye";

                if (okunan.UlkeIndeksi < 0)
                    okunan.UlkeIndeksi = 0;

                return okunan;
            }
            catch
            {
                // Bozuk JSON dosyası uygulamayı kilitlemez; güvenli varsayılana döner
                return new KullaniciAyarlari();
            }
        }

        public void Kaydet(KullaniciAyarlari ayarlar)
        {
            ArgumentNullException.ThrowIfNull(ayarlar);

            string? klasor = Path.GetDirectoryName(AyarDosyaYolu);
            if (!string.IsNullOrEmpty(klasor) && !Directory.Exists(klasor))
                Directory.CreateDirectory(klasor);

            string json = JsonSerializer.Serialize(ayarlar, JsonAyarlari);
            string geciciDosya = $"{AyarDosyaYolu}.{Guid.NewGuid():N}.tmp";

            try
            {
                File.WriteAllText(geciciDosya, json);
                File.Move(geciciDosya, AyarDosyaYolu, overwrite: true);
            }
            finally
            {
                if (File.Exists(geciciDosya))
                    File.Delete(geciciDosya);
            }
        }
    }
}