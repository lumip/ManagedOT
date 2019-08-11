using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

using ManagedOT.Networking;

namespace ManagedOT.UnitTests.Util
{
    public static class TestNetworkSession
    {
        public static int Port = 16741;

        public static ITwoPartyNetworkSession EstablishTwoParty()
        {
            try
            {
                return TcpTwoPartyNetworkSession.AcceptAsync(new Party(1), Port).Result;
            }
            catch (Exception)
            {
                return TcpTwoPartyNetworkSession.ConnectAsync(new Party(0), IPAddress.Loopback, Port).Result;
            }
        }
    }
}
