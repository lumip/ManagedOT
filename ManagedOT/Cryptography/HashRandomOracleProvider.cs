using System;
using System.Collections.Generic;
using System.Text;

namespace ManagedOT.Cryptography
{
    public class HashRandomOracleProvider
    {
        private HashAlgorithmProvider _hashProvider;

        public HashRandomOracleProvider(HashAlgorithmProvider hashProvider)
        {
            if (hashProvider == null)
                new ArgumentNullException(nameof(hashProvider));
            _hashProvider = hashProvider;
        }

        public HashRandomOracle Create()
        {
            return new HashRandomOracle(_hashProvider.CreateThreadsafe());
        }
    }
}
