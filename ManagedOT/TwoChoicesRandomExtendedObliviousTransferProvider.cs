using System;
using System.Collections.Generic;
using System.Text;

using ManagedOT.Cryptography;
using ManagedOT.Networking;

namespace ManagedOT
{
    public class TwoChoicesRandomExtendedObliviousTransferProvider : IObliviousTransferProvider<ITwoChoicesRandomObliviousTransferChannel>
    {
        private IObliviousTransferProvider<ITwoChoicesObliviousTransferChannel> _baseOT;
        private int _securityParameter;
        private CryptoContext _cryptoContext;

        public TwoChoicesRandomExtendedObliviousTransferProvider(IObliviousTransferProvider<ITwoChoicesObliviousTransferChannel> baseOT, int securityParameter, CryptoContext cryptoContext)
        {
            _baseOT = baseOT;
            _securityParameter = securityParameter;
            _cryptoContext = cryptoContext;
        }

        public ITwoChoicesRandomObliviousTransferChannel CreateChannel(IMessageChannel channel)
        {
            return new TwoChoicesRandomExtendedObliviousTransferChannel(_baseOT.CreateChannel(channel), _securityParameter, _cryptoContext);
        }
    }
}
