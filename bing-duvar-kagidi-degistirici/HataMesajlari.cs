using System;
using System.Windows.Forms;

namespace bing_duvar_kagidi_degistirici
{
    internal class HataMesajlari
    {
        // Bağlantı hatası oluştuğunda verilecek hata mesajı
        public static void BaglantiHatasiMesaji(string baglantiHatasi)
        {
            MessageBox.Show(@"İnternet bağlatınız yok galiba. Öyle diyor malum hata mesajı: " + Environment.NewLine + baglantiHatasi, @"Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Bağlantı hatası oluştuğunda verilecek hata mesajı
        public static void GenelHataMesaji(string genelHata)
        {
            MessageBox.Show(@"Bazı sorunlar oldu galiba. Bu saatten sonra program iflah olmayabilir. Şöyle bir hata var: " + Environment.NewLine + genelHata, @"Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}