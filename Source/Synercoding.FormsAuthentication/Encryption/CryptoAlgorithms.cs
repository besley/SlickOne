using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace Synercoding.FormsAuthentication.Encryption
{
    // taken from: https://github.com/Microsoft/referencesource/blob/master/System.Web/Security/Cryptography/CryptoAlgorithms.cs
    internal static class CryptoAlgorithms
    {
        internal static Aes CreateAes()
        {
            return Aes.Create();
        }

        [SuppressMessage("Microsoft.Security.Cryptography", "CA5354:SHA1CannotBeUsed", Justification = @"This is only used by legacy code; new features do not use this algorithm.")]
        internal static HMACSHA1 CreateHMACSHA1()
        {
            return new HMACSHA1();
        }

        internal static HMACSHA256 CreateHMACSHA256()
        {
            return new HMACSHA256();
        }

        internal static HMACSHA384 CreateHMACSHA384()
        {
            return new HMACSHA384();
        }

        internal static HMACSHA512 CreateHMACSHA512()
        {
            return new HMACSHA512();
        }

        internal static HMACSHA512 CreateHMACSHA512(byte[] key)
        {
            return new HMACSHA512(key);
        }

        internal static SHA256 CreateSHA256()
        {
            return SHA256.Create();
        }

        [SuppressMessage("Microsoft.Cryptographic.Standard", "CA5353:TripleDESCannotBeUsed", Justification = @"This is only used by legacy code; new features do not use this algorithm.")]
        [Obsolete("3DES is deprecated and MUST NOT be used by new features. Consider using AES instead.")]
        internal static TripleDES CreateTripleDES()
        {
            return TripleDES.Create();
        }

    }
}