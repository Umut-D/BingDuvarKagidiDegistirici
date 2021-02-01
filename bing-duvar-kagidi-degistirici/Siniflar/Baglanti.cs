using System.Net;
using System.Windows.Forms;

namespace BingDuvarKagidi.Siniflar
{
    public class Baglanti
    {
        public bool InternetVarMi()
        {
            try
            {
                using (WebClient webBaglanti = new WebClient())
                {
                    using (webBaglanti.OpenRead("http://google.com/"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                MessageBox.Show(@"Maalesef internet bağlantınız aktif değil.", @"Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}