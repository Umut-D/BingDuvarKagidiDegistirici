using System;
using System.ComponentModel;
using System.IO;

namespace BingLibrary
{
    /// <summary>
    /// Duvar kağıdını uygular. Kayıt defteri ve Windows API çağrıları arayüzler üzerinden yapılır (DIP).
    /// </summary>
    public class DuvarKagidi
    {
        public enum EkranKonumu
        {
            Dose,
            Merkezi,
            Uzat
        }

        private readonly IDuvarKagidiKayitDefteri _kayitDefteri;
        private readonly IDuvarKagidiUygulayici _uygulayici;

        public DuvarKagidi()
            : this(new WindowsKayitDefteriDuvarKagidi(), new WindowsMasaustuDuvarKagidiUygulayici())
        {
        }

        public DuvarKagidi(IDuvarKagidiKayitDefteri kayitDefteri, IDuvarKagidiUygulayici uygulayici)
        {
            _kayitDefteri = kayitDefteri ?? throw new ArgumentNullException(nameof(kayitDefteri));
            _uygulayici = uygulayici ?? throw new ArgumentNullException(nameof(uygulayici));
        }

        public void Olustur(string dosya, EkranKonumu ekranKonumu)
        {
            if (string.IsNullOrWhiteSpace(dosya))
                throw new ArgumentException("Duvar kağıdı dosya yolu boş olamaz.", nameof(dosya));

            string tamYol = Path.GetFullPath(dosya);
            if (!File.Exists(tamYol))
                throw new FileNotFoundException("Duvar kağıdı olarak uygulanacak görsel dosyası bulunamadı.", tamYol);

            // Registry ve masaüstü API'si birlikte tutarlı olmalı; API başarısızsa eski ayar geri yüklenir
            (string? eskiStyle, string? eskiTile) = _kayitDefteri.Oku();
            EkranRegistryYaz(ekranKonumu);

            try
            {
                int sonuc = _uygulayici.Uygula(tamYol);
                if (sonuc == 0)
                    throw new Win32Exception(0, "Windows masaüstü duvar kağıdı uygulanamadı (Win32 Hata Kodu: 0).");
            }
            catch
            {
                if (eskiStyle != null && eskiTile != null)
                    _kayitDefteri.Yaz(eskiStyle, eskiTile);

                throw;
            }
        }

        private void EkranRegistryYaz(EkranKonumu ekranKonumu)
        {
            switch (ekranKonumu)
            {
                case EkranKonumu.Uzat:
                    _kayitDefteri.Yaz("2", "0");
                    break;
                case EkranKonumu.Merkezi:
                    _kayitDefteri.Yaz("1", "0");
                    break;
                case EkranKonumu.Dose:
                    _kayitDefteri.Yaz("1", "1");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ekranKonumu), ekranKonumu, "Geçersiz ekran konumu.");
            }
        }
    }
}
