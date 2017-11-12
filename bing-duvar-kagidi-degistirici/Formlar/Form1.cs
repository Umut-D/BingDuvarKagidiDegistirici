using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using bing_duvar_kagidi_degistirici.Siniflar;

namespace bing_duvar_kagidi_degistirici.Formlar
{
    public partial class FrmBing : Form
    {
        public FrmBing()
        {
            InitializeComponent();
        }
        
        // Sınıflar
        private readonly Cozunurluk _cozunurluk = new Cozunurluk();
        private readonly DuvarKagidiIndir _duvarKagidiIndir = new DuvarKagidiIndir();
        private readonly XmlOkuYaz _xmlOkuYaz = new XmlOkuYaz();
        private readonly Guncelle _guncelle = new Guncelle();
        private readonly Baglanti _baglanti = new Baglanti();
        private readonly FrmHakkinda _frmHakkinda = new FrmHakkinda();
        private readonly FrmUlkeSec _frmUlkeSec = new FrmUlkeSec();
        private readonly SeciliUlkeGorseli _seciliUlkeGorseli = new SeciliUlkeGorseli();

        // Değişkenler
        public string EkranCozunurlugu, DuvarKagidiBilgisi, Saat;

        private void FrmBing_Load(object sender, EventArgs e)
        {
            // XML Oku, yoksa XML oluştur
            _xmlOkuYaz.XmlDurumu();
            tsmiSeciliUlke.Text = _xmlOkuYaz.AyarlarSeciliUlke;

            // Çözünürlük sınıfından gelen çözünürlük bilgisini al
            EkranCozunurlugu = _cozunurluk.CozunurlukBul();

            // İnternet bağlantısı olup olmadığını kontrol et
            _baglanti.BaglantiyiKontrolEt(tssDurum);
           
            // Değiştir butonu program yüklenince aktif olmasın. Kullanıcı önce görseli indirmeli
            tsmDegistir.Enabled = false;

            // Seçili olan ülkenin bayrağını sağ üstteki TSMISeciliUlke alanında göster
            _seciliUlkeGorseli.UlkeGorseliniYukle(tsmiSeciliUlke);
        }

        public void tsmIndir_Click(object sender, EventArgs e)
        {            
            // Xml dosyasından seçili ülkeyi al ve ona göre duvar kağıdını yükle
            _duvarKagidiIndir.SecilenBingWebSitesi(_xmlOkuYaz.AyarlarSeciliUlke);
            
            // Duvar kağıdını indir ve yükle
            _duvarKagidiIndir.DuvarKagidiIndirVeYukle(pbox, EkranCozunurlugu);

            tsmDegistir.Enabled = true;
            tssDurum.Text = _duvarKagidiIndir.DuzenliBilgi;
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

        private void tsmDegistir_Click(object sender, EventArgs e)
        {
            // Duvar kağıdını dizine indir ve değiştir
            var resimAdresi = new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\" + _duvarKagidiIndir.Tarih.ToString("MM.dd.yyyy") + @"-Bing" + _duvarKagidiIndir.UlkeAdi + ".bmp");
            DuvarKagidi.DuvarKagidiOlustur(resimAdresi, DuvarKagidi.Konumlandirma.Uzat);

            // Windows ekranında gerekli bilgilendirmeyi 2 saniye boyunca yap
            notifyIcon.BalloonTipText = _duvarKagidiIndir.DuzenliBilgi;
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
            _guncelle.GuncellemeKontroluYap();
        }

        private void tsmBing_Click(object sender, EventArgs e)
        {
            // Hangi Bing sitesi programda seçili ise o siteye dallan
            string seciliSiteAdresi = tsmiSeciliUlke.ToString();
            Process.Start(_duvarKagidiIndir.SecilenBingWebSitesi(seciliSiteAdresi));
        }

        private void tsmHakkinda_Click(object sender, EventArgs e)
        {
            _frmHakkinda.ShowDialog();
        }

        private void tsmiSeciliUlke_Click(object sender, EventArgs e)
        {
            _frmUlkeSec.ShowDialog();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Mevcut ve XML dosyasından kaydedilen zamanları al
            Saat = DateTime.Now.ToString("HH:mm:ss");
            _xmlOkuYaz.XmlDurumu();

            if (_xmlOkuYaz.AyarlarOtomatikDegistir)
            {
                // Eğer mevcut saatle, kullanıcının girdiği saat aynı ise duvar kağıdını otomatik olarak değiştir
                if (Saat == _xmlOkuYaz.AyarlarSaat.ToLongTimeString())
                {
                    tsmIndir.PerformClick();
                    tsmDegistir.PerformClick();
                }
            }
        }

        private void tsmKapat_Click(object sender, EventArgs e)
        {
            // Uygulamadan çık
            Application.Exit();
        }
    }
}