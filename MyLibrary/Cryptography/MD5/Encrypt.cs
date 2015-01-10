using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MyLibrary.Cryptography.MD5
{
    public class MD5Cryptography
    {
        /// <summary>
        /// MD5加密算法(無加額外單字)
        /// </summary>
        /// <param name="str">待加密的字串</param>
        /// <returns>加密後的字串</returns>
        public static string MD5Encrypt(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }

    }
}
