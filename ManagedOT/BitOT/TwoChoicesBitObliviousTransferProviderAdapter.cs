using System;
using System.Collections.Generic;
using System.Text;

using ManagedOT.Networking;

namespace ManagedOT.BitOT
{
    /// <summary>
    /// Implements a 1-out-of-2 bit Oblivious Transfer channel provider using any 1-out-of-2 OT provider
    /// implementation for arbitrary message lengths and wrapping the channels it returns into
    /// <see cref="TwoChoicesBitObliviousTransferChannelAdapter"/>.
    /// </summary>
    public class TwoChoicesBitObliviousTransferProviderAdapter : IObliviousTransferProvider<ITwoChoicesBitObliviousTransferChannel>
    {
        private IObliviousTransferProvider<ITwoChoicesObliviousTransferChannel> _otProvider;

        public TwoChoicesBitObliviousTransferProviderAdapter(IObliviousTransferProvider<ITwoChoicesObliviousTransferChannel> otProvider)
        {
            _otProvider = otProvider;
        }

        public ITwoChoicesBitObliviousTransferChannel CreateChannel(IMessageChannel channel)
        {
            return new TwoChoicesBitObliviousTransferChannelAdapter(_otProvider.CreateChannel(channel));
        }
    }
}
