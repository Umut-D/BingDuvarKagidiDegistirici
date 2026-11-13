using System;
using System.IO;
using System.Linq;
using System.Text;

namespace BingLibrary
{
    public sealed class YerelLog
    {
        private const long AzamiDosyaBoyutuBayt = 512 * 1024; // 512 KB
        private const int AzamiLogDosyaSayisi = 5;
        private static readonly object Kilit = new();

        public static YerelLog Varsayilan { get; } = new(Path.Combine(Dosya.VarsayilanVeriDizini(), "logs"));

        public string LogDizini { get; }

        public YerelLog(string logDizini)
        {
            if (string.IsNullOrWhiteSpace(logDizini))
                throw new ArgumentException("Log dizini boş olamaz.", nameof(logDizini));

            LogDizini = logDizini;
        }

        public void BilgiYaz(string islem, string mesaj)
        {
            SatirYaz("INFO", islem, mesaj, exceptionDetayi: null);
        }

        public void HataYaz(string islem, Exception ex, HataAsamasi? zorunluAsama = null)
        {
            HataAsamasi asama = zorunluAsama ?? HataSiniflandirici.Siniflandir(ex);
            string detay = $"{ex.GetType().Name}: {ex.Message}" +
                           (ex.InnerException != null ? $" | İç Hata: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}" : string.Empty);

            SatirYaz($"ERROR/{asama}", islem, HataSiniflandirici.KullaniciMesajiOlustur(ex, asama), detay);
        }

        private void SatirYaz(string seviye, string islem, string mesaj, string? exceptionDetayi)
        {
            try
            {
                lock (Kilit)
                {
                    if (!Directory.Exists(LogDizini))
                        Directory.CreateDirectory(LogDizini);

                    string aktifDosya = AktifLogDosyasiniBelirle();
                    string zaman = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var satir = new StringBuilder();
                    satir.Append($"[{zaman}] [{seviye}] [{islem}] {mesaj}");
                    if (!string.IsNullOrWhiteSpace(exceptionDetayi))
                        satir.Append($" || {exceptionDetayi}");
                    satir.AppendLine();

                    File.AppendAllText(aktifDosya, satir.ToString(), Encoding.UTF8);
                    EskiLoglariTemizle();
                }
            }
            catch
            {
                // Loglama hatası ana uygulamanın çökmesine neden olmamalı
            }
        }

        private string AktifLogDosyasiniBelirle()
        {
            string gunlukDosya = Path.Combine(LogDizini, $"bing-{DateTime.Now:yyyy-MM-dd}.log");
            if (File.Exists(gunlukDosya) && new FileInfo(gunlukDosya).Length >= AzamiDosyaBoyutuBayt)
            {
                string arsivAdi = Path.Combine(LogDizini, $"bing-{DateTime.Now:yyyy-MM-dd_HHmmssfff}.log");
                File.Move(gunlukDosya, arsivAdi, overwrite: true);
            }

            return gunlukDosya;
        }

        private void EskiLoglariTemizle()
        {
            var dosyalar = new DirectoryInfo(LogDizini)
                .GetFiles("bing-*.log")
                .OrderByDescending(f => f.CreationTimeUtc)
                .Skip(AzamiLogDosyaSayisi);

            foreach (FileInfo eski in dosyalar)
            {
                eski.Delete();
            }
        }
    }
}
