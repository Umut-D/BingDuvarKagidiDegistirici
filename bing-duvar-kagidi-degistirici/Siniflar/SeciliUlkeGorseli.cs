using System.Drawing;
using System.Windows.Forms;
using BingDuvarKagidi.Properties;

namespace BingDuvarKagidi.Siniflar
{
    public class SeciliUlkeGorseli
    {
        public Image UlkeGorseliniYukle(ToolStripMenuItem tsmiUlke)
        {
            switch (tsmiUlke.Text)
            {
                case @"Almanya":
                    tsmiUlke.Image = (Image) Resources.ResourceManager.GetObject("Almanya_icon");
                    break;
                case @"Amerika":
                    tsmiUlke.Image = (Image) Resources.ResourceManager.GetObject("Amerika_icon");
                    break;
                case @"Avustralya":
                    tsmiUlke.Image = (Image) Resources.ResourceManager.GetObject("Avusturalya_icon");
                    break;
                case @"Brezilya":
                    tsmiUlke.Image = (Image) Resources.ResourceManager.GetObject("Brezilya_icon");
                    break;
                case @"Britanya":
                    tsmiUlke.Image = (Image) Resources.ResourceManager.GetObject("Britanya_icon");
                    break;
                case @"Fransa":
                    tsmiUlke.Image = (Image) Resources.ResourceManager.GetObject("Fransa_icon");
                    break;
                case @"Hindistan":
                    tsmiUlke.Image = (Image) Resources.ResourceManager.GetObject("Hindistan_icon");
                    break;
                case @"Japonya":
                    tsmiUlke.Image = (Image) Resources.ResourceManager.GetObject("Japonya_icon");
                    break;
                case @"Kanada":
                    tsmiUlke.Image = (Image) Resources.ResourceManager.GetObject("Kanada_icon");
                    break;
                case @"Türkiye":
                    tsmiUlke.Image = (Image) Resources.ResourceManager.GetObject("Türkiye_icon");
                    break;
                case @"Yeni Zelanda":
                    tsmiUlke.Image = (Image) Resources.ResourceManager.GetObject("Yeni_Zelanda_icon");
                    break;
            }

            return tsmiUlke.Image;
        }
    }
}