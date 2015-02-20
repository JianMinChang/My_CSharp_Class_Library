using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection;
using MyLibrary.BaseCode.ADO.InterFace;

namespace MyLibrary.BaseCode.ADO.Repository
{
    /// <summary>
    /// MsSQL實做IDBProvider
    /// </summary>
    public class CustomMsSQL : IDBProvider
    {

        public bool IsSuccess { set; get; }
        public string ErrorMessage { set; get; }


        private string _DBConnectionString { set; get; }
        public string DBConnectionString
        {
            set { this._DBConnectionString = value; }
            private get { return this._DBConnectionString; }
        }


        private CommandType _CommandType = CommandType.Text;
        public CommandType CommandType
        {
            set { this._CommandType = value; }
            private get { return this._CommandType; }
        }

        private int _ConnectionTimeout = 30;
        public int ConnectionTimeout
        {
            set { this._ConnectionTimeout = value; }
            private get { return this._ConnectionTimeout; }
        }

        private DataTable dt;

        /// <summary>
        /// 屬性回復預設值
        /// </summary>
        private void BeforeUseReset()
        {
            this.IsSuccess = false;
            this.ErrorMessage = string.Empty;
            this.dt = new DataTable();
        }


        private void AfterUseReset()
        {
            this._CommandType = CommandType.Text;
            this._ConnectionTimeout = 30;
        }

        public DataTable ListSQL(string SQLcommand, IList<IDataParameter> Parms)
        {

            BeforeUseReset();
            using (SqlConnection con = new SqlConnection(this.DBConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(SQLcommand, con))
                {
                    da.SelectCommand.CommandTimeout = this._ConnectionTimeout;
                    for (int i = 0; i < Parms.Count; i++)
                    {
                        da.SelectCommand.Parameters.Add(new SqlParameter(Parms[i].ParameterName, Parms[i].Value));
                    }
                    da.SelectCommand.CommandType = this._CommandType;

                    try
                    {
                        con.Open();
                        da.Fill(dt);
                        IsSuccess = true;
                    }
                    catch (Exception e)
                    {
                        this.ErrorMessage = e.Message;
                    }

                }
            }
            AfterUseReset();
            Parms.Clear();
            return dt;
        }

        public DataTable ListSQL(string SQLcommand)
        {
            BeforeUseReset();
            using (SqlConnection con = new SqlConnection(this._DBConnectionString))
            {

                using (SqlDataAdapter da = new SqlDataAdapter(SQLcommand, con))
                {
                    da.SelectCommand.CommandTimeout = this._ConnectionTimeout;
                    da.SelectCommand.CommandType = this._CommandType;

                    try
                    {
                        con.Open();
                        da.Fill(dt);
                        IsSuccess = true;
                    }
                    catch (Exception e)
                    {
                        this.ErrorMessage = e.Message;
                    }

                }
            }
            AfterUseReset();
            return dt;
        }

        public object ValueListSQL(string SQLcommand, IList<IDataParameter> Parms)
        {
            BeforeUseReset();
            object obj = null;
            using (SqlConnection con = new SqlConnection(this._DBConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLcommand, con))
                {
                    cmd.CommandTimeout = this._ConnectionTimeout;

                    for (int i = 0; i < Parms.Count; i++)
                    {
                        cmd.Parameters.Add(new SqlParameter(Parms[i].ParameterName, Parms[i].Value));
                    }
                    cmd.CommandType = this._CommandType;

                    try
                    {
                        con.Open();
                        obj = cmd.ExecuteScalar();
                        IsSuccess = true;
                    }
                    catch (Exception e)
                    {
                        this.ErrorMessage = e.Message;
                    }

                }
            }
            AfterUseReset();
            Parms.Clear();
            return obj;

        }

        public object ValueListSQL(string SQLcommand)
        {
            BeforeUseReset();
            object obj = null;
            using (SqlConnection con = new SqlConnection(this._DBConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLcommand, con))
                {
                    cmd.CommandTimeout = this._ConnectionTimeout;
                    cmd.CommandType = this._CommandType;

                    try
                    {
                        con.Open();
                        obj = cmd.ExecuteScalar();
                        IsSuccess = true;
                    }
                    catch (Exception e)
                    {
                        this.ErrorMessage = e.Message;
                    }

                }
            }
            AfterUseReset();
            return obj;
        }

        public object ExecSQL(string SQLcommand, IList<IDataParameter> Parms)
        {
            BeforeUseReset();
            object obj = null;

            using (SqlConnection con = new SqlConnection(this._DBConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLcommand, con))
                {
                    cmd.CommandTimeout = this._ConnectionTimeout;
                    for (int i = 0; i < Parms.Count; i++)
                    {
                        cmd.Parameters.Add(new SqlParameter(Parms[i].ParameterName, Parms[i].Value));
                    }
                    cmd.CommandType = this._CommandType;

                    try
                    {
                        con.Open();
                        obj = cmd.ExecuteNonQuery();
                        IsSuccess = true;
                    }
                    catch (Exception e)
                    {
                        this.ErrorMessage = e.Message;
                    }

                }
            }
            AfterUseReset();
            Parms.Clear();
            return obj;
        }

        public object ExecSQL(string SQLcommand)
        {
            BeforeUseReset();
            object obj = null;

            using (SqlConnection con = new SqlConnection(this._DBConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLcommand, con))
                {
                    cmd.CommandTimeout = this._ConnectionTimeout;
                    cmd.CommandType = this._CommandType;

                    try
                    {
                        con.Open();
                        obj = cmd.ExecuteNonQuery();
                        IsSuccess = true;
                    }
                    catch (Exception e)
                    {
                        this.ErrorMessage = e.Message;
                    }

                }
            }
            AfterUseReset();
            return obj;
        }

        public void LotSqlBulkCopy(DataTable dt, string DestinationTable)
        {
            BeforeUseReset();

            try
            {
                using (SqlConnection conn = new SqlConnection(this._DBConnectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    //SqlBulkCopy批次處理新增 沒有檢驗比對處理
                    using (SqlBulkCopy bulkCopy =
                               new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans))
                    {


                        bulkCopy.DestinationTableName = DestinationTable;
                        bulkCopy.WriteToServer(dt);
                    }

                    trans.Commit();
                }
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
                this.IsSuccess = false;
            }

            if (this.ErrorMessage == string.Empty)
            {
                this.IsSuccess = true;
            }

        }

        public void LotSqlBulkCopy<T>(string DestinationTable, IList<T> temp)
        {
            BeforeUseReset();

            DataTable newdt = MyLibrary.MyReflection.Reflection.ToDataTable<T>(temp);
            try
            {
                using (SqlConnection conn = new SqlConnection(this._DBConnectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    //SqlBulkCopy批次處理新增 沒有檢驗比對處理
                    using (SqlBulkCopy bulkCopy =
                               new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans))
                    {
                        bulkCopy.DestinationTableName = DestinationTable;
                        bulkCopy.WriteToServer(newdt);
                    }

                    trans.Commit();
                }
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
                this.IsSuccess = false;
            }

            if (this.ErrorMessage == string.Empty)
            {
                this.IsSuccess = true;
            }

        }

    }
}
