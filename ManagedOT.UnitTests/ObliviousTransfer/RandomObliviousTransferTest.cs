﻿using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

using ManagedOT.Networking;
using ManagedOT.Cryptography;
using ManagedOT.Stateless;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ManagedOT.UnitTests.Util;

namespace ManagedOT.UnitTests
{
    [TestClass]
    public class RandomObliviousTransferTest
    {
        private const int NumberOfMessageBytes = 20;
        private const int NumberOfInvocations = 10;

        [TestMethod]
        public void TestRandomObliviousTransfer()
        {
            BitArray selectionBits = BitArray.FromBinaryString("0110011010");

            Task<byte[][]> receiverTask = Task.Factory.StartNew(() => RunObliviousTransferReceiverParty(selectionBits), TaskCreationOptions.LongRunning);
            Task<Pair<byte[]>[]> senderTask = Task.Factory.StartNew(() => RunObliviousTransferSenderParty(NumberOfInvocations), TaskCreationOptions.LongRunning);

            Task.WhenAll(
                receiverTask,
                senderTask
            ).Wait();

            byte[][] receiverResult = receiverTask.Result;
            Pair<byte[]>[] senderResult = senderTask.Result;
            Assert.AreEqual(NumberOfInvocations, senderResult.Length, "Sender did not return options for all invocations.");
            Assert.AreEqual(NumberOfInvocations, receiverResult.Length, "Receiver did not return results for all invocations.");

            for (int i = 0; i < NumberOfInvocations; ++i)
            {
                Assert.AreEqual(NumberOfMessageBytes, senderResult[i][0].Length, "First sender option does not have the expected length.");
                Assert.AreEqual(NumberOfMessageBytes, senderResult[i][1].Length, "Second sender option does not have the expected length.");
                CollectionAssert.AreNotEqual(senderResult[i][0], senderResult[i][1], "First and second sender option are equal.");
                byte[] expected = senderResult[i][Convert.ToInt32(selectionBits[i].Value)];
                CollectionAssert.AreEqual(
                    expected,
                    receiverResult[i],
                    "Incorrect message content {0} (should be {1}).",
                    BitArray.FromBytes(receiverResult[i], 8 * NumberOfMessageBytes).ToBinaryString(),
                    BitArray.FromBytes(expected, 8 * NumberOfMessageBytes).ToBinaryString()
                );
            }
        }

        [TestMethod]
        public void TestResumedRandomObliviousTransfer()
        {
            BitArray[] roundSelectionBits = new[] { BitArray.FromBinaryString("0110011010"), BitArray.FromBinaryString("1101001101") };

            Task<Pair<byte[][]>> receiverTask = Task.Factory.StartNew(
                () => RunResumedObliviousTransferReceiverParty(roundSelectionBits[0], roundSelectionBits[1]), TaskCreationOptions.LongRunning);
            Task<Pair<Pair<byte[]>[]>> senderTask = Task.Factory.StartNew(
                () => RunResumedObliviousTransferSenderParty(NumberOfInvocations), TaskCreationOptions.LongRunning);

            Task.WhenAll(
                receiverTask,
                senderTask
            ).Wait();

            Pair<byte[][]> resumedReceiverResult = receiverTask.Result;
            Pair<Pair<byte[]>[]> resumedSenderResult = senderTask.Result;
            for (int j = 0; j < 2; ++j)
            {
                byte[][] receiverResult = resumedReceiverResult[j];
                Pair<byte[]>[] senderResult = resumedSenderResult[j];

                Assert.AreEqual(NumberOfInvocations, senderResult.Length, "Sender did not return options for all invocations.");
                Assert.AreEqual(NumberOfInvocations, receiverResult.Length, "Receiver did not return results for all invocations.");

                for (int i = 0; i < NumberOfInvocations; ++i)
                {
                    Assert.AreEqual(NumberOfMessageBytes, senderResult[i][0].Length, "First sender option does not have the expected length.");
                    Assert.AreEqual(NumberOfMessageBytes, senderResult[i][1].Length, "Second sender option does not have the expected length.");
                    CollectionAssert.AreNotEqual(senderResult[i][0], senderResult[i][1], "First and second sender option are equal.");
                    byte[] expected = senderResult[i][Convert.ToInt32(roundSelectionBits[j][i].Value)];
                    CollectionAssert.AreEqual(
                        expected,
                        receiverResult[i],
                        "Incorrect message content {0} (should be {1}).",
                        BitArray.FromBytes(receiverResult[i], 8 * NumberOfMessageBytes).ToBinaryString(),
                        BitArray.FromBytes(expected, 8 * NumberOfMessageBytes).ToBinaryString()
                    );
                }
            }
        }

        private byte[][] RunObliviousTransferReceiverParty(BitArray selectionBits)
        {
            using (CryptoContext cryptoContext = CryptoContext.CreateDefault())
            {
                using (ITwoPartyNetworkSession session = TestNetworkSession.EstablishTwoParty())
                {
                    ITwoChoicesObliviousTransferChannel baseOT = new StatelessTwoChoicesObliviousTransferChannel(new InsecureObliviousTransfer(), session.Channel);
                    ITwoChoicesRandomObliviousTransferChannel obliviousTransfer = new TwoChoicesRandomExtendedObliviousTransferChannel(baseOT, 8, cryptoContext);

                    return obliviousTransfer.ReceiveAsync(selectionBits, selectionBits.Length, NumberOfMessageBytes).Result;
                }
            }
        }

        private Pair<byte[]>[] RunObliviousTransferSenderParty(int numberOfInvocations)
        {
            using (CryptoContext cryptoContext = CryptoContext.CreateDefault())
            {
                using (ITwoPartyNetworkSession session = TestNetworkSession.EstablishTwoParty())
                {
                    ITwoChoicesObliviousTransferChannel baseOT = new StatelessTwoChoicesObliviousTransferChannel(new InsecureObliviousTransfer(), session.Channel);
                    ITwoChoicesRandomObliviousTransferChannel obliviousTransfer = new TwoChoicesRandomExtendedObliviousTransferChannel(baseOT, 8, cryptoContext);

                    return obliviousTransfer.SendAsync(numberOfInvocations, NumberOfMessageBytes).Result;
                }
            }
        }

        private Pair<byte[][]> RunResumedObliviousTransferReceiverParty(BitArray firstRoundSelectionBits, BitArray secondRoundSelectionBits)
        {
            using (CryptoContext cryptoContext = CryptoContext.CreateDefault())
            {
                using (ITwoPartyNetworkSession session = TestNetworkSession.EstablishTwoParty())
                {
                    ITwoChoicesObliviousTransferChannel baseOT = new StatelessTwoChoicesObliviousTransferChannel(new InsecureObliviousTransfer(), session.Channel);
                    ITwoChoicesRandomObliviousTransferChannel obliviousTransfer = new TwoChoicesRandomExtendedObliviousTransferChannel(baseOT, 8, cryptoContext);

                    byte[][] firstRoundResults = obliviousTransfer.ReceiveAsync(
                        firstRoundSelectionBits, firstRoundSelectionBits.Length, NumberOfMessageBytes).Result;
                    byte[][] secondRoundResults = obliviousTransfer.ReceiveAsync(
                        secondRoundSelectionBits, secondRoundSelectionBits.Length, NumberOfMessageBytes).Result;
                    return new Pair<byte[][]>(firstRoundResults, secondRoundResults);
                }
            }
        }

        private Pair<Pair<byte[]>[]> RunResumedObliviousTransferSenderParty(int numberOfInvocations)
        {
            using (CryptoContext cryptoContext = CryptoContext.CreateDefault())
            {
                using (ITwoPartyNetworkSession session = TestNetworkSession.EstablishTwoParty())
                {
                    ITwoChoicesObliviousTransferChannel baseOT = new StatelessTwoChoicesObliviousTransferChannel(new InsecureObliviousTransfer(), session.Channel);
                    ITwoChoicesRandomObliviousTransferChannel obliviousTransfer = new TwoChoicesRandomExtendedObliviousTransferChannel(baseOT, 8, cryptoContext);

                    Pair<byte[]>[] firstRoundResults = obliviousTransfer.SendAsync(
                        numberOfInvocations, NumberOfMessageBytes).Result;
                    Pair<byte[]>[] secondRoundResults = obliviousTransfer.SendAsync(
                        numberOfInvocations, NumberOfMessageBytes).Result;
                    return new Pair<Pair<byte[]>[]>(firstRoundResults, secondRoundResults);
                }
            }
        }
    }
}
