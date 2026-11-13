using System.Diagnostics;
using BingLibrary;

namespace BingUI
{
    public partial class FrmHakkinda : Form
    {
        private readonly IInternetDenetleyici _baglanti;

        public FrmHakkinda(IInternetDenetleyici baglanti)
        {
            InitializeComponent();

            _baglanti = baglanti;
            SurumVeYilBilgisiniGuncelle();
        }

        private void SurumVeYilBilgisiniGuncelle()
        {
            string surumMetni = Guncelle.UygulamaSurumunuGetir().ToString(3);

            lblProgramAdi.Text = $"Bing Duvar Kağıdı Değiştirici {surumMetni}";
            lblAltBilgi.Text = "Umut D. | Açık kaynak lisanslı (MIT) olarak yazılmıştır | 2026";
        }

        private async void LblHakkinda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!await _baglanti.InternetVarMiAsync())
            {
                MessageBox.Show(
                    _baglanti.SonHataMesaji ?? "İnternet bağlantısı kurulamadı.",
                    "Bağlantı Hatası",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://umutd.com",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Web sayfası açılamadı:\n{ex.Message}", "Hata", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FrmHakkinda_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }
    }
}