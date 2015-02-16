using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.BaseCode.ADO.InterFace;

namespace MyLibrary.BaseCode.ADO.Repository
{
    public static class DBFactory
    {
        /// <summary>
        /// 由CustomDBType取得IDBProider
        /// </summary>
        /// <param name="DBType">選擇使用的DB類型</param>
        /// <returns></returns>
        public static IDBProvider GetDBFactory(CustomDBType DBType)
        {

            IDBProvider temp = null;
            switch (DBType)
            {
                case CustomDBType.MsSQL:
                    temp = new CustomMsSQL();
                    break;
                case CustomDBType.MySql:
                    temp = new CustomMySql();
                    break;
                default:
                    temp = new CustomMsSQL();
                    break;
            }

            return temp;
        }

    }

    /// <summary>
    /// 可使用的DB類型
    /// </summary>
    public enum CustomDBType
    {
        MsSQL = 0, MySql = 1
    }
}
