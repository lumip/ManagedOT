using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagedOT
{
    public interface IMessageChannel
    {
        Task<byte[]> ReadMessageAsync();
        Task WriteMessageAsync(byte[] message);
    }
}
