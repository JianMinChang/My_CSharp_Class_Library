using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using MySql.Data.Common;
using MySql.Data.Types;
using MyLibrary.BaseCode.ADO.InterFace;

namespace MyLibrary.BaseCode.ADO.Repository
{
    public class CustomMySql : IDBProvider
    {

        public bool IsSuccess { set; get; }

        public string ErrorMessage { set; get; }

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

        private string _DBConnectionString { set; get; }
        public string DBConnectionString
        {
            set { this._DBConnectionString = value; }
            private get { return this._DBConnectionString; }
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
            using (MySqlConnection con = new MySqlConnection(this.DBConnectionString))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(SQLcommand, con))
                {
                    da.SelectCommand.CommandTimeout = this._ConnectionTimeout;
                    for (int i = 0; i < Parms.Count; i++)
                    {
                        da.SelectCommand.Parameters.Add(new MySqlParameter(Parms[i].ParameterName, Parms[i].Value));
                    }
                    da.SelectCommand.CommandType = this._CommandType;

                    try
                    {
                        con.Open();
                        da.Fill(dt);
                        IsSuccess = true;
                    }
                    catch (MySqlException e)
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
            using (MySqlConnection con = new MySqlConnection(this.DBConnectionString))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(SQLcommand, con))
                {
                    da.SelectCommand.CommandTimeout = this._ConnectionTimeout;

                    da.SelectCommand.CommandType = this._CommandType;

                    try
                    {
                        con.Open();
                        da.Fill(dt);
                        IsSuccess = true;
                    }
                    catch (MySqlException e)
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
            using (MySqlConnection con = new MySqlConnection(this._DBConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLcommand, con))
                {
                    cmd.CommandTimeout = this._ConnectionTimeout;

                    for (int i = 0; i < Parms.Count; i++)
                    {
                        cmd.Parameters.Add(new MySqlParameter(Parms[i].ParameterName, Parms[i].Value));
                    }
                    cmd.CommandType = this._CommandType;

                    try
                    {
                        con.Open();
                        obj = cmd.ExecuteScalar();
                        IsSuccess = true;
                    }
                    catch (MySqlException e)
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
            using (MySqlConnection con = new MySqlConnection(this._DBConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLcommand, con))
                {
                    cmd.CommandTimeout = this._ConnectionTimeout;

                    cmd.CommandType = this._CommandType;

                    try
                    {
                        con.Open();
                        obj = cmd.ExecuteScalar();
                        IsSuccess = true;
                    }
                    catch (MySqlException e)
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

            using (MySqlConnection con = new MySqlConnection(this._DBConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLcommand, con))
                {
                    cmd.CommandTimeout = this._ConnectionTimeout;
                    for (int i = 0; i < Parms.Count; i++)
                    {
                        cmd.Parameters.Add(new MySqlParameter(Parms[i].ParameterName, Parms[i].Value));
                    }
                    cmd.CommandType = this._CommandType;

                    try
                    {
                        con.Open();
                        obj = cmd.ExecuteNonQuery();
                        IsSuccess = true;
                    }
                    catch (MySqlException e)
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
            
            using (MySqlConnection con = new MySqlConnection(this._DBConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLcommand, con))
                {
                    cmd.CommandTimeout = this._ConnectionTimeout;

                    cmd.CommandType = this._CommandType;

                    try
                    {
                        con.Open();
                        obj = cmd.ExecuteNonQuery();
                        IsSuccess = true;
                    }
                    catch (MySqlException e)
                    {
                        this.ErrorMessage = e.Message;
                    }

                }
            }
            AfterUseReset();
            return obj;
        }



       
    }
}
