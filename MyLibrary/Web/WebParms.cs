﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;

namespace MyLibrary.Web
{
    public class HttpParmsToObj
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

        /// <summary>
        /// 將傳入的QueryString集合轉換成T型別(泛型)物件
        /// </summary>
        /// <typeparam name="T">T泛型類別</typeparam>
        /// <param name="temp">QueryString集合</param>
        /// <returns>T型別物件</returns>
        public T DeserializeFromQueryString<T>(System.Collections.Specialized.NameValueCollection temp)
        {
            var json = JsonConvert.SerializeObject(
                           temp.AllKeys.ToDictionary(x => x, x => temp[x]));
            return JsonConvert.DeserializeObject<T>(json);
        }


        /// <summary>
        /// 將傳入的Form集合轉換成T型別(泛型)物件
        /// </summary>
        /// <typeparam name="T">T泛型類別</typeparam>
        /// <param name="temp">Form集合</param>
        /// <returns>T型別物件</returns>
        public T DeserializeFromPostForm<T>(System.Collections.Specialized.NameValueCollection temp)
        {
            var t = temp.AllKeys.ToDictionary(x => x, x => temp[x]);

            var t2 = from c in t
                     where !t.Keys.Contains("__VIEWSTATE")
                     select c;

            var t1 = JsonConvert.SerializeObject(t2);

            return JsonConvert.DeserializeObject<T>(t1);
        }
    }
}
