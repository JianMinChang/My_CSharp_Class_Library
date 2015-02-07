using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Enum
{
    public static class CustomEnum
    {
        public static string GetEnumStrAttribute<T, U>(T EnumType)
            where T : struct
            where U : class
        {
            var members = typeof(T).GetMember(EnumType.ToString());
            var attributes = members[0].GetCustomAttributes(typeof(U), false);
            var description = ((U)attributes[0]).ToString();
            return description;
        }
    }
}
