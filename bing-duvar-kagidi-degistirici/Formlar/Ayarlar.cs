using System;
using System.Windows.Forms;
using System.Xml;
using bing_duvar_kagidi_degistirici.Siniflar;
using Microsoft.Win32;

namespace bing_duvar_kagidi_degistirici.Formlar
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

        private readonly RegistryKey _baslangicRegistryKey =
            Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        private XmlNode _baslangicNode, _otomatikDegistir, _girilenSaat, _ulkeler;
        private readonly SeciliUlkeGorseli _seciliUlkeGorseli = new SeciliUlkeGorseli();

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            // XML Oku, yoksa XML oluştur
            _xmlOkuYaz.XmlDurumu();

            // Checkbox, saat ve seçili ülke alanlarını belirle
            chkboxBaslangic.CheckState = _xmlOkuYaz.AyarlarBaslangic ? CheckState.Checked : CheckState.Unchecked;
            cboxUlkeler.SelectedItem = _xmlOkuYaz.AyarlarSeciliUlke;
        }

        public void UlkeSeciminiKaydet(string gelenUlkeDegeri)
        {
            // Program ayarlarını yap ve XML dosyasına kaydet
            _xmlOku.Load(_dizin);

            // Seçili ülkeyi belirle
            _ulkeler = _xmlOku.SelectSingleNode(@"ayarlar/ulke");
            if (_ulkeler != null)
                _ulkeler.InnerText = gelenUlkeDegeri;

            // Sonuçların FrmBing (Ana Form) ekranına geçmesi için değişken aktarımı yap
            FrmBing frmBing = (FrmBing) Application.OpenForms["FrmBing"];
            if (frmBing != null)
            {
                frmBing.tsmiSeciliUlke.Text = gelenUlkeDegeri;
                frmBing.tsmiSeciliUlke.Image = _seciliUlkeGorseli.UlkeGorseliniYukle(frmBing.tsmiSeciliUlke);
            }

            _xmlOku.Save(_dizin);
        }

        public void btnKaydet_Click(object sender, EventArgs e)
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
                _baslangicRegistryKey?.SetValue("Bing Duvar Kağıdı Değiştirici", _programDizini + " /noshow", RegistryValueKind.String);
            }
            else
            {
                _baslangicNode = _xmlOku.SelectSingleNode(@"ayarlar/baslangic");
                if (_baslangicNode != null)
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
            _girilenSaat = _xmlOku.SelectSingleNode(@"ayarlar/girilenSaat");
            if (_girilenSaat != null)
                _girilenSaat.InnerText = dtpSaat.Value.ToShortTimeString();
            _xmlOkuYaz.AyarlarSaat = Convert.ToDateTime(dtpSaat.Value.ToShortTimeString());

            // Seçili ülkeyi belirle
            _ulkeler = _xmlOku.SelectSingleNode(@"ayarlar/ulke");
            if (_ulkeler != null)
                _ulkeler.InnerText = cboxUlkeler.SelectedItem.ToString();

            // Sonuçların FrmBing (Ana Form) ekranına geçmesi için değişken aktarımı yap
            FrmBing frmBing = (FrmBing) Application.OpenForms["FrmBing"];
            if (frmBing != null)
            {
                frmBing.tsmiSeciliUlke.Text = cboxUlkeler.Text;
                frmBing.tsmiSeciliUlke.Image = _seciliUlkeGorseli.UlkeGorseliniYukle(frmBing.tsmiSeciliUlke);
            }

            // Değerleri mevcut XML dosyasına kaydet
            _xmlOku.Save(_dizin);

            MessageBox.Show(@"Ayarlar Değiştirildi. İyi de oldu, güzel de oldu.", @"Bilgi", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}