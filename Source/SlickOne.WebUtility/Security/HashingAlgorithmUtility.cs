using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SlickOne.WebUtility.Security
{
    /// <summary>
    /// Hash算法Provider
    /// </summary>
    public enum EnumHashProvider
    {
        MD5CryptoServiceProvider = 1,
        RIPEMD160Managed,
        SHA1CryptoServiceProvider,
        SHA1Managed,
        SHA256Managed,
        SHA384Managed,
        SHA512Managed
    }

    /// <summary>
    /// Hash算法加密工具类
    /// </summary>
    public class HashingAlgorithmUtility
    {
        public static string GetEncryptedHashText(string plainText, out string saltText, out EnumHashProvider hashProvider)
        {
            Random random = new Random();
            int hashProviderType = random.Next(1, 7);
            hashProvider = (EnumHashProvider)hashProviderType;
            saltText = CreateSaltText();
            plainText += saltText;

            string encryptedText = ComputeHash(hashProvider, plainText);
            return encryptedText;
        }

        /// <summary>
        /// 创建Hash算法
        /// </summary>
        /// <param name="hashProvider"></param>
        /// <returns></returns>
        internal static HashAlgorithm CreateHashAlgorithm(EnumHashProvider hashProvider)
        {
            HashAlgorithm hashAlgorithm = null;
            switch (hashProvider)
            {
                case EnumHashProvider.MD5CryptoServiceProvider:
                    hashAlgorithm = new MD5CryptoServiceProvider();
                    break;
                case EnumHashProvider.RIPEMD160Managed:
                    hashAlgorithm = new RIPEMD160Managed();
                    break;
                case EnumHashProvider.SHA1CryptoServiceProvider:
                    hashAlgorithm = new SHA1CryptoServiceProvider();
                    break;
                case EnumHashProvider.SHA1Managed:
                    hashAlgorithm = new SHA1Managed();
                    break;
                case EnumHashProvider.SHA256Managed:
                    hashAlgorithm = new SHA256CryptoServiceProvider();
                    break;
                case EnumHashProvider.SHA384Managed:
                    hashAlgorithm = new SHA384CryptoServiceProvider();
                    break;
                case EnumHashProvider.SHA512Managed:
                    hashAlgorithm = new SHA512CryptoServiceProvider();
                    break;
            }
            return hashAlgorithm;
        }

        /// <summary>
        /// 计算Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="hashProvider"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string ComputeHash(EnumHashProvider hashProvider, string plainText)
        {
            var provider = CreateHashAlgorithm(hashProvider);
            var hashedBytes = provider.ComputeHash(Encoding.UTF8.GetBytes(plainText));
            var hashedText = Convert.ToBase64String(hashedBytes);
            return hashedText;
        }

        /// <summary>
        /// 比较Hash(明文)和密文是否相等
        /// </summary>
        /// <param name="hashProvider"></param>
        /// <param name="plainText"></param>
        /// <param name="saltText"></param>
        /// <param name="encrytedText"></param>
        /// <returns></returns>
        public static bool CompareHash(int hashProvider, string plainText, string saltText, string encrytedText)
        {
            var plainContent = plainText + saltText;
            var provider = (EnumHashProvider)hashProvider;
            string computedText = ComputeHash(provider, plainContent);

            return (computedText == encrytedText);
        }

        /// <summary>
        /// 创建Salt文本
        /// </summary>
        /// <returns></returns>
        private static string CreateSaltText()
        {
            byte[] bytSalt = new byte[32];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytSalt);

            return Convert.ToBase64String(bytSalt);
        }

    }
}