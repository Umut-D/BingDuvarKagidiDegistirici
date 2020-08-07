using System;
using System.Diagnostics;
using System.Windows.Forms;
using BingDuvarKagidi.Properties;
using BingDuvarKagidi.Siniflar;

namespace BingDuvarKagidi.Formlar
{
    public partial class FrmBing : Form
    {
        public FrmBing()
        {
            InitializeComponent();
        }

        private readonly Indir _indir = new Indir();
        private int _ayar;

        private void FrmBing_Load(object sender, EventArgs e)
        {
            // Ayarlardan hangi ülkenin seçili olduğunu belirle
            _ayar = (int) Settings.Default["Ulke"];

            Ulke();
            Indir();
            BingeGit();
        }

        private void TsmiDuvarKagidi_Click(object sender, EventArgs e)
        {
            Degistir();
        }

        private void TsmiSeciliUlke_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            tsmiSeciliUlke.Text = e.ClickedItem.Text;

            ToolStripMenuItem tiklananIndeks = (ToolStripMenuItem)sender;
            _ayar = tiklananIndeks.DropDownItems.IndexOf(e.ClickedItem);

            Gorseller gorseller = new Gorseller();
            tsmiSeciliUlke.Image = gorseller.Ulke(tsmiSeciliUlke);

            BingeGit();
            Indir();
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

        private void CmsKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TsmBing_Click(object sender, EventArgs e)
        {
            string seciliUlke = tsmiSeciliUlke.Text;
            Process.Start(_indir.Adres(seciliUlke));
        }

        private void TsmGuncelle_Click(object sender, EventArgs e)
        {
            Guncelle guncelle = new Guncelle();
            guncelle.Kontrol();
        }

        private void TsmHakkinda_Click(object sender, EventArgs e)
        {
            FrmHakkinda frmHakkinda = new FrmHakkinda();
            frmHakkinda.ShowDialog();
        }

        private void FrmBing_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Seçili ülkenin ayarını kaydederek çıkış yap
            Settings.Default["Ulke"] = _ayar;
            Settings.Default.Save();
        }

        private void BtnIleri_Click(object sender, EventArgs e)
        {
            _ayar++;
            if (_ayar == 10)
                _ayar = 9;

            Ulke();
            Indir();
            BingeGit();
        }

        private void BtnGeri_Click(object sender, EventArgs e)
        {
            _ayar--;
            if (_ayar == -1)
                _ayar = 0;

            Ulke();
            Indir();
            BingeGit();
        }

        // ProcessCmdKey'i ezme sayesinde sağ ve sol oka basılarak görseller ileri-geri götürülebiliyor
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                    BtnIleri_Click(null, null);
                    return true;
                case Keys.Left:
                    BtnGeri_Click(null, null);
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Ulke()
        {
            tsmiSeciliUlke.Text = tsmiSeciliUlke.DropDownItems[_ayar].Text;
            tsmiSeciliUlke.Image = tsmiSeciliUlke.DropDownItems[_ayar].Image;
        }

        private void Indir()
        {
            Cozunurluk cozunurluk = new Cozunurluk();
            _indir.Yukle(cozunurluk.Bul(), tsmiSeciliUlke.Text);

            tssDurum.Text = _indir.DuzenliBilgi;
            tsmiDuvarKagidi.Enabled = true;
        }

        private void Degistir()
        {
            DuvarKagidi duvarKagidi = new DuvarKagidi();
            Uri adres = new Uri(_indir.DosyaAdresi);
            duvarKagidi.Olustur(adres, DuvarKagidi.EkranKonumu.Uzat);

            notifyIcon.BalloonTipText = _indir.DuzenliBilgi;
            notifyIcon.ShowBalloonTip(2500);
        }

        private void BingeGit()
        {
            tsmBing.Text = tsmiSeciliUlke.Text + @" Bing.com'a Git";
        }
    }
}