using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using bing_duvar_kagidi_degistirici.Siniflar;

namespace bing_duvar_kagidi_degistirici.Formlar
{
    public partial class FrmUlkeSec : Form
    {
        public FrmUlkeSec()
        {
            InitializeComponent();
        }

        // Sınıflar
        readonly ThumbnailIndir _thumbnailIndir = new ThumbnailIndir();
        readonly FrmAyarlar _frmAyarlar = new FrmAyarlar();
        private Thread _threadIslemi;

        public void ThumbnailleriYukle()
        {
            // Önizleme görsellerini indir (Döngü içinde yapmayı çok düşündüm, fakat olduramadım nedense)
            _thumbnailIndir.ThumbnailIndirveYukle(pboxAlmanya, BingWebSiteleri.BingAlmanya);
            _thumbnailIndir.ThumbnailIndirveYukle(pboxAmerika, BingWebSiteleri.BingAmerika);
            _thumbnailIndir.ThumbnailIndirveYukle(pboxAvustralya, BingWebSiteleri.BingAvustralya);
            _thumbnailIndir.ThumbnailIndirveYukle(pboxBrezilya, BingWebSiteleri.BingBrezilya);
            _thumbnailIndir.ThumbnailIndirveYukle(pboxBritanya, BingWebSiteleri.BingBritanya);
            _thumbnailIndir.ThumbnailIndirveYukle(pboxFransa, BingWebSiteleri.BingFransa);
            _thumbnailIndir.ThumbnailIndirveYukle(pboxHindistan, BingWebSiteleri.BingHindistan);
            _thumbnailIndir.ThumbnailIndirveYukle(pboxJaponya, BingWebSiteleri.BingJaponya);
            _thumbnailIndir.ThumbnailIndirveYukle(pboxKanada, BingWebSiteleri.BingKanada);
            _thumbnailIndir.ThumbnailIndirveYukle(pboxTurkiye, BingWebSiteleri.BingTurkiye);
            _thumbnailIndir.ThumbnailIndirveYukle(pboxYeniZelanda, BingWebSiteleri.BingYeniZelanda);
        }

        private void frmUlkeSec_Load(object sender, EventArgs e)
        {
            // Threading işlemi ile kilitlenmelere son
            _threadIslemi = new Thread(ThumbnailleriYukle);
            _threadIslemi.Start();
        }

        private void FrmUlkeSec_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Threading işlemini sona erdir
            _threadIslemi.Abort();
        }

        private void pboxAlmanya_MouseEnter(object sender, EventArgs e)
        {
            lblAlmanya.BackColor = Color.GreenYellow;            
        }

        private void pboxAlmanya_MouseLeave(object sender, EventArgs e)
        {
            lblAlmanya.BackColor = DefaultBackColor;
        }

        private void pboxAlmanya_Click(object sender, EventArgs e)
        {
            _frmAyarlar.UlkeSeciminiKaydet("Almanya");
            Close();            
        }

        private void pboxAmerika_MouseEnter(object sender, EventArgs e)
        {
            lblAmerika.BackColor = Color.GreenYellow;
        }

        private void pboxAmerika_MouseLeave(object sender, EventArgs e)
        {
            lblAmerika.BackColor = DefaultBackColor;
        }

        private void pboxAmerika_Click(object sender, EventArgs e)
        {
            _frmAyarlar.UlkeSeciminiKaydet("Amerika");
            Close();
        }

        private void pboxAvustralya_MouseEnter(object sender, EventArgs e)
        {
            lblAvustralya.BackColor = Color.GreenYellow;
        }

        private void pboxAvustralya_MouseLeave(object sender, EventArgs e)
        {
            lblAvustralya.BackColor = DefaultBackColor;
        }

        private void pboxAvustralya_Click(object sender, EventArgs e)
        {
            _frmAyarlar.UlkeSeciminiKaydet("Avustralya");
            Close();
        }

        private void pboxBrezilya_MouseEnter(object sender, EventArgs e)
        {
            lblBrezilya.BackColor = Color.GreenYellow;
        }

        private void pboxBrezilya_MouseLeave(object sender, EventArgs e)
        {
            lblBrezilya.BackColor = DefaultBackColor;
        }

        private void pboxBrezilya_Click(object sender, EventArgs e)
        {
            _frmAyarlar.UlkeSeciminiKaydet("Brezilya");
            Close();
        }

        private void pboxBritanya_MouseEnter(object sender, EventArgs e)
        {
            lblBritanya.BackColor = Color.GreenYellow;
        }

        private void pboxBritanya_MouseLeave(object sender, EventArgs e)
        {
            lblBritanya.BackColor = DefaultBackColor;
        }

        private void pboxBritanya_Click(object sender, EventArgs e)
        {
            _frmAyarlar.UlkeSeciminiKaydet("Britanya");
            Close();
        }

        private void pboxFransa_MouseEnter(object sender, EventArgs e)
        {
            lblFransa.BackColor = Color.GreenYellow;
        }

        private void pboxFransa_MouseLeave(object sender, EventArgs e)
        {
            lblFransa.BackColor = DefaultBackColor;
        }

        private void pboxFransa_Click(object sender, EventArgs e)
        {
            _frmAyarlar.UlkeSeciminiKaydet("Fransa");
            Close();
        }

        private void pboxHindistan_MouseEnter(object sender, EventArgs e)
        {
            lblHindistan.BackColor = Color.GreenYellow;
        }

        private void pboxHindistan_MouseLeave(object sender, EventArgs e)
        {
            lblHindistan.BackColor = DefaultBackColor;
        }

        private void pboxHindistan_Click(object sender, EventArgs e)
        {
            _frmAyarlar.UlkeSeciminiKaydet("Hindistan");
            Close();
        }

        private void pboxJaponya_MouseEnter(object sender, EventArgs e)
        {
            lblJaponya.BackColor = Color.GreenYellow;
        }

        private void pboxJaponya_MouseLeave(object sender, EventArgs e)
        {
            lblJaponya.BackColor = DefaultBackColor;
        }

        private void pboxJaponya_Click(object sender, EventArgs e)
        {
            _frmAyarlar.UlkeSeciminiKaydet("Japonya");
            Close();
        }

        private void pboxKanada_MouseEnter(object sender, EventArgs e)
        {
            lblKanada.BackColor = Color.GreenYellow;
        }

        private void pboxKanada_MouseLeave(object sender, EventArgs e)
        {
            lblKanada.BackColor = DefaultBackColor;
        }

        private void pboxKanada_Click(object sender, EventArgs e)
        {
            _frmAyarlar.UlkeSeciminiKaydet("Kanada");
            Close();
        }

        private void pboxTurkiye_MouseEnter(object sender, EventArgs e)
        {
            lblTurkiye.BackColor = Color.GreenYellow;
        }

        private void pboxTurkiye_MouseLeave(object sender, EventArgs e)
        {
            lblTurkiye.BackColor = DefaultBackColor;
        }

        private void pboxTurkiye_Click(object sender, EventArgs e)
        {
            _frmAyarlar.UlkeSeciminiKaydet("Türkiye");
            Close();
        }

        private void pboxYeniZelanda_MouseEnter(object sender, EventArgs e)
        {
            lblYeniZelanda.BackColor = Color.GreenYellow;
        }

        private void pboxYeniZelanda_MouseLeave(object sender, EventArgs e)
        {
            lblYeniZelanda.BackColor = DefaultBackColor;
        }

        private void pboxYeniZelanda_Click(object sender, EventArgs e)
        {
            _frmAyarlar.UlkeSeciminiKaydet("Yeni Zelanda");
            Close();
        }

        private void pboxAvusturalya_Click(object sender, EventArgs e)
        {

        }

        private void pboxAvusturalya_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pboxAvusturalya_MouseLeave(object sender, EventArgs e)
        {

        }
    }
}
