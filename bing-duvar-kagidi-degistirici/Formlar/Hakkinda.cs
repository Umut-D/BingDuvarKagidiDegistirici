using System.Diagnostics;
using System.Windows.Forms;

namespace BingDuvarKagidi.Formlar
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
            Process.Start(@"https://www.umutd.com");
        }
    }
}
