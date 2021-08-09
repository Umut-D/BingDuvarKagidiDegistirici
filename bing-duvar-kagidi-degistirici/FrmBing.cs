using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BingDuvarKagidi.Properties;
using Siniflar;

namespace BingDuvarKagidi
{
    public partial class FrmBing : Form
    {
        private readonly Baglanti _baglanti;
        private readonly Gorsel _gorsel;
        private readonly Dosya _dosya;
        private int _ayar;

        public FrmBing()
        {
            InitializeComponent();

            _baglanti = new Baglanti();
            _gorsel = new Gorsel();
            _dosya = new Dosya(_gorsel);
        }

        private void FrmBing_Load(object sender, EventArgs e)
        {
            _baglanti.InternetVarMi();

            Yukle();
        }

        private void Yukle()
        {
            SeciliUlkeyiDropDownListesineEkle();
            IndirVeGoruntule();
            SeciliUlkeAdiniMenuyeEkle();
        }

        private void SeciliUlkeyiDropDownListesineEkle()
        {
            _ayar = (int) Settings.Default["Ulke"];

            tsmiSeciliUlke.Text = tsmiSeciliUlke.DropDownItems[_ayar].Text;
            tsmiSeciliUlke.Image = tsmiSeciliUlke.DropDownItems[_ayar].Image;
        }

        private void IndirVeGoruntule()
        {
            Cozunurluk cozunurluk = new Cozunurluk();
            _gorsel.WebSayfasiniIndir(cozunurluk.EkranCozunurlukEki, tsmiSeciliUlke.Text);

            _dosya.Kaydet();

            pbox.Image = _gorsel.Yukle();

            tssDurum.Text = _gorsel.Bilgi;
            tsmiDuvarKagidi.Enabled = true;
        }

        private void SeciliUlkeAdiniMenuyeEkle()
        {
            tsmBing.Text = $@"{tsmiSeciliUlke.Text} Bing.com'a Git";
        }

        private void TsmiDuvarKagidi_Click(object sender, EventArgs e)
        {
            DuvarKagidi duvarKagidi = new DuvarKagidi();
            duvarKagidi.Olustur(_dosya.DosyaYoluVeAdi, DuvarKagidi.EkranKonumu.Uzat);

            notifyIcon.BalloonTipText = _gorsel.Bilgi;
            notifyIcon.ShowBalloonTip(2500);
        }

        private void TsmiSeciliUlke_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            tsmiSeciliUlke.Text = e.ClickedItem.Text;

            ToolStripMenuItem indeks = (ToolStripMenuItem) sender;
            _ayar = indeks.DropDownItems.IndexOf(e.ClickedItem);

            tsmiSeciliUlke.Image = UlkeIkonuEkle(tsmiSeciliUlke);

            SeciliUlkeAdiniMenuyeEkle();
            IndirVeGoruntule();
        }

        public Image UlkeIkonuEkle(ToolStripMenuItem tsmiUlke)
        {
            string seciliUlke = tsmiUlke.Text + "_icon";
            return (Image) Resources.ResourceManager.GetObject(seciliUlke);
        }

        private void TsmBing_Click(object sender, EventArgs e)
        {
            Ulke ulke = new Ulke(tsmiSeciliUlke.Text);
            Process.Start(ulke.BingWebAdresi);
        }

        private void TsmGuncelle_Click(object sender, EventArgs e)
        {
            Guncelle guncelle = new Guncelle(_baglanti);
            guncelle.VersiyonKontroluYap();
        }

        private void TsmHakkinda_Click(object sender, EventArgs e)
        {
            FrmHakkinda frmHakkinda = new FrmHakkinda(_baglanti);
            frmHakkinda.ShowDialog();
        }

        private void FrmBing_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default["Ulke"] = _ayar;
            Settings.Default.Save();
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