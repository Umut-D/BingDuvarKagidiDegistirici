namespace BingLibrary
{
    /// <summary>
    /// Masaüstü duvar kağıdı kayıt defteri değerlerini okuyup yazan soyutlama.
    /// </summary>
    public interface IDuvarKagidiKayitDefteri
    {
        (string? Style, string? Tile) Oku();

        void Yaz(string wallpaperStyle, string tileWallpaper);
    }
}
