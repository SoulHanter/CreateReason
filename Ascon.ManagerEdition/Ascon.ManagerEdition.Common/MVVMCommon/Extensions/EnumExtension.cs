using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Common.MVVMCommon.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum source)
        {
            var type = source.GetType();
            var info = type.GetMember(source.ToString());

            if(info != null && info.Count() > 0)
            {
                var attributes = info[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if(attributes != null && attributes.Count() > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            return source.ToString();
        }
    }
}
