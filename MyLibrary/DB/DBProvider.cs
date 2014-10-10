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
        public CommandType CommandType { set; get; }

        /// <summary>
        /// 查詢並回傳值
        /// </summary>
        /// <param name="SQLcommand">SQL語法或預存程序名稱</param>
        /// <param name="Parms">參數集合</param>
        /// <returns>查詢到的值</returns>
        public object ValueListSQL(string SQLcommand, IList<IDataParameter> Parms)
        {
            IsSuccess = false;
            ErrorMessage = null;
            object obj = null;
            using (SqlConnection con = new SqlConnection(this.ConnectString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLcommand, con))
                {


                    for (int i = 0; i < Parms.Count; i++)
                    {
                        cmd.Parameters.Add(new SqlParameter(Parms[i].ParameterName, Parms[i].Value));
                    }
                    cmd.CommandType = this.CommandType;

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
            IsSuccess = false;
            ErrorMessage = null;
            object obj = null;
            using (SqlConnection con = new SqlConnection(this.ConnectString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLcommand, con))
                {

                    cmd.CommandType = this.CommandType;

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
            IsSuccess = false;
            ErrorMessage = null;
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(this.ConnectString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(SQLcommand, con))
                {

                    for (int i = 0; i < Parms.Count; i++)
                    {
                        da.SelectCommand.Parameters.Add(new SqlParameter(Parms[i].ParameterName, Parms[i].Value));
                    }
                    da.SelectCommand.CommandType = this.CommandType;

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
            IsSuccess = false;
            ErrorMessage = null;
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(this.ConnectString))
            {
                
                using (SqlDataAdapter da = new SqlDataAdapter(SQLcommand, con))
                {

                    da.SelectCommand.CommandType = this.CommandType;

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
            object obj = null;
            IsSuccess = false;
            ErrorMessage = null;

            using (SqlConnection con = new SqlConnection(this.ConnectString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLcommand, con))
                {
                    for (int i = 0; i < Parms.Count; i++)
                    {
                        cmd.Parameters.Add(new SqlParameter(Parms[i].ParameterName, Parms[i].Value));
                    }
                    cmd.CommandType = this.CommandType;

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
            object obj = null;
            IsSuccess = false;
            ErrorMessage = null;

            using (SqlConnection con = new SqlConnection(this.ConnectString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLcommand, con))
                {
                    
                    cmd.CommandType = this.CommandType;

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
            return obj;
        }
    }

    
}
