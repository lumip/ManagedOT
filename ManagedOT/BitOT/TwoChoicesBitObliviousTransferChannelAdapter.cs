using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using ManagedOT.Networking;

namespace ManagedOT.BitOT
{
    /// <summary>
    /// Implements a 1-out-of-2 bit Oblivious Transfer channel on top of any 1-out-of-2 OT channel 
    /// implementation for arbitrary message lengths.
    /// </summary>
    public class TwoChoicesBitObliviousTransferChannelAdapter : ITwoChoicesBitObliviousTransferChannel
    {

        private ITwoChoicesObliviousTransferChannel _generalOt;

        public TwoChoicesBitObliviousTransferChannelAdapter(ITwoChoicesObliviousTransferChannel generalOt)
        {
            _generalOt = generalOt;
        }

        public IMessageChannel Channel { get { return _generalOt.Channel; } }

        public Task<BitArray> ReceiveAsync(BitArray selectionIndices, int numberOfInvocations)
        {
            return _generalOt.ReceiveAsync(selectionIndices, numberOfInvocations, 1).ContinueWith(
                task => ConvertOutputByteToBit(task.Result)
            );
        }

        public Task<BitArray> ReceiveAsync(PairIndexArray selectionIndices, int numberOfInvocations)
        {
            return _generalOt.ReceiveAsync(selectionIndices, numberOfInvocations, 1).ContinueWith(
                task => ConvertOutputByteToBit(task.Result)
            );
        }

        public Task SendAsync(Pair<Bit>[] correlationBits, int numberOfInvocations)
        {
            return _generalOt.SendAsync(ConvertOptionBitsToBytes(correlationBits), numberOfInvocations, 1);
        }

        private Pair<byte[]>[] ConvertOptionBitsToBytes(Pair<Bit>[] correlationBits)
        {
            return correlationBits.Select(pair => new Pair<byte[]>(
                new byte[] { Convert.ToByte(pair[0].Value) },
                new byte[] { Convert.ToByte(pair[1].Value) } )).ToArray();
        }

        private BitArray ConvertOutputByteToBit(byte[][] resultMessages)
        {
            return new BitArray(resultMessages.Select(message => (Bit)message[0]).ToArray());
        }
    }
}
