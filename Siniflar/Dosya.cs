using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace Siniflar
{
    public class Dosya
    {
        public string DosyaYoluVeAdi => Yol();
        private readonly Gorsel _gorsel;

        public Dosya(Gorsel gorsel)
        {
            _gorsel = gorsel;
        }

        private string Yol()
        {
            KlasorVarMi();

            string mevcutDizin = Directory.GetCurrentDirectory();
            string tarihBicemi = DateTime.Now.ToString("yyyy.MM.dd");
            string dosyaAdi = tarihBicemi + ".bmp";

            return mevcutDizin + "\\images\\" + dosyaAdi;
        }

        private void KlasorVarMi()
        {
            string anaDizin = AppDomain.CurrentDomain.BaseDirectory + "images";
            if (!Directory.Exists(anaDizin))
                Directory.CreateDirectory(anaDizin);
        }

        public void Kaydet()
        {
            using (WebClient webIstem = new WebClient())
            {
                using (Stream akis = webIstem.OpenRead(_gorsel.Adres))
                {
                    if (akis != null)
                    {
                        using (Bitmap bitmap = new Bitmap(akis))
                        {
                            if (File.Exists(DosyaYoluVeAdi))
                                File.Delete(DosyaYoluVeAdi);

                            bitmap.Save(DosyaYoluVeAdi, ImageFormat.Bmp);
                        }
                    }
                }
            }
        }
    }
}