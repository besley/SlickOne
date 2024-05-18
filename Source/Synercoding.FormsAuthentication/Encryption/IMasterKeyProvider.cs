namespace Synercoding.FormsAuthentication.Encryption
{
    // Copied from: https://github.com/Microsoft/referencesource/blob/master/System.Web/Security/Cryptography/IMasterKeyProvider.cs
    internal interface IMasterKeyProvider
    {

        // encryption + decryption key
        CryptographicKey GetEncryptionKey();

        // signing + validation key
        CryptographicKey GetValidationKey();

    }
}