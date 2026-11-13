namespace BingLibrary
{
    /// <summary>
    /// Kullanıcı ayarlarını okuyup yazan soyutlama.
    /// </summary>
    public interface IAyarDeposu
    {
        KullaniciAyarlari Yukle();

        void Kaydet(KullaniciAyarlari ayarlar);
    }
}
