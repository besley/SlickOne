using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Synercoding.FormsAuthentication.Encryption
{
    // Most taken from https://github.com/Microsoft/referencesource/blob/master/System.Web/Security/Cryptography/CryptoUtil.cs
    internal class CryptoUtil
    {
        /// <summary>
        /// Similar to Encoding.UTF8, but throws on invalid bytes. Useful for security routines where we need
        /// strong guarantees that we're always producing valid UTF8 streams.
        /// </summary>
        public static readonly UTF8Encoding SecureUTF8Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

        /// <summary>
        /// Converts a byte array into its hexadecimal representation.
        /// </summary>
        /// <param name="data">The binary byte array.</param>
        /// <returns>The hexadecimal (uppercase) equivalent of the byte array.</returns>
        public static string BinaryToHex(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            char[] hex = new char[checked(data.Length * 2)];

            for (int i = 0; i < data.Length; i++)
            {
                byte thisByte = data[i];
                hex[2 * i] = NibbleToHex((byte)(thisByte >> 4)); // high nibble
                hex[2 * i + 1] = NibbleToHex((byte)(thisByte & 0xf)); // low nibble
            }

            return new string(hex);
        }

        /// <summary>
        /// Converts a hexadecimal string into its binary representation.
        /// </summary>
        /// <param name="data">The hex string.</param>
        /// <returns>The byte array corresponding to the contents of the hex string,
        /// or null if the input string is not a valid hex string.</returns>
        public static byte[] HexToBinary(string data)
        {
            if (data == null || data.Length % 2 != 0)
            {
                // input string length is not evenly divisible by 2
                return null;
            }

            byte[] binary = new byte[data.Length / 2];

            for (int i = 0; i < binary.Length; i++)
            {
                int highNibble = HexToInt(data[2 * i]);
                int lowNibble = HexToInt(data[2 * i + 1]);

                if (highNibble == -1 || lowNibble == -1)
                {
                    return null; // bad hex data
                }
                binary[i] = (byte)((highNibble << 4) | lowNibble);
            }

            return binary;
        }

        // Determines if two buffer instances are equal, e.g. whether they contain the same payload. This method
        // is written in such a manner that it should take the same amount of time to execute regardless of
        // whether the result is success or failure. The modulus operation is intended to make the check take the
        // same amount of time, even if the buffers are of different lengths.
        //
        // !! DO NOT CHANGE THIS METHOD WITHOUT SECURITY 
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static bool BuffersAreEqual(byte[] buffer1, int buffer1Offset, int buffer1Count, byte[] buffer2, int buffer2Offset, int buffer2Count)
        {
            bool success = (buffer1Count == buffer2Count); // can't possibly be successful if the buffers are of different lengths
            for (int i = 0; i < buffer1Count; i++)
            {
                success &= (buffer1[buffer1Offset + i] == buffer2[buffer2Offset + (i % buffer2Count)]);
            }
            return success;
        }

        /// <summary>
        /// Returns an IV that's based solely on the contents of a buffer; useful for generating
        /// predictable IVs for ciphertexts that need to be cached. The output value is only
        /// appropriate for use as an IV and must not be used for any other purpose.
        /// </summary>
        /// <remarks>This method uses an iterated unkeyed SHA256 to calculate the IV.</remarks>
        /// <param name="buffer">The input buffer over which to calculate the IV.</param>
        /// <param name="ivBitLength">The requested length (in bits) of the IV to generate.</param>
        /// <returns>The calculated IV.</returns>
        public static byte[] CreatePredictableIV(byte[] buffer, int ivBitLength)
        {
            byte[] output = new byte[ivBitLength / 8];
            int bytesCopied = 0;
            int bytesRemaining = output.Length;

            using (SHA256 sha256 = CryptoAlgorithms.CreateSHA256())
            {
                while (bytesRemaining > 0)
                {
                    byte[] hashed = sha256.ComputeHash(buffer);

                    int bytesToCopy = Math.Min(bytesRemaining, hashed.Length);
                    Buffer.BlockCopy(hashed, 0, output, bytesCopied, bytesToCopy);

                    bytesCopied += bytesToCopy;
                    bytesRemaining -= bytesToCopy;

                    buffer = hashed; // next iteration (if it occurs) will operate over the block just hashed
                }
            }

            return output;
        }

        // Taken from https://github.com/Microsoft/referencesource/blob/master/System.Web/Util/HttpEncoderUtility.cs
        private static int HexToInt(char h)
        {
            return (h >= '0' && h <= '9') ? h - '0' :
                (h >= 'a' && h <= 'f') ? h - 'a' + 10 :
                (h >= 'A' && h <= 'F') ? h - 'A' + 10 :
                -1;
        }

        // converts a nibble (4 bits) to its uppercase hexadecimal character representation [0-9, A-F]
        private static char NibbleToHex(byte nibble)
        {
            return (char)((nibble < 10) ? (nibble + '0') : (nibble - 10 + 'A'));
        }
    }
}