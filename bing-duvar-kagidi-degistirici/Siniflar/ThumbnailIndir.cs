using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BingDuvarKagidi.Siniflar
{
    // Farklı Bing web sitelerindeki görsellerin önizleme görüntülerini indirmek için bu sınıf kullanılıyor
    public class ThumbnailIndir : DuvarKagidiIndir
    {
        // Sınıflar
        private readonly WebClient _webIstemicisi = new WebClient();

        public void ThumbnailIndirveYukle(PictureBox pbox, string secilenUlke)
        {
            // 1. HTML sayfa kodunu (seçilen web sitesine göre) indir
            HtmlOku = _webIstemicisi.DownloadString(secilenUlke);

            // 2. HTML'den duvar kağıdı linkini ayıkla 
            EslestirilenLink[0] = Regex.Match(HtmlOku, "(\\S+?)\\.(jpg)");

            // 3. Gelen düzenli ifadeye gerekli eklemeleri yaparak tam linki oluştur
            DuzenliHtmlLink = EslestirilenLink[0].Groups[1].ToString().Substring(1).Replace(@"\", "");
            DuzenliHtmlLink = BingWebAdresi + DuzenliHtmlLink.Substring(0, DuzenliHtmlLink.LastIndexOf("_", StringComparison.Ordinal)) +
                              "_640x480.jpg";

            // 4. Düzenlenmiş resmi HTML linkine göre istem yap
            WebRequest webIstemi = WebRequest.Create(DuzenliHtmlLink);

            // 5. İstem sonucu dönen değeri akış haline çevir
            WebResponse webDonenDeger = webIstemi.GetResponse();
            Stream gelenAkis = webDonenDeger.GetResponseStream();

            // Gelen akışta sorun yoksa görseli belirlenen boyuta (16:9) dönüştür  
            if (gelenAkis != null)
            {
                Image gorsel = Image.FromStream(gelenAkis);
                Image thumbnail = gorsel.GetThumbnailImage(200, 113, () => false, IntPtr.Zero);
                pbox.Image = thumbnail;
            }
        }
    }
}