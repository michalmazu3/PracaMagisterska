using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TeamLeasing.Infrastructure.Extension
{
    public static class EnumExtansion
    {
        public static DisplayAttribute GetAttribute(this Enum enumValue)
             
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>();
        }
    }
}
