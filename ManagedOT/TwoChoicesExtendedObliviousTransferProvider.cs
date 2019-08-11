using System;
using System.Collections.Generic;
using System.Text;

namespace ManagedOT
{
    public class TwoChoicesExtendedObliviousTransferProvider : IObliviousTransferProvider<ITwoChoicesObliviousTransferChannel>
    {
        private IObliviousTransferProvider<ITwoChoicesObliviousTransferChannel> _baseOT;
        private int _securityParameter;
        private CryptoContext _cryptoContext;

        public TwoChoicesExtendedObliviousTransferProvider(IObliviousTransferProvider<ITwoChoicesObliviousTransferChannel> baseOT, int securityParameter, CryptoContext cryptoContext)
        {
            _baseOT = baseOT;
            _securityParameter = securityParameter;
            _cryptoContext = cryptoContext;
        }

        public ITwoChoicesObliviousTransferChannel CreateChannel(IMessageChannel channel)
        {
            return new TwoChoicesExtendedObliviousTransferChannel(_baseOT.CreateChannel(channel), _securityParameter, _cryptoContext);
        }
    }
}
