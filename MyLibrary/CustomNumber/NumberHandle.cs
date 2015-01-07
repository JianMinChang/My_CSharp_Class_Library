using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.CustomNumber
{
    public class NumberHandle
    {
        /// <summary>
        /// 數字轉字串並缺項補0
        /// </summary>
        /// <param name="num">要補0的數字</param>
        /// <param name="Digits">總共有幾位數</param>
        /// <returns>已補上不足項的數字字串</returns>
        public static string intFormat(int num, int Digits)
        {
            string snum = num.ToString();
            return snum.PadLeft(Digits, '0');
        }

    }
}
