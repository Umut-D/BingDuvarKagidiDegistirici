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

        private void LblHakkinda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://umutd.com");
        }

        private void FrmHakkinda_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
