using System;
using System.Collections.Generic;
using System.Text;

namespace ManagedOT.Buffers
{
    public class ByteBuffer
    {
        public byte[] Value;

        public ByteBuffer(int size)
        {
            if (size < 0)
                throw new ArgumentException("Buffer received a negative size argument.", nameof(size));
            Value = new byte[size];
        }

        public ByteBuffer(byte[] value) : this(value.Length)
        {
            Buffer.BlockCopy(Value, 0, value, 0, value.Length);
        }

        // note(lumip): these turned out to be handy in extended OT where messages are given as byte[]
        //  but often need xoring (and converting to and from BitArray every time was tedious (and
        //  probably not that performant)).. this was the closest existing place to put them
        //  but I guess we should find a better one.
        public static void ApplyOr(byte[] left, byte[] right)
        {
            if (left.Length != right.Length)
                throw new ArgumentException("Byte arrays length does not match");

            for (int i = 0; i < left.Length; ++i)
                left[i] |= right[i];
        }

        public static void ApplyXor(byte[] left, byte[] right)
        {
            if (left.Length != right.Length)
                throw new ArgumentException("Byte arrays length does not match");

            for (int i = 0; i < left.Length; ++i)
                left[i] ^= right[i];
        }

        public static void ApplyAnd(byte[] left, byte[] right)
        {
            if (left.Length != right.Length)
                throw new ArgumentException("Byte arrays length does not match");

            for (int i = 0; i < left.Length; ++i)
                left[i] &= right[i];
        }

        public static void ApplyNot(byte[] value)
        {
            for (int i = 0; i < value.Length; ++i)
                value[i] = (byte)~value[i];
        }

        public static byte[] Or(byte[] left, byte[] right)
        {
            byte[] result = (byte[])left.Clone();
            ApplyOr(result, right);
            return result;
        }

        public static byte[] Xor(byte[] left, byte[] right)
        {
            byte[] result = (byte[])left.Clone();
            ApplyXor(result, right);
            return result;
        }

        public static byte[] And(byte[] left, byte[] right)
        {
            byte[] result = (byte[])left.Clone();
            ApplyAnd(result, right);
            return result;
        }

        public static byte[] Not(byte[] value)
        {
            byte[] result = (byte[])value.Clone();
            ApplyNot(result);
            return result;
        }
    }
}
