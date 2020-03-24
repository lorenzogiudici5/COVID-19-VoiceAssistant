using System;
using System.ComponentModel;
using System.Reflection;

namespace CoronavirusFunction.Helpers
{
    public static class EnumHelper
    {
        public static string ToDescription(this Enum value)    //extension method
        {
            Type type = value.GetType();

            MemberInfo[] memInfo = type.GetMember(value.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return value.ToString();
        }

        public static bool IsValidValue<T>(string value) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return Enum.IsDefined(typeof(T), value);
        }

        public static T Parse<T>(string value) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an Enum Type");
            }
            T parsedValue;
            Enum.TryParse<T>(value, true, out parsedValue);
            return parsedValue;
        }
    }
}
