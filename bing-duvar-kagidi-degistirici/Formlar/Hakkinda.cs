using System.Diagnostics;
using System.Windows.Forms;

namespace bing_duvar_kagidi_degistirici.Formlar
{
    public partial class FrmHakkinda : Form
    {
        public FrmHakkinda()
        {
            InitializeComponent();
        }

        private void llblHakkinda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Umutd.com anasayfasına dallan
            Process.Start(@"http://www.umutd.com");
        }
    }
}
