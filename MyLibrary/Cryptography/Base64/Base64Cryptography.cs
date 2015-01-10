using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Cryptography.Base64
{
    public static class Base64Cryptography
    {
        /// <summary>
        /// Base64 加密
        /// </summary>
        /// <param name="str">須加密的字串</param>
        /// <returns>加密後字串</returns>
        public static string Encrypt(string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64 解密
        /// </summary>
        /// <param name="str">須解密的字串</param>
        /// <returns>解密後的字串</returns>
        public static string Decrypt(string str)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }
    }
}
