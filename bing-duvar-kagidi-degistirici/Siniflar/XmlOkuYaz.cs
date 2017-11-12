using System;
using System.IO;
using System.Text;
using System.Xml;

namespace bing_duvar_kagidi_degistirici.Siniflar
{
    internal class XmlOkuYaz
    {
        // Değişken alanı
        public bool AyarlarBaslangic, AyarlarOtomatikDegistir;
        public DateTime AyarlarSaat;
        public string AyarlarSeciliUlke;
        private readonly string _dizin = AppDomain.CurrentDomain.BaseDirectory + @"\ayarlar.xml";

        // Sınıf alanı
        private readonly XmlDocument _xmlOku = new XmlDocument();

        public void XmlDurumu()
        {
            try
            {
                // Eğer ayarlar.xml varsa, içindeki değerleri oku
                if (File.Exists(_dizin) && new FileInfo(_dizin).Length != 0)
                {
                    _xmlOku.Load(_dizin);

                    // Değerlerde bir sorun yoksa tek tek oku (? ile C# 6.0 ile gelen Conditional Access kullandım)
                    AyarlarBaslangic = Convert.ToBoolean(_xmlOku.SelectSingleNode(@"ayarlar/baslangic")?.InnerText);
                    AyarlarOtomatikDegistir = Convert.ToBoolean(_xmlOku.SelectSingleNode(@"ayarlar/otomatikDegistir")?.InnerText);
                    AyarlarSaat = Convert.ToDateTime(_xmlOku.SelectSingleNode(@"ayarlar/girilenSaat")?.InnerText);
                    AyarlarSeciliUlke = _xmlOku.SelectSingleNode(@"ayarlar/ulke")?.InnerText;
                }

                // Eğer ayarlar.xml yoksa, yeni ayarlar.xml oluştur. İçini varsayılan bilgilerle doldur
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

                    xmlYaz.WriteStartElement("ulke");
                    xmlYaz.WriteString("Türkiye");
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