using System;
using Microsoft.Win32;

namespace BingLibrary
{
    /// <summary>
    /// Duvar kağıdı stil değerlerini HKCU\Control Panel\Desktop altında okuyup yazar.
    /// </summary>
    public sealed class WindowsKayitDefteriDuvarKagidi : IDuvarKagidiKayitDefteri
    {
        private const string MasaustuAnahtari = @"Control Panel\Desktop";

        public (string? Style, string? Tile) Oku()
        {
            using RegistryKey? anahtar = Registry.CurrentUser.OpenSubKey(MasaustuAnahtari, writable: false);

            return (
                anahtar?.GetValue("WallpaperStyle") as string,
                anahtar?.GetValue("TileWallpaper") as string);
        }

        public void Yaz(string wallpaperStyle, string tileWallpaper)
        {
            using RegistryKey? anahtar = Registry.CurrentUser.OpenSubKey(MasaustuAnahtari, writable: true);
            if (anahtar == null)
                throw new InvalidOperationException(@"Registry anahtarı açılamadı: HKCU\Control Panel\Desktop");

            anahtar.SetValue("WallpaperStyle", wallpaperStyle);
            anahtar.SetValue("TileWallpaper", tileWallpaper);
        }
    }
}
