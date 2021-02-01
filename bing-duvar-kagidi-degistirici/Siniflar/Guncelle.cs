using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace BingDuvarKagidi.Siniflar
{
    public class Guncelle
    {
        private readonly Baglanti _baglanti;

        public Guncelle(Baglanti baglanti)
        {
            _baglanti = baglanti;
        }

        public void VersiyonKontroluYap()
        {
            if (_baglanti.InternetVarMi())
                WebXmlBaglanti();
        }

        private void WebXmlBaglanti()
        {
            string guncellemeLinki = @"https://raw.githubusercontent.com/Umut-D/umutd.com/master/assets/program-versions/bing-duvar-kagidi-degistirici.xml";

            WebClient webIstemcisi = new WebClient();
            ServicePointManager.SecurityProtocol = (SecurityProtocolType) 3072;
            XmlReader xmlOku = XmlReader.Create(webIstemcisi.OpenRead(guncellemeLinki) ?? throw new InvalidOperationException());

            WebXmlOku(xmlOku);
        }

        private void WebXmlOku(XmlReader xmlOku)
        {
            while (xmlOku.Read())
            {
                if (xmlOku.NodeType != XmlNodeType.Element || xmlOku.Name != "bing" || !xmlOku.HasAttributes)
                    continue;

                VersiyonKarsilastir(xmlOku);
            }
        }

        private void VersiyonKarsilastir(XmlReader xmlOku)
        {
            string guncelVersiyon = "2.4";
            string sunucudakiVersiyon = xmlOku.GetAttribute("version");

            if (guncelVersiyon == sunucudakiVersiyon)
                MessageBox.Show(@"Program günceldir. Yeni versiyon çıkana kadar şimdilik en iyisi bu.", @"Güncelle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DialogResult guncelleDiyalog = MessageBox.Show(@"Yeni bir güncelleme var. Programı " + sunucudakiVersiyon + @" versiyonuna yükselttim. Yenilikler var. Web sayfasına girip indirmek ister misiniz?", @"Güncelle", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (guncelleDiyalog == DialogResult.OK)
                    Process.Start("http://www.umutd.com/programlar/bing-duvar-kagidi-degistirici");
            }
        }
    }
}