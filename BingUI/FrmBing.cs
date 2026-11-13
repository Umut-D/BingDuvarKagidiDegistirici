using System.Diagnostics;
using BingLibrary;
using BingUI.Properties;

namespace BingUI
{
    public partial class FrmBing : Form
    {
        private readonly IInternetDenetleyici _baglanti;
        private readonly IAyarDeposu _ayarlar;
        private CancellationTokenSource? _indirmeCts;
        private Gorsel? _sonGorsel;
        private Dosya? _sonDosya;
        private int _ayar;

        public FrmBing()
        {
            InitializeComponent();

            _baglanti = new Baglanti();
            _ayarlar = new Ayarlar();
        }

        private async void FrmBing_Load(object sender, EventArgs e)
        {
            SeciliUlkeyiDropDownListesineEkle();
            SeciliUlkeAdiniMenuyeEkle();

            if (!await _baglanti.InternetVarMiAsync())
            {
                tssDurum.Text = _baglanti.SonHataMesaji ?? "İnternet bağlantısı kurulamadı.";
                return;
            }

            await IndirVeGoruntuleAsync(zorlaYenidenIndir: false);
        }

        private void SeciliUlkeyiDropDownListesineEkle()
        {
            KullaniciAyarlari kayitliAyar = _ayarlar.Yukle();
            int bulunanIndeks = -1;

            for (int i = 0; i < tsmiSeciliUlke.DropDownItems.Count; i++)
            {
                if (string.Equals(tsmiSeciliUlke.DropDownItems[i].Text, kayitliAyar.SeciliUlke,
                        StringComparison.OrdinalIgnoreCase))
                {
                    bulunanIndeks = i;
                    break;
                }
            }

            if (bulunanIndeks >= 0)
            {
                _ayar = bulunanIndeks;
            }
            else if (kayitliAyar.UlkeIndeksi >= 0 && kayitliAyar.UlkeIndeksi < tsmiSeciliUlke.DropDownItems.Count)
            {
                _ayar = kayitliAyar.UlkeIndeksi;
            }
            else
            {
                _ayar = tsmiSeciliUlke.DropDownItems.Count - 1;
            }

            ToolStripItem seciliOge = tsmiSeciliUlke.DropDownItems[_ayar];
            tsmiSeciliUlke.Text = seciliOge.Text;
            tsmiSeciliUlke.Image = seciliOge.Image;
        }

        private async Task IndirVeGoruntuleAsync(bool zorlaYenidenIndir)
        {
            _indirmeCts?.Cancel();
            _indirmeCts?.Dispose();
            _indirmeCts = new CancellationTokenSource();
            CancellationToken ct = _indirmeCts.Token;

            tsmiDuvarKagidi.Enabled = false;
            tssDurum.Text = $"{tsmiSeciliUlke.Text} için günün görseli indiriliyor...";

            try
            {
                // Her istek kendi Gorsel/Dosya örneğini kullanır; üst üste istekler birbirini bozmaz
                var gorsel = new Gorsel();
                var dosya = new Dosya(gorsel);
                Cozunurluk cozunurluk = new Cozunurluk(Screen.PrimaryScreen?.Bounds);

                await gorsel.WebSayfasiniIndirAsync(cozunurluk.EkranCozunurlukEki, tsmiSeciliUlke.Text ?? string.Empty, ct);
                await dosya.KaydetAsync(zorlaYenidenIndir, ct);
                ct.ThrowIfCancellationRequested();

                Image yeniGorsel = GorselYukleyici.Yukle(dosya);
                Image? eskiGorsel = pbox.Image;
                pbox.Image = yeniGorsel;
                eskiGorsel?.Dispose();

                _sonGorsel = gorsel;
                _sonDosya = dosya;
                tssDurum.Text = gorsel.Bilgi;
                tsmiDuvarKagidi.Enabled = true;
            }
            catch (OperationCanceledException)
            {
                // Kullanıcı yeni bir ülke seçtiğinde önceki istek iptal edilir
            }
            catch (Exception ex)
            {
                YerelLog.Varsayilan.HataYaz("GorselIndirVeGoruntule", ex);
                string kullaniciMesaji = HataSiniflandirici.KullaniciMesajiOlustur(ex);
                tssDurum.Text = kullaniciMesaji;

                MessageBox.Show(
                    $"{kullaniciMesaji}\n\nTeknik ayrıntılar log klasörüne kaydedildi:\n{YerelLog.Varsayilan.LogDizini}",
                    "İndirme Hatası",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void SeciliUlkeAdiniMenuyeEkle()
        {
            tsmBing.Text = $"{tsmiSeciliUlke.Text} Bing.com'a Git";
        }

        private void TsmiDuvarKagidi_Click(object sender, EventArgs e)
        {
            try
            {
                if (_sonDosya is null || _sonGorsel is null)
                {
                    MessageBox.Show("Önce bir görsel indirilmelidir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DuvarKagidi duvarKagidi = new DuvarKagidi();
                duvarKagidi.Olustur(_sonDosya.DosyaYoluVeAdi, DuvarKagidi.EkranKonumu.Uzat);

                notifyIcon.BalloonTipText = _sonGorsel.Bilgi;
                notifyIcon.ShowBalloonTip(2500);
                tssDurum.Text = "Duvar kağıdı başarıyla uygulandı.";
            }
            catch (Exception ex)
            {
                YerelLog.Varsayilan.HataYaz("DuvarKagidiUygula", ex, HataAsamasi.WindowsApi);
                string kullaniciMesaji = HataSiniflandirici.KullaniciMesajiOlustur(ex, HataAsamasi.WindowsApi);

                MessageBox.Show(
                    $"{kullaniciMesaji}\n\nTeknik ayrıntılar log klasörüne kaydedildi:\n{YerelLog.Varsayilan.LogDizini}",
                    "Duvar Kağıdı Hatası",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void TsmiSeciliUlke_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == null)
                return;

            tsmiSeciliUlke.Text = e.ClickedItem.Text;

            ToolStripMenuItem indeks = (ToolStripMenuItem)sender;
            _ayar = indeks.DropDownItems.IndexOf(e.ClickedItem);

            tsmiSeciliUlke.Image = UlkeIkonuEkle(tsmiSeciliUlke) ?? e.ClickedItem.Image;

            SeciliUlkeAdiniMenuyeEkle();
            await IndirVeGoruntuleAsync(zorlaYenidenIndir: true);
        }

        public Image? UlkeIkonuEkle(ToolStripMenuItem tsmiUlke)
        {
            string seciliUlke = $"{tsmiUlke.Text}_icon";
            return Resources.ResourceManager.GetObject(seciliUlke) as Image;
        }

        private void TsmBing_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://www.bing.com",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                YerelLog.Varsayilan.HataYaz("TarayiciAc", ex);
                MessageBox.Show($"Tarayıcı açılamadı:\n{ex.Message}", "Hata", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void TsmGuncelle_Click(object sender, EventArgs e)
        {
            tsmGuncelle.Enabled = false;
            try
            {
                Guncelle guncelle = new Guncelle(_baglanti);
                GuncellemeSonucu sonuc = await guncelle.VersiyonKontroluYapAsync();

                if (sonuc.Durum == GuncellemeDurumu.YeniSurumMevcut && sonuc.IndirmeAdresi is not null)
                {
                    DialogResult cevap = MessageBox.Show(
                        $"{sonuc.Mesaj}\n\nSürüm sayfasını tarayıcıda açmak ister misiniz?",
                        "Güncelleme Mevcut",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (cevap == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = sonuc.IndirmeAdresi,
                            UseShellExecute = true
                        });
                    }
                }
                else
                {
                    MessageBox.Show(sonuc.Mesaj, "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                YerelLog.Varsayilan.HataYaz("GuncellemeKontrolu", ex);
                MessageBox.Show(
                    $"Güncelleme kontrolü sırasında hata oluştu:\n{HataSiniflandirici.KullaniciMesajiOlustur(ex)}",
                    "Güncelleme Hatası",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                tsmGuncelle.Enabled = true;
            }
        }

        private void TsmHakkinda_Click(object sender, EventArgs e)
        {
            using FrmHakkinda frmHakkinda = new FrmHakkinda(_baglanti);
            frmHakkinda.ShowDialog(this);
        }

        private void FrmBing_FormClosing(object sender, FormClosingEventArgs e)
        {
            _indirmeCts?.Cancel();
            _indirmeCts?.Dispose();

            pbox.Image?.Dispose();
            pbox.Image = null;

            try
            {
                _ayarlar.Kaydet(new KullaniciAyarlari
                {
                    SeciliUlke = tsmiSeciliUlke.Text ?? string.Empty,
                    UlkeIndeksi = _ayar
                });
            }
            catch (Exception ex)
            {
                YerelLog.Varsayilan.HataYaz("AyarKaydet", ex, HataAsamasi.Ayarlar);
            }
        }

        private void CmsKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmBing_Resize(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Minimized:
                    notifyIcon.Visible = true;
                    notifyIcon.BalloonTipText = "Program hala çalışıyor.";
                    notifyIcon.BalloonTipTitle = "Bing Duvar Kağıdı Değiştirici";
                    notifyIcon.ShowBalloonTip(500);
                    Hide();
                    break;
                case FormWindowState.Normal:
                    notifyIcon.Visible = false;
                    break;
            }
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            Show();
            WindowState = FormWindowState.Normal;
        }

        private void CmsGoster_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
    }
}