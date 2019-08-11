using System;
using System.Collections.Generic;
using System.Text;

using ManagedOT.Cryptography;
using ManagedOT.Networking;

namespace ManagedOT
{
    public class TwoChoicesCorrelatedExtendedObliviousTransferProvider : IObliviousTransferProvider<ITwoChoicesCorrelatedObliviousTransferChannel>
    {
        private IObliviousTransferProvider<ITwoChoicesObliviousTransferChannel> _baseOT;
        private int _securityParameter;
        private CryptoContext _cryptoContext;

        public TwoChoicesCorrelatedExtendedObliviousTransferProvider(IObliviousTransferProvider<ITwoChoicesObliviousTransferChannel> baseOT, int securityParameter, CryptoContext cryptoContext)
        {
            _baseOT = baseOT;
            _securityParameter = securityParameter;
            _cryptoContext = cryptoContext;
        }

        public ITwoChoicesCorrelatedObliviousTransferChannel CreateChannel(IMessageChannel channel)
        {
            return new TwoChoicesCorrelatedExtendedObliviousTransferChannel(_baseOT.CreateChannel(channel), _securityParameter, _cryptoContext);
        }
    }
}
