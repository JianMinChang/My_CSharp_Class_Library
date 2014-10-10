using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MyLibrary.Valid
{
    public static class ValidText
    {
        /// <summary>
        /// 驗證是否是數字
        /// </summary>
        /// <param name="Input">輸入字串</param>
        /// <returns>True:成功;False:失敗</returns>
        public static bool IsNumber(string Input)
        {
            return Regex.IsMatch(Input, @"^-?\d+$");
        }

        /// <summary>
        /// 驗證是否是英文
        /// </summary>
        /// <param name="Input">輸入字串</param>
        /// <returns>True:成功;False:失敗</returns>
        public static bool IsEnglish(string Input)
        {
            return Regex.IsMatch(Input, @"^[A-Za-z]+$");
        }
        /// <summary>
        /// 驗證是否是英文和數字
        /// </summary>
        /// <param name="Input">輸入字串</param>
        /// <returns>True:成功;False:失敗</returns>
        public static bool IsEngAndNumber(string Input)
        {
            return Regex.IsMatch(Input, @"^[A-Za-z0-9]+$");
        }
        /// <summary>
        /// 驗證是否是Email
        /// </summary>
        /// <param name="Input">輸入字串</param>
        /// <returns>True:成功;False:失敗</returns>
        public static bool IsEmail(string Input)
        {
            return Regex.IsMatch(Input, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 驗證是否是IPv4
        /// </summary>
        /// <param name="Input">輸入字串</param>
        /// <returns>True:成功;False:失敗</returns>
        public static bool IsIPv4(string Input)
        {
            return Regex.IsMatch(Input, @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");
        }

        /// <summary>
        /// 驗證是否是中文
        /// </summary>
        /// <param name="Input">輸入字串</param>
        /// <returns>True:成功;False:失敗</returns>
        public static bool IsChinese(string Input)
        {
            return Regex.IsMatch(Input, @"^[\u4E00-\u9fa5]+$");
        }
        /// <summary>
        /// 驗證是否是中文、英文和數字
        /// </summary>
        /// <param name="Input">輸入字串</param>
        /// <returns>True:成功;False:失敗</returns>
        public static bool IsChEgNu(string Input)
        {
            return Regex.IsMatch(Input, @"^[\u4E00-\u9fa5A-Za-z0-9]+$");
        }


        /// <summary>
        /// 驗證是否符合，由patten傳規則
        /// </summary>
        /// <param name="Input">輸入字串</param>
        /// <param name="Patten">True:成功;False:失敗</param>
        /// <returns></returns>
        public static bool IsMatchByPatten(string Input, string Patten)
        {
            return Regex.IsMatch(Input, Patten);
        }
    }
}
