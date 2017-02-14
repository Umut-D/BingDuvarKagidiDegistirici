using System;
using System.IO;
using System.Text;
using System.Xml;

namespace bing_duvar_kagidi_degistirici
{
    internal class XmlOkuYaz
    {
        // Değişken alanı
        private readonly XmlDocument _xmlOku = new XmlDocument();
        public bool AyarlarBaslangic, AyarlarOtomatikDegistir;
        public DateTime AyarlarGirilenSaat;
        private readonly string _dizin = AppDomain.CurrentDomain.BaseDirectory + @"\ayarlar.xml";

        public void XmlDurumu()
        {
            try
            {
                // Eğer ayarlar.xml varsa, içindeki değerleri oku
                if (File.Exists(_dizin) && new FileInfo(_dizin).Length != 0)
                {
                    _xmlOku.Load(_dizin);

                    // Değerlerde sorun yoksa oku
                    AyarlarBaslangic = Convert.ToBoolean(_xmlOku.SelectSingleNode(@"ayarlar/baslangic").InnerText);
                    AyarlarOtomatikDegistir = Convert.ToBoolean(_xmlOku.SelectSingleNode(@"ayarlar/otomatikDegistir").InnerText);
                    AyarlarGirilenSaat = Convert.ToDateTime(_xmlOku.SelectSingleNode(@"ayarlar/girilenSaat").InnerText);
                }

                // Eğer ayarlar.xml yoksa, yeni ayarlar.xml oluştur. İçini doldur
                else
                {
                    File.Create(_dizin).Close();

                    XmlTextWriter xmlYaz = new XmlTextWriter(_dizin, Encoding.UTF8)
                    {
                        Formatting = Formatting.Indented
                    };

                    xmlYaz.WriteStartElement("ayarlar");

                    xmlYaz.WriteStartElement("baslangic");
                    xmlYaz.WriteString("False");
                    xmlYaz.WriteEndElement();

                    xmlYaz.WriteStartElement("otomatikDegistir");
                    xmlYaz.WriteString("False");
                    xmlYaz.WriteEndElement();

                    xmlYaz.WriteStartElement("girilenSaat");
                    xmlYaz.WriteString("11:11");
                    xmlYaz.WriteEndElement();

                    xmlYaz.WriteEndElement();

                    xmlYaz.Close();
                }
            }
            catch (Exception genelHataMesaji)
            {
                HataMesajlari.GenelHataMesaji(genelHataMesaji.Message);
            }
        }
    }
}