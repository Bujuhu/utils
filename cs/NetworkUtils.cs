using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Utils
{
    public class NetworkUtils
    {
        public static bool TryGetHostname(IPAddress ipAdrress, out string? remoteHostname)
        {
            try
            {
                remoteHostname = Dns.GetHostEntry(ipAdrress).HostName.ToLower();
                return true;
            }
            catch (SocketException)
            {
                remoteHostname = null;
                return false;
            }
        }
    }
}
