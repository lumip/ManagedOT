using System.Threading.Tasks;
using System.Security.Cryptography;
using System;

using ManagedOT.Networking;

namespace ManagedOT
{
    /// <summary>
    /// A 1-out-of-2 Oblivious Transfer channel implementation.
    /// 
    /// Provides 1oo2-OT on a given channel (i.e., pair of parties) and may maintain
    /// channel-specific protocol state in-between invocations.
    /// </summary>
    public interface ITwoChoicesObliviousTransferChannel
    {
        /// <summary>
        /// Sends one out of two options provided by the sender. Which one is chosen by the receiver without the sender learning that.
        /// 
        /// Several invocations can be batched into one function call, indicated by the numberOfInvocations parameter. Each option
        /// for each invocation has to be of the specified length.
        /// 
        /// The number of invoactions and message length are explicitely passed as parameters to enforce that the options are given correctly.
        /// </summary>
        /// <param name="options">An array of pairs of options, one pair for each invocation. The receiver obtains exactly one element from each pair.</param>
        /// <param name="numberOfInvocations">The number of invocations/instances of OT.</param>
        /// <param name="numberOfMessageBytes">The length of each option/message in bytes.</param>
        /// <returns></returns>
        Task SendAsync(Pair<byte[]>[] options, int numberOfInvocations, int numberOfMessageBytes);

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
        /// <param name="numberOfMessageBytes">The length of each option/message in bytes.</param>
        /// <returns>An array of the received options/messages.</returns>
        Task<byte[][]> ReceiveAsync(BitArray selectionIndices, int numberOfInvocations, int numberOfMessageBytes);

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
        /// <param name="numberOfMessageBytes">The length of each option/message in bytes.</param>
        /// <returns>An array of the received options/messages.</returns>
        Task<byte[][]> ReceiveAsync(PairIndexArray selectionIndices, int numberOfInvocations, int numberOfMessageBytes);

        /// <summary>
        /// The network channel the OT operates on, uniquely identifying the pair of parties involved in the OT.
        /// </summary>
        IMessageChannel Channel { get; }
    }
}
