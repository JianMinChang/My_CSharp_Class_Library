using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MyLibrary.Cryptography.AES
{
    public class CustomAESCryptography
    {
        public bool IsSuccess { private set; get; }
        public string ErrorMessage { private set; get; }

        private void Reset()
        {
            this.IsSuccess = false;
            this.ErrorMessage = string.Empty;
            this.Temp = string.Empty;
        }

        /// <summary>
        /// 設定對稱演算法的秘密金鑰
        /// </summary>
        public string PrivateKey { set; private get; }

        /// <summary>
        /// 設定對稱演算法的初始化向量
        /// </summary>
        public string IV { set; private get; }

        /// <summary>
        /// 指定要用來加密的區塊密碼模式
        /// </summary>
        public CipherMode CipherMode { set; private get; }

        /// <summary>
        /// 指定所要套用之填補的型別，當訊息資料區塊比密碼編譯作業所需的全部位元組數目要短時。
        /// </summary>
        public PaddingMode PaddingMode { set; private get; }

        private string Temp = string.Empty;

        private RijndaelManaged rDel = new RijndaelManaged();


        /// <summary>
        /// 對稱式AES加密
        /// </summary>
        /// <param name="strData">須加密的資料</param>
        /// <param name="strKey">加密的key</param>
        /// <returns></returns>
        public string Encrypt(string strData)
        {

            Reset();
            try
            {
                byte[] sourceBytes = UTF8Encoding.UTF8.GetBytes(strData);
                byte[] byte_pwdMD5 = UTF8Encoding.UTF8.GetBytes(GetMD5(this.PrivateKey));
                byte[] byte_ivMD5 = UTF8Encoding.UTF8.GetBytes(GetMD5(this.IV).Substring(0, 16));

                
                rDel.Key = byte_pwdMD5;
                rDel.IV = byte_ivMD5;
                rDel.Mode = this.CipherMode;
                rDel.Padding = this.PaddingMode;
                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length);

                this.Temp = Convert.ToBase64String(resultArray, 0, resultArray.Length);
                this.IsSuccess = true;
            }
            catch (Exception e)
            {
                this.IsSuccess = false;
                this.ErrorMessage = e.Message;

            }

            return this.Temp;
        }


        /// <summary>
        /// 對稱式AES解密
        /// </summary>
        /// <param name="toDecrypt">須解密的資料</param>
        /// <param name="key">加密的key</param>
        /// <returns></returns>
        public string Decrypt(string toDecrypt)
        {
            string temp = string.Empty;
            try
            {
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(GetMD5(this.PrivateKey));
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
                byte[] byte_ivMD5 = UTF8Encoding.UTF8.GetBytes(GetMD5(this.IV).Substring(0, 16));

                rDel.Key = keyArray;
                rDel.IV = byte_ivMD5;
                rDel.Mode = this.CipherMode;
                rDel.Padding = this.PaddingMode;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                this.Temp = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception e)
            {
                this.IsSuccess = false;
                this.ErrorMessage = e.Message;
            }
            return this.Temp;
        }

        private static byte[] GetBytes(string original)
        {
            return UTF8Encoding.UTF8.GetBytes(GetMD5(original));
        }

        private static string GetMD5(string original)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] b = md5.ComputeHash(Encoding.UTF8.GetBytes(original));
            return BitConverter.ToString(b).Replace("-", string.Empty).ToLower();
        }
    }

}
