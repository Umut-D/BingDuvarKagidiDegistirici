using System;
using System.Drawing;
using System.IO;

namespace BingLibrary
{
    /// <summary>
    /// Diskteki görsel dosyasını bellekte bağımsız bir <see cref="Bitmap"/> olarak yükler.
    /// </summary>
    public static class GorselYukleyici
    {
        public static Image Yukle(Dosya dosya)
        {
            ArgumentNullException.ThrowIfNull(dosya);

            byte[] gorselBaytlari = File.ReadAllBytes(dosya.DosyaYoluVeAdi);
            using var bellekAkisi = new MemoryStream(gorselBaytlari);
            using var geciciGorsel = Image.FromStream(bellekAkisi);

            return new Bitmap(geciciGorsel);
        }
    }
}
