using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BingDuvarKagidi.Siniflar
{
    internal class Baglanti
    {
        // İnternet bağlatısını kontrol etmek için wininet.dll'yi kullanıp işletim sistemi kaynaklarına eriş
        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
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

        public void BaglantiKontrol(ToolStripStatusLabel tssDurum)
        {
            InternetConnectionStateFlags flags = 0;
            bool baglantiDurumu = InternetGetConnectedState(ref flags, 0);

            if (baglantiDurumu)
            {
                tssDurum.Text = @"İnternet bağlantınız var. Duvar kağıdını indirebilirsiniz.";
                tssDurum.ForeColor = Color.Green;
            }
            else
            {
                tssDurum.Text = @"İnternet bağlantınız maalesef yok. Günün duvar kağıdını indiremezsiniz.";
                tssDurum.ForeColor = Color.MediumVioletRed;
            }
        }
    }
}