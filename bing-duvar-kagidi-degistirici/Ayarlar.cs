using System;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;

namespace bing_duvar_kagidi_degistirici
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        // Değişken, sınıf vs. oluşturma alanı
        private readonly string _dizin = AppDomain.CurrentDomain.BaseDirectory + @"\ayarlar.xml";
        private readonly string _programDizini = System.Reflection.Assembly.GetExecutingAssembly().Location;
        private string _registryDegeri;
        private readonly XmlDocument _xmlOku = new XmlDocument();
        private readonly XmlOkuYaz _xmlOkuYaz = new XmlOkuYaz();
        private readonly RegistryKey _baslangicRegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        private XmlNode _baslangicNode, _otomatikDegistir;

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            // XML Oku, yoksa XML oluştur
            _xmlOkuYaz.XmlDurumu();

            // Otomatik değiştir aktifse saati açık hale getir
            dtpSaat.Enabled = chkboxOtomatikDegistir.CheckState == CheckState.Checked;

            // Checkbox ve saat durumlarını belirle
            chkboxBaslangic.CheckState = _xmlOkuYaz.AyarlarBaslangic ? CheckState.Checked : CheckState.Unchecked;
            chkboxOtomatikDegistir.CheckState = _xmlOkuYaz.AyarlarOtomatikDegistir ? CheckState.Checked : CheckState.Unchecked;
            dtpSaat.Text = _xmlOkuYaz.AyarlarGirilenSaat.ToShortTimeString();
        }

        private void chkboxOtomatikDegistir_CheckedChanged(object sender, EventArgs e)
        {
            // Otomatik değiştir aktifse saati açık hale getir
            dtpSaat.Enabled = chkboxOtomatikDegistir.CheckState == CheckState.Checked;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Program ayarlarını yap ve XML dosyasına kaydet
            _xmlOku.Load(_dizin);

            // Eğer başlangıçta çalıştır seçeneği aktifse Registry'ye kayıt gir
            if (chkboxBaslangic.Checked)
            {
                _baslangicNode = _xmlOku.SelectSingleNode(@"ayarlar/baslangic");
                if (_baslangicNode != null)
                    _baslangicNode.InnerText = "True";

                // Gerekli Registry ayarını yap ve programın Windows'un başlangıcında çalışmasını sağla
                if (_baslangicRegistryKey != null)
                    _baslangicRegistryKey.SetValue("Bing Duvar Kağıdı Değiştirici", _programDizini + " /noshow", RegistryValueKind.String);
            }
            else
            {                
                _baslangicNode = _xmlOku.SelectSingleNode(@"ayarlar/baslangic");
                if (_baslangicNode  != null)
                    _baslangicNode.InnerText = "False";

                // Registry değeri boş değilse sil
                _registryDegeri = (string) _baslangicRegistryKey.GetValue("Bing Duvar Kağıdı Değiştirici");
                if (_registryDegeri != null)
                    _baslangicRegistryKey.DeleteValue("Bing Duvar Kağıdı Değiştirici");
            }
            
            // Eğer otomatik değiştir aktifse TRUE/FALSE değerlerini XML dosyasına yaz
            _otomatikDegistir = _xmlOku.SelectSingleNode(@"ayarlar/otomatikDegistir");            
            if (_otomatikDegistir != null)
                _otomatikDegistir.InnerText = chkboxOtomatikDegistir.Checked ? "True" : "False";

            // Girilen saat değerini al ve XML dosyasına yaz
            _xmlOku.SelectSingleNode(@"ayarlar/girilenSaat").InnerText = dtpSaat.Value.ToShortTimeString();
            _xmlOkuYaz.AyarlarGirilenSaat = Convert.ToDateTime(dtpSaat.Value.ToShortTimeString());

            _xmlOku.Save(_dizin);

            MessageBox.Show(@"Ayarlar Değiştirildi...", @"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}