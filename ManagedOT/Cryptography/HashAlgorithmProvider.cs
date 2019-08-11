using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace ManagedOT.Cryptography
{
    public abstract class HashAlgorithmProvider
    {
        public abstract HashAlgorithm Create();
        public ThreadsafeHashAlgorithm CreateThreadsafe()
        {
            return new ThreadsafeHashAlgorithm(Create());
        }
    }
}
