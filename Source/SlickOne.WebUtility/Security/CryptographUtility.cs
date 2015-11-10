using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.WebUtility.Security
{
    public class CryptographUtility
    {
        /// <summary>  
        /// AES加密  
        /// </summary>  
        /// <param name="str">要加密字符串</param>  
        /// <returns>返回加密后字符串</returns>  
        public static String Encrypt_AES(String str, String strAesKey)  
        {      
            Byte[] keyArray = System.Text.UTF8Encoding.UTF8.GetBytes(strAesKey);      
            Byte[] toEncryptArray = System.Text.UTF8Encoding.UTF8.GetBytes(str);        
            System.Security.Cryptography.RijndaelManaged rDel = new System.Security.Cryptography.RijndaelManaged();      
            rDel.Key = keyArray;      
            rDel.Mode = System.Security.Cryptography.CipherMode.ECB;      
            rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;        
            System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateEncryptor();      
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);        
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);  
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="str">要解密字符串</param>  
        /// <returns>返回解密后字符串</returns>  
        public static String Decrypt_AES(String str, string strAesKey)  
        {      
            Byte[] keyArray = System.Text.UTF8Encoding.UTF8.GetBytes(strAesKey);      
            Byte[] toEncryptArray = Convert.FromBase64String(str);        
            System.Security.Cryptography.RijndaelManaged rDel = new System.Security.Cryptography.RijndaelManaged();      
            rDel.Key = keyArray;      
            rDel.Mode = System.Security.Cryptography.CipherMode.ECB;      
            rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;        
            System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateDecryptor();      
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);        
            return System.Text.UTF8Encoding.UTF8.GetString(resultArray);  
        }
    }
}
