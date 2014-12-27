﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;

namespace MyLibrary.Web
{
    public class HttpQueryStringObj
    {
        /// <summary>
        /// 將傳入的URL字串含有的QueryString轉換成T型別(泛型)物件
        /// </summary>
        /// <typeparam name="T">T泛型類別</typeparam>
        /// <param name="queryString">含QueryString的URL</param>
        /// <returns>T型別物件</returns>
        public static T DeserializeFromQueryString<T>(string queryString)
        {
            var collection = HttpUtility.ParseQueryString(queryString);
            var json = JsonConvert.SerializeObject(
                collection.AllKeys.ToDictionary(x => x, x => collection[x]));
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
