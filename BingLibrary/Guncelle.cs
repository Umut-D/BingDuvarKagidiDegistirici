using System.Reflection;
using System.Xml;

namespace BingLibrary
{
    public enum GuncellemeDurumu
    {
        Guncel,
        YeniSurumMevcut,
        BaglantiYok,
        HataOlustu
    }

    public sealed class GuncellemeSonucu
    {
        public GuncellemeDurumu Durum { get; init; }
        public string MevcutSurum { get; init; } = string.Empty;
        public string? SunucuSurumu { get; init; }
        public string Mesaj { get; init; } = string.Empty;

        // Yalnızca YeniSurumMevcut durumunda dolu olur; arayüz katmanı kullanıcıya sorup açar.
        public string? IndirmeAdresi { get; init; }
    }

    public class Guncelle
    {
        public const string VarsayilanGuncellemeLinki =
            "https://raw.githubusercontent.com/Umut-D/umutd.com/master/assets/program-versions/bing-duvar-kagidi-degistirici.xml";

        public const string VarsayilanProgramSayfasiLinki =
            "https://www.umutd.com/programlar/bing-duvar-kagidi-degistirici";

        private readonly IInternetDenetleyici? _baglanti;
        private readonly HttpClient _httpIstemcisi;
        private readonly Version _mevcutSurum;

        public Guncelle(IInternetDenetleyici baglanti)
            : this(baglanti, Baglanti.PaylasilanHttpIstemcisi, UygulamaSurumunuGetir())
        {
        }

        // Birim testlerinde sahte HTTP ve sürüm bilgisi verebilmek için
        public Guncelle(IInternetDenetleyici? baglanti, HttpClient httpIstemcisi, Version mevcutSurum)
        {
            _baglanti = baglanti;
            _httpIstemcisi = httpIstemcisi ?? throw new ArgumentNullException(nameof(httpIstemcisi));
            _mevcutSurum = SurumuNormallestir(mevcutSurum ?? throw new ArgumentNullException(nameof(mevcutSurum)));
        }

        // Tek sürüm kaynağı: derlenmiş assembly sürümü (BingUI.csproj <Version>)
        public static Version UygulamaSurumunuGetir()
        {
            Version hamSurum = Assembly.GetEntryAssembly()?.GetName().Version ?? new Version(0, 0, 0);
            return SurumuNormallestir(hamSurum);
        }

        public static Version SurumuNormallestir(Version surum)
        {
            int build = surum.Build < 0 ? 0 : surum.Build;
            return new Version(surum.Major, surum.Minor, build);
        }

        public async Task<GuncellemeSonucu> VersiyonKontroluYapAsync(CancellationToken cancellationToken = default)
        {
            if (_baglanti != null && !await _baglanti.InternetVarMiAsync(cancellationToken).ConfigureAwait(false))
            {
                return new GuncellemeSonucu
                {
                    Durum = GuncellemeDurumu.BaglantiYok,
                    MevcutSurum = _mevcutSurum.ToString(),
                    Mesaj = "İnternet bağlantısı olmadığı için güncelleme kontrolü yapılamadı."
                };
            }

            try
            {
                using var yanit = await _httpIstemcisi
                    .GetAsync(VarsayilanGuncellemeLinki, cancellationToken)
                    .ConfigureAwait(false);

                yanit.EnsureSuccessStatusCode();

                await using Stream xmlAkisi =
                    await yanit.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
                string? sunucuSurumMetni = XmlIcindenSurumOku(xmlAkisi);

                if (!Version.TryParse(sunucuSurumMetni, out Version? hamSunucuSurumu))
                {
                    string hataMesaji = "Sunucudan geçerli bir sürüm bilgisi okunamadı.";
                    return new GuncellemeSonucu
                    {
                        Durum = GuncellemeDurumu.HataOlustu,
                        MevcutSurum = _mevcutSurum.ToString(),
                        SunucuSurumu = sunucuSurumMetni,
                        Mesaj = hataMesaji
                    };
                }

                Version sunucuSurumu = SurumuNormallestir(hamSunucuSurumu);

                if (sunucuSurumu <= _mevcutSurum)
                {
                    string mesaj = "Program günceldir. Yeni versiyon çıkana kadar şimdilik en iyisi bu.";
                    return new GuncellemeSonucu
                    {
                        Durum = GuncellemeDurumu.Guncel,
                        MevcutSurum = _mevcutSurum.ToString(),
                        SunucuSurumu = sunucuSurumu.ToString(),
                        Mesaj = mesaj
                    };
                }

                string yeniSurumMesaji =
                    $"Yeni bir güncelleme var ({sunucuSurumu}). Web sayfasına girip indirmek ister misiniz?";

                return new GuncellemeSonucu
                {
                    Durum = GuncellemeDurumu.YeniSurumMevcut,
                    MevcutSurum = _mevcutSurum.ToString(),
                    SunucuSurumu = sunucuSurumu.ToString(),
                    Mesaj = yeniSurumMesaji,
                    IndirmeAdresi = VarsayilanProgramSayfasiLinki
                };
            }
            catch (Exception ex)
            {
                string hataMesaji = $"Güncelleme sunucusuna ulaşılamadı: {ex.Message}";

                return new GuncellemeSonucu
                {
                    Durum = GuncellemeDurumu.HataOlustu,
                    MevcutSurum = _mevcutSurum.ToString(),
                    Mesaj = hataMesaji
                };
            }
        }

        private static string? XmlIcindenSurumOku(Stream xmlAkisi)
        {
            var ayarlar = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Prohibit,
                IgnoreWhitespace = true
            };

            using XmlReader xmlOku = XmlReader.Create(xmlAkisi, ayarlar);
            while (xmlOku.Read())
            {
                if (xmlOku.NodeType == XmlNodeType.Element && xmlOku.Name == "bing" && xmlOku.HasAttributes)
                    return xmlOku.GetAttribute("version");
            }

            return null;
        }
    }
}