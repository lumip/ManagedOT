﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagedOT.Networking
{
    public interface ITwoPartyNetworkSession : IDisposable
    {
        IMessageChannel Channel { get; }
        Party LocalParty { get; }
        Party RemoteParty { get; }
    }
}
