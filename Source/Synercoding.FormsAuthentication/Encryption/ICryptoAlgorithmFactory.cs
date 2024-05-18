using System.Security.Cryptography;

namespace Synercoding.FormsAuthentication.Encryption
{
    // Copied from: https://github.com/Microsoft/referencesource/blob/master/System.Web/Security/Cryptography/ICryptoAlgorithmFactory.cs
    internal interface ICryptoAlgorithmFactory
    {

        // Gets a SymmetricAlgorithm instance that can be used for encryption / decryption
        SymmetricAlgorithm GetEncryptionAlgorithm();

        // Gets a KeyedHashAlgorithm instance that can be used for signing / validation
        KeyedHashAlgorithm GetValidationAlgorithm();

    }
}