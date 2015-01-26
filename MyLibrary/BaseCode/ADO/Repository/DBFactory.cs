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
                    break;
            }

            return temp;
        }

    }


    public enum CustomDBType
    {
        MsSQL = 0, MySql = 1
    }
}
