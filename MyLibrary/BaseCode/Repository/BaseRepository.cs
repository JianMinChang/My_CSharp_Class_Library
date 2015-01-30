using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MyLibrary.BaseCode.InterFace;
using MyLibrary.BaseCode.ADO.InterFace;

namespace MyLibrary.BaseCode.Repository
{
    public class BaseRepository : IBaseRepository
    {
        public bool IsSuccess { protected set; get; }

        public string ErrorMessage { protected set; get; }

        protected IList<IDataParameter> Parms = new List<IDataParameter>();
        protected DataTable dt = new DataTable();

        /// <summary>
        /// 使用前，屬性回復預設值
        /// </summary>
        protected void BeforeUseReset()
        {
            this.IsSuccess = false;
            this.ErrorMessage = string.Empty;
            this.dt = new DataTable();
            if (this.Parms == null)
            {
                this.Parms = new List<IDataParameter>();
            }
            else
            {
                this.Parms.Clear();
            }
        }

        /// <summary>
        /// 使用完，屬性回復預設值
        /// </summary>
        protected void AfterUseReset()
        {
            this.Parms.Clear();
        }



        protected IDBProvider DBProvider { set; get; }

    }
}
