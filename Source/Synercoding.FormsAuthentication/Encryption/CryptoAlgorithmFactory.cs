using System;
using System.IO;
using System.Security.Cryptography;

namespace Synercoding.FormsAuthentication.Encryption
{
    // based upon: https://github.com/Microsoft/referencesource/blob/master/System.Web/Security/Cryptography/MachineKeyCryptoAlgorithmFactory.cs
    internal sealed class CryptoAlgorithmFactory : ICryptoAlgorithmFactory
    {

        private readonly FormsAuthenticationOptions _options;
        private Func<SymmetricAlgorithm> _encryptionAlgorithmFactory;
        private Func<KeyedHashAlgorithm> _validationAlgorithmFactory;

        public CryptoAlgorithmFactory(FormsAuthenticationOptions options)
        {
            _options = options;
        }

        public SymmetricAlgorithm GetEncryptionAlgorithm()
        {
            if (_encryptionAlgorithmFactory == null)
            {
                _encryptionAlgorithmFactory = GetEncryptionAlgorithmFactory();
            }
            return _encryptionAlgorithmFactory();
        }

        private Func<SymmetricAlgorithm> GetEncryptionAlgorithmFactory()
        {
            switch (_options.EncryptionMethod)
            {
                case EncryptionMethod.AES:
                    return CryptoAlgorithms.CreateAes;
                case EncryptionMethod.TripleDES:
                    return CryptoAlgorithms.CreateTripleDES;
                default:
                    throw new InvalidDataException();
            }
        }

        public KeyedHashAlgorithm GetValidationAlgorithm()
        {
            if (_validationAlgorithmFactory == null)
            {
                _validationAlgorithmFactory = GetValidationAlgorithmFactory();
            }
            return _validationAlgorithmFactory();
        }

        private Func<KeyedHashAlgorithm> GetValidationAlgorithmFactory()
        {
            switch (_options.ValidationMethod)
            {
                case ValidationMethod.SHA1:
                    return CryptoAlgorithms.CreateHMACSHA1;
                case ValidationMethod.HMACSHA256:
                    return CryptoAlgorithms.CreateHMACSHA256;
                case ValidationMethod.HMACSHA384:
                    return CryptoAlgorithms.CreateHMACSHA384;
                case ValidationMethod.HMACSHA512:
                    return CryptoAlgorithms.CreateHMACSHA512;
                default:
                    throw new InvalidDataException();
            }
        }
    }
}