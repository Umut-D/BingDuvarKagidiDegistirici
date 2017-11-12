using System;
using System.Windows.Forms;

namespace bing_duvar_kagidi_degistirici.Siniflar
{
    internal class HataMesajlari
    {
        // Bağlantı hatası oluştuğunda verilecek hata mesajı
        public static void BaglantiHatasiMesaji(string baglantiHatasi)
        {
            MessageBox.Show(@"İnternet bağlatınız yok galiba. Öyle diyor malum hata mesajı: " + Environment.NewLine + baglantiHatasi, @"Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        // Genel bir hata oluştuğunda verilecek hata mesajı
        public static void GenelHataMesaji(string genelHata)
        {
            MessageBox.Show(@"Bazı sorunlar oldu galiba. Bu saatten sonra program iflah olmayabilir. Şöyle bir hata var: " + Environment.NewLine + genelHata, @"Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // İndirilen bir görsel varsa ve açıksa üzerine yazma konusunda yaşanacak sıkıntı için verilecek hata mesajı
        public static void GorselUzerineYazmaHataMesaji(string gorselHatasi)
        {
            MessageBox.Show(@"Bu görseli indirmiştiniz. Tekrar indirip üzerine yazmanıza gerek yok bence. Ayarlar alanından ve sağ üst köşedeki ülke alanından başka bir ülke seçmenizi öneririm. Hem değişiklik olur.", @"Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}