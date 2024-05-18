using System;
using System.Security.Cryptography;

namespace Synercoding.FormsAuthentication.Encryption
{
    // Based upon: https://github.com/Microsoft/referencesource/blob/master/System.Web/Security/Cryptography/HomogenizingCryptoServiceWrapper.cs
    internal sealed class HomogenizingCryptoServiceWrapper : ICryptoService
    {

        public HomogenizingCryptoServiceWrapper(ICryptoService wrapped)
        {
            WrappedCryptoService = wrapped;
        }

        internal ICryptoService WrappedCryptoService
        {
            get;
            private set;
        }

        private static byte[] HomogenizeErrors(Func<byte[], byte[]> func, byte[] input)
        {
            // If the underlying method returns null or throws an exception, the
            // error will be homogenized as a single CryptographicException.

            byte[] output = null;

            try
            {
                output = func(input);
                return output;
            }
            finally
            {
                if (output == null)
                {
                    throw new CryptographicException();
                }
            }
        }

        public byte[] Protect(byte[] clearData)
        {
            return HomogenizeErrors(WrappedCryptoService.Protect, clearData);
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            return HomogenizeErrors(WrappedCryptoService.Unprotect, protectedData);
        }

    }
}