﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;

namespace MyLibrary.MyReflection
{
    public static class Reflection
    {

        /// <summary>
        /// 將傳入的datatable 對應到傳入的型別上，執行完回傳T，裡面是已經對應好並轉型完成的型別
        /// </summary>
        /// <typeparam name="T">Model class</typeparam>
        /// <param name="dt">要對應的表格</param>
        /// <returns>回傳對應好的 T </returns>
        public static T DataTableRefToOne<T>(DataTable dt) where T : new()
        {
            DataTableReader dtr = new DataTableReader(dt);

            T item = new T();

            while (dtr.Read())
            {
                for (int i = 0; i < dtr.FieldCount; i++)
                {
                    PropertyInfo property = item.GetType().GetProperty(dtr.GetName(i));
                    SetPropertyValue(item, dtr[dtr.GetName(i)].ToString(), property);
                }
            }

            return item;
        }



        /// <summary>
        /// 將傳入的datatable 對應到傳入的型別上，並存在List上，執行完回傳IList，裡面是已經對應好並轉型完成的型別
        /// </summary>
        /// <typeparam name="T">Model class</typeparam>
        /// <param name="dt">要對應的表格</param>
        /// <returns>回傳對應好的IList</returns>
        public static IList<T> DataTableRefToList<T>(DataTable dt) where T : new()
        {
            DataTableReader dtr = new DataTableReader(dt);

            IList<T> Data = new List<T>();

            while (dtr.Read())
            {
                T item = new T();
                for (int i = 0; i < dtr.FieldCount; i++)
                {
                    PropertyInfo property = item.GetType().GetProperty(dtr.GetName(i));
                    SetPropertyValue(item, dtr[dtr.GetName(i)].ToString(), property);
                }
                Data.Add(item);
            }
            return Data;
        }


        private static void SetPropertyValue(object instance, string val, PropertyInfo info)
        {
            //本方法由"http://kevintsengtw.blogspot.tw/2013/05/aspnet-mvc-model-adonet.html#.VDczPvmSztv" 參考而來
            try
            {
                if (info.PropertyType.Equals(typeof(string)))
                {
                    info.SetValue(instance, val, new object[0]);
                }
                else if (info.PropertyType.Equals(typeof(Boolean)))
                {
                    bool value = false;
                    value = val.ToLower().StartsWith("true");
                    info.SetValue(instance, value, new object[0]);
                }
                else if (info.PropertyType.Equals(typeof(int)))
                {
                    int value = String.IsNullOrEmpty(val) ? 0 : int.Parse(val);
                    info.SetValue(instance, value, new object[0]);
                }
                else if (info.PropertyType.Equals(typeof(double)))
                {
                    double value = 0.0d;
                    if (!String.IsNullOrEmpty(val))
                    {
                        value = Convert.ToDouble(val);
                    }
                    info.SetValue(instance, value, new object[0]);
                }
                else if (info.PropertyType.Equals(typeof(DateTime)))
                {
                    DateTime value = String.IsNullOrEmpty(val)
                        ? DateTime.MinValue
                        : DateTime.Parse(val);
                    info.SetValue(instance, value, new object[0]);
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }



        /// <summary>
        /// 將物件轉換成DataTable
        /// </summary>
        /// <typeparam name="T">j物件型別</typeparam>
        /// <param name="collection">物件集合容器</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }
    }
}