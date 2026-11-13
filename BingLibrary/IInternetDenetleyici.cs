using System.Threading;
using System.Threading.Tasks;

namespace BingLibrary
{
    /// <summary>
    /// İnternet bağlantısını denetleyen soyutlama. Üst katmanlar somut <see cref="Baglanti"/> sınıfına değil bu arayüze bağlanır.
    /// </summary>
    public interface IInternetDenetleyici
    {
        string? SonHataMesaji { get; }

        Task<bool> InternetVarMiAsync(CancellationToken cancellationToken = default);
    }
}
