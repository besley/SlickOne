using System;
using System.IO;
using System.Security.Cryptography;

namespace Synercoding.FormsAuthentication.Encryption
{
    // Mostly copied from: https://github.com/Microsoft/referencesource/blob/master/System.Web/Security/Cryptography/AspNetCryptoServiceProvider.cs
    internal sealed class AspNetCryptoServiceProvider
    {
        private readonly ICryptoAlgorithmFactory _cryptoAlgorithmFactory;
        private readonly IMasterKeyProvider _masterKeyProvider;
        
        private AspNetCryptoServiceProvider(ICryptoAlgorithmFactory cryptoAlgorithmFactory, IMasterKeyProvider masterKeyProvider)
        {
            _cryptoAlgorithmFactory = cryptoAlgorithmFactory;
            _masterKeyProvider = masterKeyProvider;
        }

        public ICryptoService GetCryptoService()
        {
            ICryptoService cryptoService = GetNetFXCryptoService();

            // always homogenize errors returned from the crypto service
            return new HomogenizingCryptoServiceWrapper(cryptoService);
        }

        private CryptographicKey _encryptionKey = null;
        private CryptographicKey _validationKey = null;

        private NetFXCryptoService GetNetFXCryptoService()
        {
            if (_encryptionKey == null)
                _encryptionKey = DeriveKey(_masterKeyProvider.GetEncryptionKey());
            if (_validationKey == null)
                _validationKey = DeriveKey(_masterKeyProvider.GetValidationKey()); 

            // and return the ICryptoService
            // (predictable IV turned on if the caller requested cacheable output)
            return new NetFXCryptoService(_cryptoAlgorithmFactory, _encryptionKey, _validationKey);
        }

        private static CryptographicKey DeriveKey(CryptographicKey keyDerivationKey)
        {
            using (HMACSHA512 hmac = CryptoAlgorithms.CreateHMACSHA512(keyDerivationKey.GetKeyMaterial()))
            {
                byte[] label, context;
                GetKeyDerivationParameters(out label, out context);

                byte[] derivedKey = DeriveKeyImpl(hmac, label, context, keyDerivationKey.KeyLength);
                return new CryptographicKey(derivedKey);
            }
        }

        private static void GetKeyDerivationParameters(out byte[] label, out byte[] context)
        {
            label = CryptoUtil.SecureUTF8Encoding.GetBytes("FormsAuthentication.Ticket");
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, CryptoUtil.SecureUTF8Encoding))
            {
                context = stream.ToArray();
            }
        }

        private static byte[] DeriveKeyImpl(HMAC hmac, byte[] label, byte[] context, int keyLengthInBits)
        {
            checked
            {
                int labelLength = (label != null) ? label.Length : 0;
                int contextLength = (context != null) ? context.Length : 0;
                byte[] buffer = new byte[4 /* [i]_2 */ + labelLength /* label */ + 1 /* 0x00 */ + contextLength /* context */ + 4 /* [L]_2 */];

                if (labelLength != 0)
                {
                    Buffer.BlockCopy(label, 0, buffer, 4, labelLength); // the 4 accounts for the [i]_2 length
                }
                if (contextLength != 0)
                {
                    Buffer.BlockCopy(context, 0, buffer, 5 + labelLength, contextLength); // the '5 +' accounts for the [i]_2 length, the label, and the 0x00 byte
                }
                WriteUInt32ToByteArrayBigEndian((uint)keyLengthInBits, buffer, 5 + labelLength + contextLength); // the '5 +' accounts for the [i]_2 length, the label, the 0x00 byte, and the context

                // Initialization

                int numBytesWritten = 0;
                int numBytesRemaining = keyLengthInBits / 8;
                byte[] output = new byte[numBytesRemaining];

                // Calculate each K_i value and copy the leftmost bits to the output buffer as appropriate.

                for (uint i = 1; numBytesRemaining > 0; i++)
                {
                    WriteUInt32ToByteArrayBigEndian(i, buffer, 0); // set the first 32 bits of the buffer to be the current iteration value
                    byte[] K_i = hmac.ComputeHash(buffer);

                    // copy the leftmost bits of K_i into the output buffer
                    int numBytesToCopy = Math.Min(numBytesRemaining, K_i.Length);
                    Buffer.BlockCopy(K_i, 0, output, numBytesWritten, numBytesToCopy);
                    numBytesWritten += numBytesToCopy;
                    numBytesRemaining -= numBytesToCopy;
                }

                // finished
                return output;
            }
        }

        private static void WriteUInt32ToByteArrayBigEndian(uint value, byte[] buffer, int offset)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value);
        }

        public static AspNetCryptoServiceProvider GetCryptoServiceProvider(FormsAuthenticationOptions options)
        {
            return new AspNetCryptoServiceProvider(
                new CryptoAlgorithmFactory(options),
                new MasterKeyProvider(options));
        }

    }
}