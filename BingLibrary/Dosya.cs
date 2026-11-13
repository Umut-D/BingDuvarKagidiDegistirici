using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BingLibrary
{
    public class Dosya
    {
        private readonly Gorsel _gorsel;
        private readonly string _kokDizin;
        private readonly HttpClient _httpIstemcisi;

        public string DosyaYoluVeAdi => Yol();

        public Dosya(Gorsel gorsel)
            : this(gorsel, VarsayilanVeriDizini(), Baglanti.PaylasilanHttpIstemcisi)
        {
        }

        // Birim testlerinde özel klasör ve sahte HttpClient verebilmek için
        public Dosya(Gorsel gorsel, string kokDizin, HttpClient? httpIstemcisi = null)
        {
            _gorsel = gorsel ?? throw new ArgumentNullException(nameof(gorsel));
            _kokDizin = string.IsNullOrWhiteSpace(kokDizin) ? VarsayilanVeriDizini() : kokDizin;
            _httpIstemcisi = httpIstemcisi ?? Baglanti.PaylasilanHttpIstemcisi;
        }

        public static string VarsayilanVeriDizini()
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(localAppData, "BingDuvarKagidiDegistirici");
        }

        // Dosya adı: tarih + görsel URL'sinin özeti. URL ülke, çözünürlük ve görsel kimliğini taşıdığı
        // için farklı ayarlar aynı gün içinde birbirinin dosyasını ezmez.
        private string Yol()
        {
            string klasorYolu = Path.Combine(_kokDizin, "images");
            string tarihBicemi = DateTime.Now.ToString("yyyy.MM.dd");
            string urlOzeti = Convert.ToHexString(
                SHA256.HashData(Encoding.UTF8.GetBytes(_gorsel.Adres)))[..12];

            return Path.Combine(klasorYolu, $"{tarihBicemi}_{urlOzeti}.jpg");
        }

        public async Task KaydetAsync(bool zorlaYenidenIndir = false, CancellationToken cancellationToken = default)
        {
            string hedefYol = DosyaYoluVeAdi;

            // Aynı günün geçerli dosyası zaten varsa tekrar indirme
            if (!zorlaYenidenIndir && GecerliGorselDosyasiMi(hedefYol))
                return;

            if (string.IsNullOrWhiteSpace(_gorsel.Adres))
                throw new InvalidOperationException("İndirilecek görsel adresi boş.");

            using var yanit = await _httpIstemcisi
                .GetAsync(_gorsel.Adres, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                .ConfigureAwait(false);

            yanit.EnsureSuccessStatusCode();

            string? icerikTuru = yanit.Content.Headers.ContentType?.MediaType;
            if (!string.IsNullOrWhiteSpace(icerikTuru) &&
                !icerikTuru.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Beklenen içerik türü görsel değil: '{icerikTuru}'.");
            }

            byte[] gorselBaytlari = await yanit.Content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
            if (gorselBaytlari.Length == 0)
                throw new InvalidOperationException("Sunucudan boş görsel verisi döndü.");

            // Verinin gerçekten bozuk olmayan bir görsel olduğunu doğrula
            using (var dogrulamaAkisi = new MemoryStream(gorselBaytlari))
            using (var _ = Image.FromStream(dogrulamaAkisi))
            {
            }

            Directory.CreateDirectory(Path.GetDirectoryName(hedefYol)!);

            string geciciYol = $"{hedefYol}.{Guid.NewGuid():N}.tmp";
            try
            {
                await File.WriteAllBytesAsync(geciciYol, gorselBaytlari, cancellationToken).ConfigureAwait(false);
                File.Move(geciciYol, hedefYol, overwrite: true);
            }
            finally
            {
                if (File.Exists(geciciYol))
                    File.Delete(geciciYol);
            }
        }

        private static bool GecerliGorselDosyasiMi(string dosyaYolu)
        {
            try
            {
                var bilgi = new FileInfo(dosyaYolu);
                if (!bilgi.Exists || bilgi.Length == 0)
                    return false;

                using var akis = File.OpenRead(dosyaYolu);
                using var _ = Image.FromStream(akis, useEmbeddedColorManagement: false, validateImageData: true);
                return true;
            }
            catch (Exception ex)
            {
                // Bozuk önbellek dosyası yeniden indirilecek; neden kayda geçirilir
                YerelLog.Varsayilan.HataYaz("GecerliGorselKontrol", ex, HataAsamasi.GorselDecode);
                return false;
            }
        }
    }
}