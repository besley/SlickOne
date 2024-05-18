using System;

namespace Synercoding.FormsAuthentication.Encryption
{
    // Lossly based upon: https://github.com/Microsoft/referencesource/blob/master/System.Web/Security/Cryptography/MachineKeyMasterKeyProvider.cs
    // Removed auto-gen functionality and replaced config requirement
    internal sealed class MasterKeyProvider : IMasterKeyProvider
    {
        private CryptographicKey _encryptionKey;
        private readonly FormsAuthenticationOptions _options;
        private CryptographicKey _validationKey;

        // the only required parameter is 'machineKeySection'; other parameters are just used for unit testing
        internal MasterKeyProvider(FormsAuthenticationOptions machineKeySection)
        {
            _options = machineKeySection;
        }

        private CryptographicKey GenerateCryptographicKey(string configAttributeValue)
        {
            byte[] keyMaterial = CryptoUtil.HexToBinary(configAttributeValue);

            // If <machineKey> contained a valid key, just use it verbatim.
            if (keyMaterial != null && keyMaterial.Length > 0)
            {
                return new CryptographicKey(keyMaterial);
            }

            throw new Exception();
        }

        public CryptographicKey GetEncryptionKey()
        {
            if (_encryptionKey == null)
                _encryptionKey = GenerateCryptographicKey(_options.DecryptionKey);
            return _encryptionKey;
        }

        public CryptographicKey GetValidationKey()
        {
            if (_validationKey == null)
                _validationKey = GenerateCryptographicKey(_options.ValidationKey);
            return _validationKey;
        }

    }
}