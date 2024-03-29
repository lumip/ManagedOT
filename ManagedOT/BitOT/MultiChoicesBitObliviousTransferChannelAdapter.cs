﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using ManagedOT.Networking;

namespace ManagedOT.BitOT
{
    /// <summary>
    /// Implements a 1-out-of-N bit Oblivious Transfer channel on top of any 1-out-of-N OT channel 
    /// implementation for arbitrary message lengths.
    /// </summary>
    public class MultiChoicesBitObliviousTransferChannelAdapter : IMultiChoicesBitObliviousTransferChannel
    {
        private IMultiChoicesObliviousTransferChannel _generalOt;

        public MultiChoicesBitObliviousTransferChannelAdapter(IMultiChoicesObliviousTransferChannel generalOt)
        {
            _generalOt = generalOt;
        }

        public IMessageChannel Channel { get { return _generalOt.Channel; } }

        private byte[][][] ConvertBitToByteOptionMessages(BitArray[][] options)
        {
            byte[][][] optionMessages = new byte[options.Length][][];
            for (int i = 0; i < optionMessages.Length; ++i)
            {
                optionMessages[i] = options[i].Select(option => option.ToBytes()).ToArray();
            }

            return optionMessages;
        }
        
        private BitArray ConvertByteToBitResultMessages(byte[][] resultMessages)
        {
            BitArray result = new BitArray(resultMessages.Length);
            for (int i = 0; i < result.Length; ++i)
                result[i] = (Bit)resultMessages[i][0];

            return result;
        }

        public Task SendAsync(BitArray[][] options, int numberOfOptions, int numberOfInvocations)
        {
            return _generalOt.SendAsync(ConvertBitToByteOptionMessages(options), numberOfOptions, numberOfInvocations, 1);
        }

        public Task<BitArray> ReceiveAsync(int[] selectionIndices, int numberOfOptions, int numberOfInvocations)
        {
            return _generalOt.ReceiveAsync(selectionIndices, numberOfOptions, numberOfInvocations, 1).ContinueWith(
                task => ConvertByteToBitResultMessages(task.Result)
            );
        }
    }
}
