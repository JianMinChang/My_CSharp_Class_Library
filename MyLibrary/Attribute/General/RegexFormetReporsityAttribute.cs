using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Attribute
{
    /// <summary>
    /// 自定列舉使用屬性，提供存取正規表示式寫法
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RegexFormetReporsityAttribute : System.Attribute
    {
        public string RegexStr { get; set; }

        public RegexFormetReporsityAttribute(string RegexStr)
        {
            this.RegexStr = RegexStr;
        }

        public override string ToString()
        {
            return this.RegexStr.ToString();
        }
    }
}
