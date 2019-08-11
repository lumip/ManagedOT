using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ManagedOT.Cryptography
{
    public sealed class CryptoContext : IDisposable
    {
        private RandomNumberGenerator _randomNumberGenerator;
        private HashAlgorithmProvider _hashAlgorithmProvider;

        public CryptoContext(RandomNumberGenerator randomNumberGenerator, HashAlgorithmProvider hashAlgorithmProvider)
        {
            if (randomNumberGenerator == null)
                throw new ArgumentNullException(nameof(randomNumberGenerator));

            if (hashAlgorithmProvider == null)
                throw new ArgumentNullException(nameof(hashAlgorithmProvider));

            _randomNumberGenerator = randomNumberGenerator;
            _hashAlgorithmProvider = hashAlgorithmProvider;
        }

        public static CryptoContext CreateDefault()
        {
            return new CryptoContext(
                RandomNumberGenerator.Create(),
                new SHA256Provider()
            );
        }

        public void Dispose()
        {
            _randomNumberGenerator.Dispose();
        }

        public RandomNumberGenerator RandomNumberGenerator
        {
            get
            {
                return _randomNumberGenerator;
            }
        }

        public HashAlgorithmProvider HashAlgorithmProvider
        {
            get
            {
                return _hashAlgorithmProvider;
            }
        }
    }
}
