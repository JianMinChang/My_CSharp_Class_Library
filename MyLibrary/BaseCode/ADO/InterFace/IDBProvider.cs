using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MyLibrary.BaseCode.ADO.InterFace
{
    /// <summary>
    /// 定義DBProvider需要實做的內容
    /// </summary>
    public interface IDBProvider
    {
        bool IsSuccess { get; }
        string ErrorMessage { get; }


        CommandType CommandType { set; }

        int ConnectionTimeout { set; }

        string DBConnectionString { set; }

        DataTable ListSQL(string SQLcommand, IList<IDataParameter> Parms);

        DataTable ListSQL(string SQLcommand);

        object ValueListSQL(string SQLcommand, IList<IDataParameter> Parms);

        object ValueListSQL(string SQLcommand);

        object ExecSQL(string SQLcommand, IList<IDataParameter> Parms);

        object ExecSQL(string SQLcommand);

    }
}
