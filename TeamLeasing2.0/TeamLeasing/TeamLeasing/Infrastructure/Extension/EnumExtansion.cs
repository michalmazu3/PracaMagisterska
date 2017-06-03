using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

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

        public static DisplayAttribute GetAttribute<T>(this T enumValue)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>();
        }

        public static IEnumerable<TEnum> Values<TEnum>()
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var enumType = typeof(TEnum);
            return Enum.GetValues(enumType).Cast<TEnum>();
        }

        public static TEnum ValueByDisplayName<TEnum>(string displayName)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var enumType = typeof(TEnum);
            return Enum.GetValues(enumType).Cast<TEnum>().FirstOrDefault(f => f.GetAttribute().Name == displayName);
        }
    }
}