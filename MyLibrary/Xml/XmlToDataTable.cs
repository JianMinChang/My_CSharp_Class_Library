using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;

namespace MyLibrary.Xml
{
    public class XmlToDataTable
    {
        /// <summary>
        /// 建立XML檔
        /// </summary>
        /// <param name="ds">要放到Xml的DataTable集合of DataSet</param>
        /// <returns>建好的XmlDocument</returns>
        public XmlDocument CreateXml(DataSet ds)
        {
            XmlDocument Xdoc = new XmlDocument();

            XmlDeclaration Xdec = Xdoc.CreateXmlDeclaration("1.0", "UTF-16", null);
            Xdoc.InsertBefore(Xdec, Xdoc.DocumentElement);

            XmlElement Xroot = Xdoc.CreateElement("Log");
            Xdoc.AppendChild(Xroot);

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
            return Xdoc;
        }

    }
}
