using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace MyLibrary.Web
{

    /// <summary>
    /// WebRequest.Method
    /// </summary>
    public enum WebRequestMethod
    {
        Get = 0, Post = 1, Put = 2, Delete = 3, Head = 4, Trace = 5, Options = 6
    }


    public class CustomWebRequest
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { private set; get; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrorMessage { private set; get; }

        private WebRequestMethod _method = WebRequestMethod.Get;
        public WebRequestMethod Method
        {
            set
            {
                this._method = value;
            }
            private get
            {
                return this._method;
            }
        }

        private int _webTimeout = 20 * 1000;
        public int WebTimeout
        {
            set
            {
                this._webTimeout = value * 1000;
            }
            private get
            {
                return this._webTimeout;
            }
        }

        private StringBuilder strParame = new StringBuilder();
        private string strResult = string.Empty;

        /// <summary>
        /// 執行前初始化
        /// </summary>
        private void BeforeReset()
        {
            this.IsSuccess = false;
            this.ErrorMessage = string.Empty;
            this.strParame.Clear();
            this.strResult = string.Empty;
        }

        /// <summary>
        /// 執行後回初始值
        /// </summary>
        private void AfterReset()
        {
            this._method = WebRequestMethod.Get;
            this._webTimeout = 20 * 1000;
            this._webContentType = "application/x-www-form-urlencoded";
        }

        private string _webContentType = "application/x-www-form-urlencoded";
        public string _WebContentType
        {
            set
            {
                this._webContentType = value;
            }
            private get
            {
                return this._webContentType;
            }
        }



        public string SendAction(string strURL, IDictionary<string, string> Parms)
        {
            BeforeReset();




            foreach (var item in Parms)
            {
                strParame.Append(item.Key + "=" + item.Value + "&");
            }
            string strtemp = string.Empty;
            strtemp = strParame.ToString().TrimEnd('&');

            byte[] postData = null;

            //Get時需要
            if (this.Method == WebRequestMethod.Get)
            {
                strURL += "?" + strtemp;
            }
            else
            {
                postData = Encoding.UTF8.GetBytes(strtemp);
            }



            WebRequest request = WebRequest.Create(strURL);
            request.Method = this._method.ToString();
            request.ContentType = this._webContentType;
            request.Timeout = this._webTimeout;
            request.ContentLength = postData == null ? 0 : postData.Length;


            try
            {
                if (this.Method != WebRequestMethod.Get)
                {
                    using (Stream st = request.GetRequestStream())
                    {
                        st.Write(postData, 0, postData.Length);
                    }
                }


                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        strResult = sr.ReadToEnd();
                    }
                }
                this.IsSuccess = true;
            }
            catch (Exception e)
            {
                this.IsSuccess = false;
                this.ErrorMessage = e.Message;
            }


            AfterReset();
            return this.strResult;
        }
    }
}
