using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagedOT.UnitTests
{
    public class ConstantRandomOracle : RandomOracle
    {
        private byte[] _invokeResponse;

        public ConstantRandomOracle(byte[] invokeResponse)
        {
            _invokeResponse = invokeResponse;
        }
        
        public override IEnumerable<byte> Invoke(byte[] query)
        {
            return _invokeResponse;
        }
    }
}
