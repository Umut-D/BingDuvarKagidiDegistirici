using System;
using System.Collections.Generic;
using System.Drawing;

namespace BingLibrary
{
    public class Cozunurluk
    {
        public const string VarsayilanCozunurlukEki = "_1920x1080.jpg";

        public string EkranCozunurlukEki { get; private set; } = VarsayilanCozunurlukEki;
        public Size SecilenEkranBoyutu { get; private set; }

        // Ekran sınırları UI katmanından (ör. Screen.PrimaryScreen?.Bounds) verilir; kütüphane WinForms'a bağlı değildir.
        public Cozunurluk(Rectangle? birincilEkranSinirlari)
        {
            if (birincilEkranSinirlari is { Width: > 0, Height: > 0 } sinirlar)
            {
                SecilenEkranBoyutu = sinirlar.Size;
                EkranCozunurlukEki = CozunurlukEkiSec(sinirlar.Width, sinirlar.Height);
            }
            else
            {
                SecilenEkranBoyutu = Size.Empty;
                EkranCozunurlukEki = VarsayilanCozunurlukEki;
            }
        }

        public static string CozunurlukEkiSec(int genislik, int yukseklik)
        {
            if (genislik <= 0 || yukseklik <= 0)
                return VarsayilanCozunurlukEki;

            // 1. Önce birebir (Genişlik x Yükseklik) eşleşmesine bak
            var tamOlculer = TamOlcuHaritasi();
            if (tamOlculer.TryGetValue((genislik, yukseklik), out string? tamEk))
                return tamEk;

            // 2. 2K / 4K / Ultrawide gibi yüksek çözünürlüklü ekranlar için UHD görsel seç
            if (genislik >= 2560 || yukseklik >= 1440)
                return "_UHD.jpg";

            // 3. En-boy oranına (16:10 vs 16:9 vs 4:3) göre en yakın güvenli geri dönüş
            double oran = (double)genislik / yukseklik;

            // 16:10 (1.60) ekranlar (örn. 1680x1050, 1440x900)
            if (Math.Abs(oran - 1.6) <= 0.04)
                return "_1920x1200.jpg";

            // 4:3 (1.33) veya 5:4 (1.25) kareye yakın ekranlar
            if (oran <= 1.4)
                return genislik <= 800 ? "_800x600.jpg" : "_1024x768.jpg";

            // 16:9 standart geniş ekranlar için güvenli geri dönüş
            return VarsayilanCozunurlukEki;
        }

        private static Dictionary<(int Genislik, int Yukseklik), string> TamOlcuHaritasi()
        {
            return new Dictionary<(int, int), string>
            {
                { (640, 480), "_640x480.jpg" },
                { (800, 600), "_800x600.jpg" },
                { (1024, 768), "_1024x768.jpg" },
                { (1280, 720), "_1280x720.jpg" },
                { (1366, 768), "_1366x768.jpg" },
                { (1920, 1080), "_1920x1080.jpg" },
                { (1920, 1200), "_1920x1200.jpg" },
                { (2560, 1440), "_UHD.jpg" },
                { (3440, 1440), "_UHD.jpg" },
                { (3840, 2160), "_UHD.jpg" }
            };
        }
    }
}