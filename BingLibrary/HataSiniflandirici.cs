using System;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace BingLibrary
{
    /// <summary>
    /// Bir istisnayı uygulama aşamasına sınıflandırır ve kullanıcıya gösterilecek mesajı üretir.
    /// </summary>
    public static class HataSiniflandirici
    {
        public static HataAsamasi Siniflandir(Exception ex)
        {
            return ex switch
            {
                TaskCanceledException or OperationCanceledException => HataAsamasi.ZamanAsimi,
                HttpRequestException httpEx when httpEx.StatusCode.HasValue => HataAsamasi.HttpDurumKodu,
                HttpRequestException => HataAsamasi.Baglanti,
                JsonException => HataAsamasi.JsonSema,
                Win32Exception => HataAsamasi.WindowsApi,
                IOException or UnauthorizedAccessException => HataAsamasi.DosyaSistemi,
                ArgumentException when ex.Source == "System.Drawing.Common" => HataAsamasi.GorselDecode,
                _ => HataAsamasi.Bilinmeyen
            };
        }

        public static string KullaniciMesajiOlustur(Exception ex, HataAsamasi? zorunluAsama = null)
        {
            HataAsamasi asama = zorunluAsama ?? Siniflandir(ex);
            return asama switch
            {
                HataAsamasi.Baglanti => "Bing sunucusuna bağlanılamadı. İnternet bağlantınızı kontrol edin.",
                HataAsamasi.ZamanAsimi => "Sunucu yanıt vermedi (zaman aşımı). Daha sonra tekrar deneyin.",
                HataAsamasi.HttpDurumKodu => "Bing sunucusu isteği reddetti veya geçici bir sunucu hatası döndürdü.",
                HataAsamasi.JsonSema => "Bing servisinden gelen veri biçimi beklenenden farklı.",
                HataAsamasi.DosyaSistemi => "Görsel dosyası diske yazılamadı veya okunamadı.",
                HataAsamasi.GorselDecode => "İndirilen görsel dosyası bozuk veya açılamıyor.",
                HataAsamasi.WindowsApi => "Windows masaüstü duvar kağıdı ayarı uygulanamadı.",
                HataAsamasi.Ayarlar => "Kullanıcı ayarları okunamadı; varsayılan ayarlar yüklendi.",
                _ => "İşlem sırasında beklenmeyen bir hata oluştu."
            };
        }
    }
}
