using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using ManagedOT.Networking;

namespace ManagedOT.BitOT
{
    /// <summary>
    /// A 1-out-of-2 bit Oblivious Transfer channel implementation.
    /// 
    /// Provides 1oo2-OT for single bits on a given channel (i.e., pair of parties) and may maintain
    /// channel-specific protocol state in-between invocations.
    /// </summary>
    public interface ITwoChoicesBitObliviousTransferChannel
    {
        /// <summary>
        /// Sends one out of two options provided by the sender. Which one is chosen by the receiver without the sender learning that.
        /// 
        /// Several invocations can be batched into one function call, indicated by the numberOfInvocations parameter. Each option
        /// for each invocation has to be of the specified length.
        /// 
        /// The number of invoactions and message length are explicitely passed as parameters to enforce that the options are given correctly.
        /// </summary>
        /// <param name="options">An array of pairs of option bits, one pair for each invocation. The receiver obtains exactly one element from each pair.</param>
        /// <param name="numberOfInvocations">The number of invocations/instances of OT.</param>
        /// <returns></returns>
        Task SendAsync(Pair<Bit>[] options, int numberOfInvocations);

        /// <summary>
        /// Receives one out of two options provided by the sender. Which one is chosen by the receiver without the sender learning that.
        /// 
        /// Several invocations can be batched into one function call, indicated by the numberOfInvocations parameter. Each option
        /// for each invocation has to be of the specified length.
        /// 
        /// The number of invoactions and message length are explicitely passed as parameters to enforce that the options are given correctly.
        /// </summary>
        /// <param name="selectionIndices">An array of selection bits, one for each invocation.</param>
        /// <param name="numberOfInvocations">The number of invocations/instances of OT.</param>
        /// <returns>An array of the received options/messages.</returns>
        Task<BitArray> ReceiveAsync(BitArray selectionIndices, int numberOfInvocations);

        /// <summary>
        /// Receives one out of two options provided by the sender. Which one is chosen by the receiver without the sender learning that.
        /// 
        /// Several invocations can be batched into one function call, indicated by the numberOfInvocations parameter. Each option
        /// for each invocation has to be of the specified length.
        /// 
        /// The number of invoactions and message length are explicitely passed as parameters to enforce that the options are given correctly.
        /// </summary>
        /// <param name="selectionIndices">An array of pair indices, one for each invocation.</param>
        /// <param name="numberOfInvocations">The number of invocations/instances of OT.</param>
        /// <returns>An array of the received options/messages.</returns>
        Task<BitArray> ReceiveAsync(PairIndexArray selectionIndices, int numberOfInvocations);

        /// <summary>
        /// The network channel the OT operates on, uniquely identifying the pair of parties involved in the OT.
        /// </summary>
        IMessageChannel Channel { get; }
    }
}
