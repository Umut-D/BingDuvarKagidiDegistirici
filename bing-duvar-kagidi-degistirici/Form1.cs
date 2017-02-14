using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace bing_duvar_kagidi_degistirici
{
    public partial class FrmBing : Form
    {
        public FrmBing()
        {
            InitializeComponent();
        }

        public const string BingWebAdresi = "http://www.bing.com";

        // Değişken, sınıf vs. oluşturma alanı
        private readonly CozunurlukBul _cozunurlukBul = new CozunurlukBul();
        private readonly XmlOkuYaz _xmlOkuYaz = new XmlOkuYaz();
        private string _htmlOku, _duzenliHtmlLink, _duzenliBilgi;
        private int _linkBaslangic, _linkBitis, _bilgiBaslangic, _bilgiBitis;
        public string EkranCozunurlugu, DuvarKagidiBilgisi;
        public string Todaysdate = DateTime.Now.ToString("MM-dd-yyyy");

        public string GuncellemeLinki = @"http://www.umutd.com/wp-content/program-versions/bing-duvar-kagidi-degistirici.xml";

        public string Versiyon = "2.0", Saat;
        public DateTime Tarih = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Pacific Standard Time"); 

        private void FrmBing_Load(object sender, EventArgs e)
        {
            // Açılışta zamanlayıcıyı başlat
            timer.Start();

            // XML Oku, yoksa XML oluştur
            _xmlOkuYaz.XmlDurumu();

            // Çözünürlük sınıfından gelen çözünürlük bilgisini al
            EkranCozunurlugu = _cozunurlukBul.Cozunurluk();

            // İnternet bağlantısı olup olmadığını kontrol etmek için ping at
            try
            {
                var pingTest = new Ping();
                var pingCevap = pingTest.Send(IPAddress.Parse("64.15.112.45"));

                if (pingCevap != null && pingCevap.Status == IPStatus.Success)
                {
                    tssDurum.Text = @"İnternet bağlantınız var. Duvar kağıdını indirebilirsiniz.";
                    tssDurum.ForeColor = Color.Green;
                }
                else
                {
                    tssDurum.Text = @"İnternet bağlantınız yok. Günün duvar kağıdını indiremezsiniz.";
                    tssDurum.ForeColor = Color.MediumVioletRed;
                }
            }
            catch (PingException baglantiHatasi)
            {
                HataMesajlari.BaglantiHatasiMesaji(baglantiHatasi.Message);
            }

            // Duvar kağıdı için günün tarihini at (Saat 11:00'e göre)

            if (Tarih > Convert.ToDateTime("00:00"))
            {
                Text += @"   [" + DateTime.Now.ToLongDateString() + @"]";
            }
            else
            {
                Text += @"   [" + DateTime.Now.AddDays(-1).ToLongDateString() + @"]";
            }

            tsmDegistir.Enabled = false;
        }

        private void tsmIndir_Click(object sender, EventArgs e)
        {
            try
            {
                pbox.InitialImage = null;

                // I. HTML sayfa kodunu indir
                var webIstemicisi = new WebClient();
                _htmlOku = webIstemicisi.DownloadString(BingWebAdresi);

                // II. HTML'den duvar kağıdı linkini ayıkla 
                _linkBaslangic = _htmlOku.IndexOf("g_img={url:", StringComparison.Ordinal) + "g_img={url:".Length;
                _linkBitis = _htmlOku.LastIndexOf(",id:'", StringComparison.Ordinal);

                _duzenliHtmlLink = BingWebAdresi +
                                   _htmlOku.Substring(_linkBaslangic + 2, (_linkBitis - 3) - _linkBaslangic);

                // III. HTML'den duvar kağıdı bilgisini ayıkla
                _bilgiBaslangic = _htmlOku.IndexOf("copyright", StringComparison.Ordinal) + "copyright=".Length;
                _bilgiBitis = _htmlOku.IndexOf("copyrightlink", StringComparison.Ordinal);

                _duzenliBilgi = _htmlOku.Substring(_bilgiBaslangic + 2, (_bilgiBitis) - _bilgiBaslangic - 5);

                // IV. Farklı karakterlerin kodlamasını düzelt
                var bytes = Encoding.Default.GetBytes(_duzenliBilgi);
                _duzenliBilgi = Encoding.UTF8.GetString(bytes);

                // V. Duvar kağıdını images klasörünün içine at, klasör yoksa oluştur
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "images"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "images");
                }

                // VI. Duvar kağıdını indir
                webIstemicisi.DownloadFile(_duzenliHtmlLink,
                    AppDomain.CurrentDomain.BaseDirectory + "images\\" + Tarih.ToShortDateString() + @" - Bing.bmp");

                // VII. Duvar kağıdını görüntüle
                pbox.InitialImage = null;
                pbox.Image =
                    new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "images\\" + Tarih.ToShortDateString() +
                               @" - Bing.bmp");

                webIstemicisi.Dispose();
            }
            catch (Exception genelHataMesaji)
            {
                HataMesajlari.GenelHataMesaji(genelHataMesaji.Message);
            }

            tsmIndir.Enabled = false;
            tsmDegistir.Enabled = true;
            tssDurum.Text = _duzenliBilgi;
            tssDurum.ForeColor = Color.Black;
        }

        private void FrmBing_Resize(object sender, EventArgs e)
        {
            // Form küçülüp büyütüldüğünde görüntülenecek mesajlar ve form durumu
            switch (WindowState)
            {
                case FormWindowState.Minimized:
                    notifyIcon.Visible = true;
                    notifyIcon.BalloonTipText = @"Program hala çalışıyor.";
                    notifyIcon.BalloonTipTitle = @"Bing Duvar Kağıdı Değiştirici";
                    notifyIcon.ShowBalloonTip(500);
                    Hide();
                    break;
                case FormWindowState.Normal:
                    notifyIcon.Visible = false;
                    break;
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            // Farenin sol tuşuna basıldığında formu göster
            if (e.Button == MouseButtons.Left)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
        }

        private void cmsGoster_Click(object sender, EventArgs e)
        {
            // Bağlam menüsü > Göster
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void cmsKapat_Click(object sender, EventArgs e)
        {
            // Bağlam menüsü > Kapat
            tsmKapat.PerformClick();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Mevcut ve XML dosyasından kaydedilen zamanları al
            Saat = DateTime.Now.ToString("HH:mm:ss");
            _xmlOkuYaz.XmlDurumu();

            if (_xmlOkuYaz.AyarlarOtomatikDegistir)
            {
                // Eğer mevcut saatle, kullanıcının girdiği saat aynı ise duvar kağıdını otomatik olarak değiştir
                if (Saat == _xmlOkuYaz.AyarlarGirilenSaat.ToLongTimeString())
                {
                    tsmIndir.PerformClick();
                    tsmDegistir.PerformClick();
                }
            }
        }

        private void tsmDegistir_Click(object sender, EventArgs e)
        {
            try
            {
                // Duvar kağıdını dizine indir ve değiştir
                var resimAdresi =
                    new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\" + Tarih.ToShortDateString() +
                            @" - Bing.bmp");
                DuvarKagidi.DuvarKagidiOlustur(resimAdresi, DuvarKagidi.Konumlandirma.Uzat);
            }
            catch (Exception genelHataMesaji)
            {
                HataMesajlari.GenelHataMesaji(genelHataMesaji.Message);
            }

            // Windows ekranında gerekli bilgilendirmeyi yap
            notifyIcon.BalloonTipText = @"[Duvar kağıdı başarıyla değiştirildi]" + Environment.NewLine + @"> " +
                                        _duzenliBilgi;
            notifyIcon.ShowBalloonTip(2000);
        }

        private void tsmAyarlar_Click(object sender, EventArgs e)
        {
            // Ayarlar alanına geçiş yap
            var frmAyarlar = new FrmAyarlar();
            frmAyarlar.ShowDialog();
        }

        private void tsmGuncelle_Click(object sender, EventArgs e)
        {
            // Programın güncel olup olmadığını denetle
            try
            {
                WebClient webIstemcisi = new WebClient();
                webIstemcisi.Headers.Add("user-agent", "MyRSSReader/1.0");
                XmlReader xmlOku = XmlReader.Create(webIstemcisi.OpenRead(GuncellemeLinki));

                while (xmlOku.Read())
                {
                    if ((xmlOku.NodeType == XmlNodeType.Element) && (xmlOku.Name == "bing"))
                    {
                        if (xmlOku.HasAttributes)
                        {
                            Versiyon = xmlOku.GetAttribute("version");

                            if (Versiyon == "2.0") //Versiyon güncellemelerinde bu alana dikkat!
                            {
                                MessageBox.Show(@"Program günceldir. Yeni versiyon çıkana kadar şimdilik en iyisi bu.",
                                    @"Güncelle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                var guncelleDialog =
                                    MessageBox.Show(
                                        @"Yeni bir güncelleme var. Evet evet, programı " +
                                        xmlOku.GetAttribute("version") +
                                        @" versiyonuna yükselttim. Yenilikler var. Web sayfasına girip indirmek ister misiniz?",
                                        @"Güncelle", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                                if (guncelleDialog == DialogResult.OK)
                                {
                                    Process.Start("http://www.umutd.com/programlar/bing-duvar-kagidi-degistirici");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception baglantiHatasi)
            {
                HataMesajlari.BaglantiHatasiMesaji(baglantiHatasi.Message);
            }
        }

        private void tsmBing_Click(object sender, EventArgs e)
        {
            // Bing web sitesini aç
            Process.Start("http://www.bing.com");
        }

        private void tsmHakkinda_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"Bu program ile Bing (Türkiye) arama motorunun web sayfasında her gün saat 11:00 itibariyle yenilenen ve çok farklı konu, yer veya yapılardan oluşan fotoğrafları görebilir, ayrıca bunları duvar kağıdınız yapabilirsiniz." +
                Environment.NewLine + Environment.NewLine + @"Umut D. / 2017 / umutd.com", @"Hakkında",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsmKapat_Click(object sender, EventArgs e)
        {
            // Uygulamadan çık
            Application.Exit();
        }
    }
}