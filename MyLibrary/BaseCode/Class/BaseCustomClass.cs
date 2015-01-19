using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.BaseCode.Class
{
    public class BaseCustomClass
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
