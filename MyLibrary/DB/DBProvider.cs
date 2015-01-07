using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;


namespace MyLibrary.DB
{
    
    public class DBProvider
    {
        public DBProvider()
        {

        }

        public DBProvider(string ConnectionString)
        {
            this.ConnectString = ConnectionString;
        }


        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { set; get; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrorMessage { set; get; }

        /// <summary>
        /// 資料庫連線資訊
        /// </summary>
        public string ConnectString { set; get; }

        /// <summary>
        /// 指定如何解譯命令字串。
        /// </summary>
        private CommandType _CommandType = CommandType.Text;

        /// <summary>
        /// 指定如何解譯命令字串。
        /// </summary>
        public CommandType CommandType
        {
            set { this._CommandType = value; }
            get { return this._CommandType; }
        }


        private DataTable dt = new DataTable();

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

        /// <summary>
        /// TimeOut時間
        /// </summary>
        private int _ConnectionTimeout = 30;

        /// <summary>
        /// 設定/取得 TimeOut時間
        /// </summary>
        public int ConnectionTimeout
        {
            set { this._ConnectionTimeout = value; }
            get { return this._ConnectionTimeout; }
        }



        /// <summary>
        /// 查詢並回傳值
        /// </summary>
        /// <param name="SQLcommand">SQL語法或預存程序名稱</param>
        /// <param name="Parms">參數集合</param>
        /// <returns>查詢到的值</returns>
        public object ValueListSQL(string SQLcommand, IList<IDataParameter> Parms)
        {
            BeforeUseReset();
            object obj = null;
            using (SqlConnection con = new SqlConnection(this.ConnectString))
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

        /// <summary>
        /// 查詢並回傳值
        /// </summary>
        /// <param name="SQLcommand">SQL語法或預存程序名稱</param>
        /// <returns>查詢到的值</returns>
        public object ValueListSQL(string SQLcommand)
        {
            BeforeUseReset();
            object obj = null;
            using (SqlConnection con = new SqlConnection(this.ConnectString))
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

        /// <summary>
        /// 查詢並回傳資料表
        /// </summary>
        /// <param name="SQLcommand">資料庫語法或預存程序名稱</param>
        /// <param name="Parms">參數集合</param>
        /// <returns>資料表</returns>
        public DataTable ListSQL(string SQLcommand, IList<IDataParameter> Parms)
        {
            BeforeUseReset();
            using (SqlConnection con = new SqlConnection(this.ConnectString))
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

        /// <summary>
        /// 查詢並回傳資料表
        /// </summary>
        /// <param name="SQLcommand">資料庫語法或預存程序名稱</param>
        /// <returns>資料表</returns>
        public DataTable ListSQL(string SQLcommand)
        {
            BeforeUseReset();
            using (SqlConnection con = new SqlConnection(this.ConnectString))
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

        /// <summary>
        /// 執行SQL動作
        /// </summary>
        /// <param name="SQLcommand">資料庫語法或預存程序名稱</param>
        /// <param name="Parms">參數集合</param>
        /// <returns>受影響的資料數目</returns>
        public object ExecSQL(string SQLcommand, IList<IDataParameter> Parms)
        {
            BeforeUseReset();
            object obj = null;
            
            using (SqlConnection con = new SqlConnection(this.ConnectString))
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

        /// <summary>
        /// 執行SQL動作
        /// </summary>
        /// <param name="SQLcommand">資料庫語法或預存程序名稱</param>
        /// <returns>受影響的資料數目</returns>
        public object ExecSQL(string SQLcommand)
        {
            BeforeUseReset();
            object obj = null;
            
            using (SqlConnection con = new SqlConnection(this.ConnectString))
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
    }

    
}
