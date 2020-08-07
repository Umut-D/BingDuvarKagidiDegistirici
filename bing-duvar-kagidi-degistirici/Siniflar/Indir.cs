using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BingDuvarKagidi.Siniflar
{
    public class Indir : WebSiteleri
    {
        public string DuzenliBilgi;
        public string DosyaAdresi = Directory.GetCurrentDirectory() + "\\images\\" + DateTime.Now.ToString("MM.dd.yyyy") + ".bmp";

        // 1. HTML sayfa kodunu (seçilen web sitesine göre) indir
        private WebClient WebIstemici(out string htmlOku, string ada)
        {
            WebSiteleri webSiteleri = new WebSiteleri();
            string adres = webSiteleri.Adres(ada);

            WebClient webIstem = new WebClient();
            htmlOku = webIstem.DownloadString(adres);

            return webIstem;
        }

        // 2. HTML'den duvar kağıdı linkini ayıkla
        private Match[] LinkEsle(string htmlOku)
        {
            Match[] eslesenLink = new Match[1];
            eslesenLink[0] = Regex.Match(htmlOku, "(\\S+?)\\.(jpg)");
            return eslesenLink;
        }

        // 3. Gelen düzenli ifadeye gerekli eklemeleri yaparak tam linki oluştur ve HTML'den duvar kağıdı bilgisini ayıkla
        private string DuzenliBilgiler(string cozunurluk, Match[] eslestirilenLink, string htmlOku, out string gorselBilgi)
        {
            string html = eslestirilenLink[0].Groups[1].ToString().Replace("content=\"", "");
            html = html.Substring(0, html.LastIndexOf("_", StringComparison.Ordinal)) + cozunurluk;

            int bilgiBaslangic = htmlOku.LastIndexOf("\"Title\":", StringComparison.Ordinal);
            int bilgiBitis = htmlOku.LastIndexOf("\"SocialGood\"", StringComparison.Ordinal);
            gorselBilgi = htmlOku.Substring(bilgiBaslangic + 9, (bilgiBitis) - bilgiBaslangic - 11);

            return html;
        }

        // 4. Farklı karakterlerin kodlamasını düzelt (UTF8)
        private void KarakterKodlama(string duzenliBilgi)
        {
            byte[] bytes = Encoding.Default.GetBytes(duzenliBilgi.Replace("\",\"Copyright\":\"", " "));
            DuzenliBilgi = Encoding.UTF8.GetString(bytes);
        }

        // 5. Duvar kağıdını images klasörünün içine at, klasör yoksa oluştur ve images klasörünün içine indir
        private void KlasorOlustur(WebClient webIstemcisi, string duzenliHtmlLink)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "images"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "images");

            webIstemcisi.DownloadFileAsync(new Uri(duzenliHtmlLink), DosyaAdresi);
        }

        // 6. Tüm işlemleri toparla
        public void Yukle(string cozunurluk, string ayarlarSeciliUlke)
        {
            if (!Baglanti.Kontrol())
                throw new Exception();
            
            using (WebClient webIstemicisi = WebIstemici(out var htmlOku, ayarlarSeciliUlke))
            {
                Match[] eslesenLink = LinkEsle(htmlOku);

                string duzenliHtmlLink = DuzenliBilgiler(cozunurluk, eslesenLink, htmlOku, out var duzenliBilgi);
                KarakterKodlama(duzenliBilgi);

                KlasorOlustur(webIstemicisi, duzenliHtmlLink);
                webIstemicisi.DownloadFileCompleted += WebIstemicisi_DownloadFileCompleted;
            }
        }

        // 7. İndirme sonrası işlemler
        public void WebIstemicisi_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            FileStream dosyaAkis = new FileStream(DosyaAdresi, FileMode.OpenOrCreate, FileAccess.Read);

            if (Application.OpenForms["FrmBing"]?.Controls["pbox"] is PictureBox pbox) 
                pbox.Image = Image.FromStream(dosyaAkis);

            dosyaAkis.Close();
        }
    }
}