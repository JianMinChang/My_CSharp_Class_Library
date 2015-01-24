using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MyLibrary.BaseCode.InterFace
{

    public interface IBaseRepository
    {
        bool IsSuccess { get; }
        string ErrorMessage { get; }

    }
}
