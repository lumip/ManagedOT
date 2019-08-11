using System;
using System.IO;
using System.Security.Cryptography;

namespace ManagedOT.Cryptography
{
    public class ThreadsafeHashAlgorithm : IDisposable
    {
        private HashAlgorithm _hashAlgorithm;
        private object _lock;

        internal ThreadsafeHashAlgorithm(HashAlgorithm hashAlgorithm)
        {
            if (hashAlgorithm == null)
                throw new ArgumentNullException(nameof(hashAlgorithm));
            _hashAlgorithm = hashAlgorithm;
            _lock = new object();
        }

        public byte[] ComputeHash(byte[] buffer)
        {
            lock (_lock)
            {
                return _hashAlgorithm.ComputeHash(buffer);
            }
        }

        public byte[] ComputeHash(Stream inputStream)
        {
            lock (_lock)
            {
                return _hashAlgorithm.ComputeHash(inputStream);
            }
        }

        public void Dispose()
        {
            lock (_lock)
            {
                _hashAlgorithm.Dispose();
            }
        }
        
        ~ThreadsafeHashAlgorithm()
        {
            Dispose();
        }
    }
}