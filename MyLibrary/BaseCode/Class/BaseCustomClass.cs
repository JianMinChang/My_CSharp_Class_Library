using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.BaseCode.InterFace;

namespace MyLibrary.BaseCode.Class
{
    public class BaseCustomClass : IBaseRepository
    {
        public bool IsSuccess { protected set; get; }

        public string ErrorMessage { protected set; get; }

        protected virtual void Reset()
        {
            this.IsSuccess = false;
            this.ErrorMessage = string.Empty;
        }
    }
}
