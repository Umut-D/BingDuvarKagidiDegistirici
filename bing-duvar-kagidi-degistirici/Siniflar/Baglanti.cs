using System;
using System.Runtime.InteropServices;

namespace BingDuvarKagidi.Siniflar
{
    public class Baglanti
    {
        // İnternet bağlatısını kontrol etmek için wininet.dll'yi kullanıp işletim sistemi kaynaklarına eriş
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(ref InternetConnectionStateFlags lpdwFlags, int dwReserved);

        [Flags]
        private enum InternetConnectionStateFlags
        {
            INTERNET_CONNECTION_MODEM = 0x01,
            INTERNET_CONNECTION_LAN = 0x02,
            INTERNET_CONNECTION_PROXY = 0x04,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }

        public static bool Kontrol()
        {
            InternetConnectionStateFlags flags = 0;
            bool baglantiDurumu = InternetGetConnectedState(ref flags, 0);

            return baglantiDurumu;
        }
    }
}