using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MyLibrary.Cryptography.AES
{
    public class AESCryptography
    {
        /// <summary>
        /// 對稱式AES加密
        /// </summary>
        /// <param name="strData">須加密的資料</param>
        /// <param name="strKey">加密的key</param>
        /// <returns></returns>
        public static string encrypt(string strData,string strKey )
        {
            byte[] sourceBytes = UTF8Encoding.UTF8.GetBytes(strData);
            byte[] byte_pwdMD5 = UTF8Encoding.UTF8.GetBytes(strKey);


            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = byte_pwdMD5;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.ANSIX923;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }


        /// <summary>
        /// 對稱式AES解密
        /// </summary>
        /// <param name="toDecrypt">須解密的資料</param>
        /// <param name="key">加密的key</param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.ANSIX923;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        private static string GetMD5(string original)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] b = md5.ComputeHash(Encoding.UTF8.GetBytes(original));
            return BitConverter.ToString(b).Replace("-", string.Empty).ToLower();
        }
    }
}
