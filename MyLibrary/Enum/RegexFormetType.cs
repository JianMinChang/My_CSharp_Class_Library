using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Attribute;

namespace MyLibrary.Enum.CustomEnumRepository
{
    public enum RegexFormetType
    {
        [RegexFormetReporsityAttribute("")]
        None = 0,

        [RegexFormetReporsityAttribute(@"\d+")]
        Number = 1,

        [RegexFormetReporsityAttribute(@"[A-Za-z]+")]
        English = 2,

        [RegexFormetReporsityAttribute(@"[A-Za-z0-9]+")]
        EngAndNumber = 3,

        [RegexFormetReporsityAttribute(@"([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)")]
        Email = 4,

        [RegexFormetReporsityAttribute(@"([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}")]
        IPv4 = 5,

        [RegexFormetReporsityAttribute(@"[\u4E00-\u9fa5]+")]
        Chinese = 6,

        [RegexFormetReporsityAttribute(@"[\u4E00-\u9fa5A-Za-z]+")]
        ChEng = 7,

        [RegexFormetReporsityAttribute(@"[\u4E00-\u9fa5A-Za-z0-9]+")]
        ChEgNu = 8
    }
}
