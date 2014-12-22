using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MyLibrary.Mobile
{
    public class OSDetail
    {

        private static string IOSRegex = @"iPhone OS [0-9]_[0-9]_[0-9]|iPhone OS [0-9]_[0-9]";
        private static string AndroidRegex = @"Android [0-9].[0-9].[0-9]|Android [0-9].[0-9]|Android[0-9].[0-9].[0-9]|Android[0-9].[0-9]";
        private static string WindowPhoneRegex = @"Windows Phone [0-9].[0-9]";

        private string GetOSString(string input)
        {
            return Regex.Match(input, @"(" + IOSRegex.ToString() + "|" + AndroidRegex.ToString() + "|" + WindowPhoneRegex.ToString() + ")").Value;
        }

        /// <summary>
        /// 取得行動裝置的作業系統名稱
        /// </summary>
        /// <param name="UserAgent">UserAgent</param>
        /// <returns>作業系統名稱</returns>
        public string GetMobileOS(string UserAgent)
        {
            string temp = GetOSString(UserAgent);

            if (!string.IsNullOrEmpty(temp))
            {
                return Regex.Match(temp, @"(iPhone OS|Android|Windows Phone)").Value.Replace(" OS", string.Empty);
            }
            else
            {
                return "Other";
            }
        }

        /// <summary>
        /// 取得行動裝置的作業系統版本號
        /// </summary>
        /// <param name="UserAgent">UserAgent</param>
        /// <returns>版本號</returns>
        public string GetMobileOSVersion(string UserAgent)
        {
            string temp = GetOSString(UserAgent);

            if (!string.IsNullOrEmpty(temp))
            {
                return Regex.Match(temp, @"([0-9]_[0-9]_[0-9]|[0-9]_[0-9]|[0-9].[0-9].[0-9]|[0-9].[0-9])").Value.Replace('_', '.');
            }
            else
            {
                return string.Empty;
            }
        }


    }
}
