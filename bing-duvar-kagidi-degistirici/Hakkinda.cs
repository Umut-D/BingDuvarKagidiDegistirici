using System.Diagnostics;
using System.Windows.Forms;
using Siniflar;

namespace BingDuvarKagidi
{
    public partial class FrmHakkinda : Form
    {
        private readonly Baglanti _baglanti;

        public FrmHakkinda(Baglanti baglanti)
        {
            InitializeComponent();
            
            _baglanti = baglanti;
        }

        private void LblHakkinda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_baglanti.InternetVarMi())
                Process.Start(@"https://umutd.com");
        }

        private void FrmHakkinda_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }
    }
}