using System.Drawing;
using System.Windows.Forms;
using BingDuvarKagidi.Properties;

namespace BingDuvarKagidi.Siniflar
{
    public class Gorseller
    {
        private string SeciliUlke { get; set; }

        public Image Ulke(ToolStripMenuItem tsmiUlke)
        {
            SeciliUlke = tsmiUlke.Text + "_icon";

            return (Image) Resources.ResourceManager.GetObject(SeciliUlke);
        }
    }
}