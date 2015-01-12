using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace MyLibrary.Cryptography.RSA
{

    public enum CustomGetFile
    {
        VoucherFileName, VoucherFilePath
    }

    public class RSACryptography
    {
        private string VoucherPath = string.Empty;

        private string GetFilePath(string VoucherPathOrFielName)
        {
            string tmp = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            string Path = tmp.Remove(0, 8).Replace("MyLibrary.DLL", VoucherPathOrFielName);

            return Path.ToString();
        }


        /// <summary>
        /// 用私鑰取得RSA的Key
        /// </summary>
        /// <param name="VoucherPath">憑證路徑</param>
        /// <param name="Password">密碼</param>
        /// <param name="Flags">X509KeyStorageFlags</param>
        public RSACryptography(string VoucherPathOrFielName, CustomGetFile Pattern, string Password, X509KeyStorageFlags Flags)
        {
            Reset();

            if (Pattern == CustomGetFile.VoucherFilePath)
            {
                this.VoucherPath = VoucherPathOrFielName;
            }
            else
            {
                this.VoucherPath =GetFilePath( VoucherPathOrFielName);
            }
           
            
            
            string tmp_PublicKey = string.Empty;
            string tmp_PrivateKey = string.Empty;
            
            try
            {
                X509Certificate2 cert = new X509Certificate2(VoucherPath, Password, Flags);

                tmp_PublicKey = cert.PublicKey.Key.ToXmlString(false);
                tmp_PrivateKey = cert.PrivateKey.ToXmlString(true);
            }
            catch (CryptographicException e)
            {
                this.ErrorMessage = e.Message;
            }
            this._PublicKey = tmp_PublicKey;
            this._PrivateKey = tmp_PrivateKey;
        }

        /// <summary>
        /// 用公鑰取得RSA的Key
        /// </summary>
        /// <param name="VoucherPath">憑證路徑</param>
        public RSACryptography(string VoucherPathOrFielName, CustomGetFile Pattern)
        {
            Reset();

            if (Pattern == CustomGetFile.VoucherFilePath)
            {
                this.VoucherPath = VoucherPathOrFielName;
            }
            else
            {
                this.VoucherPath = GetFilePath(VoucherPathOrFielName);
            }

            string tmp_PublicKey = string.Empty;
            string tmp_PrivateKey = string.Empty;

            try
            {
                X509Certificate2 cert = new X509Certificate2(VoucherPath);
                tmp_PublicKey = cert.PublicKey.Key.ToXmlString(false);
                tmp_PrivateKey = string.Empty;
            }
            catch (CryptographicException e)
            {
                this.ErrorMessage = e.Message;
            }
             this._PublicKey = tmp_PublicKey;
             this._PrivateKey = tmp_PrivateKey;
        }

        /// <summary>
        /// 初始化直接給Key值
        /// </summary>
        /// <param name="PublicKey">公鑰的XmlString</param>
        /// <param name="PrivateKey">私鑰的XmlString</param>
        public RSACryptography(string PublicKey, string PrivateKey)
        {
            this._PublicKey = PublicKey;
            this._PrivateKey = PrivateKey;
        }

        /// <summary>
        /// 用RSAParameters初始化給Key值
        /// </summary>
        /// <param name="parameters"></param>
        public RSACryptography(RSAParameters parameters)
        {
            try
            {
                this._rsaCrypto.ImportParameters(parameters);
                this._PublicKey= this._rsaCrypto.ToXmlString(false);
                this._PrivateKey = this._rsaCrypto.ToXmlString(true);
            }
            catch (CryptographicException ex)
            {
                this.ErrorMessage = ex.Message;
                this._PublicKey = string.Empty;
                this._PrivateKey = string.Empty;
            }
        }


        private RSACryptoServiceProvider _rsaCrypto = new RSACryptoServiceProvider();

        public bool IsSuccess { private set; get; }

        public string ErrorMessage { private set; get; }

        private UnicodeEncoding ByteConverter = new UnicodeEncoding();

        private string _PrivateKey { set; get; }

        private string _PublicKey { set; get; }

        private void Reset()
        {
            this.IsSuccess = true;
            this.ErrorMessage = string.Empty;
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="DataToEncrypt">須加密的字串</param>
        /// <returns>加密後的字串</returns>
        public string RSAEncrypt(string DataToEncrypt)
        {
            Reset();

            if (this._PublicKey == string.Empty)
            {
                this.ErrorMessage = "No PublicKey";
                return null;
            }
            else
            {
                try
                {
                    byte[] encryptedData;
                    encryptedData = encryptor(ByteConverter.GetBytes(DataToEncrypt));
                    return Convert.ToBase64String(encryptedData);
                }
                catch (CryptographicException e)
                {
                    this.ErrorMessage = e.Message;
                    return null;
                }
            }
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="DataToDecrypt">須解密的字串</param>
        /// <returns>解密後字串</returns>
        public string RSADecrypt(string DataToDecrypt)
        {
            Reset();
            string word = string.Empty;

            if (this._PrivateKey == string.Empty)
            {
                this.ErrorMessage = "No PrivateKey";
                return string.Empty;
            }
            else
            {
                try
                {
                    byte[] decryptedData;

                    decryptedData = decryptor(Convert.FromBase64String(DataToDecrypt));
                    word = ByteConverter.GetString(decryptedData);
                }
                catch (CryptographicException e)
                {
                    this.ErrorMessage = e.Message;
                }

                if (this.ErrorMessage == string.Empty)
                {
                    return word;
                }
                else
                {
                    return string.Empty;
                }
            }
        }


        private byte[] encryptor(byte[] OriginalData)
        {
            if (OriginalData == null || OriginalData.Length <= 0)
            {
                throw new NotSupportedException();
            }
            if (this._rsaCrypto == null)
            {
                throw new ArgumentNullException();
            }
            this._rsaCrypto.FromXmlString(this._PublicKey);

            int bufferSize = (this._rsaCrypto.KeySize / 8) - 11;
            byte[] buffer = new byte[bufferSize];
            //分段加密
            using (MemoryStream input = new MemoryStream(OriginalData))
            using (MemoryStream ouput = new MemoryStream())
            {
                while (true)
                {
                    int readLine = input.Read(buffer, 0, bufferSize);
                    if (readLine <= 0)
                    {
                        break;
                    }
                    byte[] temp = new byte[readLine];
                    Array.Copy(buffer, 0, temp, 0, readLine);
                    byte[] encrypt = this._rsaCrypto.Encrypt(temp, false);
                    ouput.Write(encrypt, 0, encrypt.Length);
                }
                return ouput.ToArray();
            }
        }

        private byte[] decryptor(byte[] EncryptDada)
        {
            if (EncryptDada == null || EncryptDada.Length <= 0)
            {
                throw new NotSupportedException();
            }

            this._rsaCrypto.FromXmlString(this._PrivateKey);
            int keySize = this._rsaCrypto.KeySize / 8; byte[] buffer = new byte[keySize];

            using (MemoryStream input = new MemoryStream(EncryptDada))
            using (MemoryStream output = new MemoryStream())
            {
                while (true)
                {
                    int readLine = input.Read(buffer, 0, keySize);
                    if (readLine <= 0)
                    {
                        break;
                    }
                    byte[] temp = new byte[readLine];
                    Array.Copy(buffer, 0, temp, 0, readLine);
                    byte[] decrypt = _rsaCrypto.Decrypt(temp, false);
                    output.Write(decrypt, 0, decrypt.Length);
                }
                return output.ToArray();
            }
        }




    }
}
