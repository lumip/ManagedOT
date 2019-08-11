using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ManagedOT.Cryptography
{
    public class SHA256Provider : HashAlgorithmProvider
    {
        public override HashAlgorithm Create()
        {
            return SHA256.Create();
        }
    }
}
