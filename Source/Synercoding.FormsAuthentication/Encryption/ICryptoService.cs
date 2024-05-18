namespace Synercoding.FormsAuthentication.Encryption
{
    // Copied from: https://github.com/Microsoft/referencesource/blob/master/System.Web/Security/Cryptography/ICryptoService.cs
    internal interface ICryptoService
    {

        // Protects some data by applying appropriate cryptographic transformations to it.
        byte[] Protect(byte[] clearData);

        // Returns the unprotected form of some protected data by validating and undoing the cryptographic transformations that led to it.
        byte[] Unprotect(byte[] protectedData);

    }
}