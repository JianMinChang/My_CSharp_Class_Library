using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;

namespace MyLibrary.Xml
{
    public class CustomXML
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess {private set; get; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrorMessage { private set; get; }

        /// <summary>
        /// 屬性回復預設值
        /// </summary>
        private void Reset()
        {
            this.IsSuccess = false;
            this.ErrorMessage = string.Empty;
        }

        /// <summary>
        /// 建立XML檔
        /// </summary>
        /// <param name="ds">要放到Xml的DataTable集合of DataSet</param>
        /// <returns>建好的XmlDocument</returns>
        public XmlDocument CreateXml(DataSet ds)
        {
            Reset();

            XmlDocument Xdoc = new XmlDocument();

            XmlDeclaration Xdec = Xdoc.CreateXmlDeclaration("1.0", "UTF-16", null);
            Xdoc.InsertBefore(Xdec, Xdoc.DocumentElement);

            XmlElement Xroot = Xdoc.CreateElement("Log");
            Xdoc.AppendChild(Xroot);

            try
            {
                ////有[i]個Table
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    XmlElement Xfirst = Xdoc.CreateElement("Table");
                    //Xfirst.SetAttribute("Name", myinfo[i].tablename.ToString());

                    Xfirst.SetAttribute("Name", ds.Tables[i].TableName.ToString());
                    //Xfirst.SetAttribute("Where", myinfo[i].condition.ToString());
                    Xroot.AppendChild(Xfirst);
                    ////第[i]個TABLE裡有多少[j]筆資料(ROW)
                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        XmlElement xrow = Xdoc.CreateElement("Row");

                        ////第[i]個TABLE裡有多少[K]個欄位
                        for (int k = 0; k < ds.Tables[i].Columns.Count; k++)
                        {
                            xrow.SetAttribute(ds.Tables[i].Columns[k].ToString(), ds.Tables[i].Rows[j][k].ToString());
                        }

                        Xfirst.AppendChild(xrow);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }

            if (this.ErrorMessage ==string.Empty)
            {
                this.IsSuccess = true;
            }
            return Xdoc;
        }

        /// <summary>
        /// 將XMLSting 轉成 DataTable
        /// </summary>
        /// <param name="Xmlstring">XML String</param>
        /// <returns>DataTable</returns>
        public DataTable XmlStringToDataTable(string Xmlstring)
        {
            Reset();

            DataTable dt =null;
            try
            {
                //新建XML文件類別
                XmlDocument Xmldoc = new XmlDocument();
                //從指定的字串載入XML文件
                Xmldoc.LoadXml(Xmlstring);
                //建立此物件，並輸入透過StringReader讀取Xmldoc中的Xmldoc字串輸出
                XmlReader Xmlreader = XmlReader.Create(new System.IO.StringReader(Xmldoc.OuterXml));
                //建立DataSet
                DataSet ds = new DataSet();
                //透過DataSet的ReadXml方法來讀取Xmlreader資料
                ds.ReadXml(Xmlreader);
                //建立DataTable並將DataSet中的第0個Table資料給DataTable
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }

            if (this.ErrorMessage == string.Empty)
            {
                this.IsSuccess = true;
            }
            //回傳DataTable
            return dt;
        }

        /// <summary>
        /// 將XmlDocument 轉成 DataTable
        /// </summary>
        /// <param name="Xmlstring">XML String</param>
        /// <returns>DataTable</returns>
        public DataTable XmlStringToDataTable(XmlDocument Xmldoc)
        {
            Reset();

            DataTable dt = null;
            try
            {
                //建立此物件，並輸入透過StringReader讀取Xmldoc中的Xmldoc字串輸出
                XmlReader Xmlreader = XmlReader.Create(new System.IO.StringReader(Xmldoc.OuterXml));
                //建立DataSet
                DataSet ds = new DataSet();
                //透過DataSet的ReadXml方法來讀取Xmlreader資料
                ds.ReadXml(Xmlreader);
                //建立DataTable並將DataSet中的第0個Table資料給DataTable
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }

            if (this.ErrorMessage == string.Empty)
            {
                this.IsSuccess = true;
            }
            //回傳DataTable
            return dt;
        }

    }
}
