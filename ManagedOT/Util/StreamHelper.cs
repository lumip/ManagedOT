﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ManagedOT.Util
{
    internal static class StreamHelper
    {
        public static byte[] Read(this Stream stream, int count)
        {
            byte[] buffer = new byte[count];
            int offset = 0;

            while (offset < count)
            {
                int readBytes = stream.Read(buffer, offset, count - offset);
                offset += readBytes;
            }

            return buffer;
        }

        public static async Task<byte[]> ReadAsync(this Stream stream, int count)
        {
            byte[] buffer = new byte[count];
            int offset = 0;

            while (offset < count)
            {
                int readBytes = await stream.ReadAsync(buffer, offset, count - offset);
                offset += readBytes;
            }

            return buffer;
        }

        public static async Task<int> ReadByteAsync(this Stream stream)
        {
            byte[] buffer = new byte[1];
            int readBytes = await stream.ReadAsync(buffer, 0, 1);
            if (readBytes > 0)
                return buffer[0];

            return -1;
        }
        
        public static void Write(this Stream stream, byte[] buffer)
        {
            stream.Write(buffer, 0, buffer.Length);
        }

        public static Task WriteAsync(this Stream stream, byte[] buffer)
        {
            return stream.WriteAsync(buffer, 0, buffer.Length);
        }

        public static Task WriteByteAsync(this Stream stream, byte value)
        {
            return stream.WriteAsync(new[] { value }, 0, 1);
        }
    }
}
