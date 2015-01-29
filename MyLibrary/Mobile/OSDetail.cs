using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MyLibrary.Mobile
{
    public static class OSDetail
    {
        private static string IOSRegex = @"iPhone OS [0-9]_[0-9]_[0-9]|iPhone OS [0-9]_[0-9]|iPad; CPU OS [0-9]_[0-9]_[0-9]|iPad; CPU OS [0-9]_[0-9]|iPad;U;CPU OS [0-9]_[0-9]_[0-9]|iPad;U;CPU OS [0-9]_[0-9]|iPad; U; CPU OS [0-9]_[0-9]_[0-9]|iPad; U; CPU OS [0-9]_[0-9]";
        private static string AndroidRegex = @"Android [0-9].[0-9].[0-9]|Android [0-9].[0-9]|Android[0-9].[0-9].[0-9]|Android[0-9].[0-9]";
        private static string WindowPhoneRegex = @"Windows Phone [0-9].[0-9]";
        private static string DeviceRegex = @"mobile|Mobile|iPad|IPAD";

        private static string iOSosRegex = @"iPhone OS|iPhone OS|iPad; CPU OS|iPad; CPU OS|iPad;U;CPU OS|iPad;U;CPU OS|iPad; U; CPU OS|iPad; U; CPU OS";
        private static string AndroidosRegex = @"Android";
        private static string WindowPhoneosRegex = @"Windows Phone";


        private static string GetOSString(string input)
        {
            return Regex.Match(input, @"(" + IOSRegex.ToString() + "|" + AndroidRegex.ToString() + "|" + WindowPhoneRegex.ToString() + ")").Value;
        }

        /// <summary>
        /// 取得行動裝置的作業系統名稱
        /// </summary>
        /// <param name="UserAgent">UserAgent</param>
        /// <returns>作業系統名稱</returns>
        public static string GetMobileOS(string UserAgent)
        {
            string temp = GetOSString(UserAgent);

            if (!string.IsNullOrEmpty(temp))
            {
                return Regex.Replace(Regex.Match(temp, @"(" + iOSosRegex + "|" + AndroidosRegex + "|" + WindowPhoneosRegex + ")").Value, iOSosRegex, "iOS");
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
        public static string GetMobileOSVersion(string UserAgent)
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

        /// <summary>
        /// 識別Device是(iPad or Mobile)
        /// </summary>
        /// <param name="UserAgent">UserAgent</param>
        /// <returns>iPad or Mobile</returns>
        public static string GetDeviceStatus(string UserAgent)
        {
            string temp = GetOSString(UserAgent);

            if (!string.IsNullOrEmpty(temp))
            {
                if (Regex.Match(temp, DeviceRegex).Success)
                {
                    return Regex.Match(temp, DeviceRegex).Value;
                }
                else
                {
                    if (Regex.Match(UserAgent, DeviceRegex).Success)
                    {
                        return Regex.Match(UserAgent, DeviceRegex).Value;
                    }
                    else
                    {
                        return "iPad";
                    }

                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
