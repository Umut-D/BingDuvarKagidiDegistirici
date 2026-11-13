namespace BingLibrary
{
    /// <summary>
    /// Verilen görseli masaüstü duvar kağıdı olarak uygulayan soyutlama.
    /// </summary>
    public interface IDuvarKagidiUygulayici
    {
        /// <returns>Başarılı işlemde sıfırdan farklı bir değer.</returns>
        int Uygula(string tamDosyaYolu);
    }
}
