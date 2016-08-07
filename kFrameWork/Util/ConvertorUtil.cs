using System;
using System.Collections.Generic;

namespace kFrameWork.Util
{
    public class ConvertorUtil
    {
        public static string ToSeperatedString(IEnumerable<string> list, string Seperator, bool TrimEnd)
        {
            if (list == null)
                return "";

            string result = "";
            foreach (string item in list)
            {
                result += item + Seperator;
            }
            if (TrimEnd && result.EndsWith(Seperator.ToString()))
                result = result.Substring(0, result.Length - Seperator.Length);
            return result;
        }

        public static Object Convert(Object number, Type type, bool ByPassNull)
        {
            if (ByPassNull && number == null)
                return null;

            if (type.Equals(typeof(Int16)) )
                return ConvertorUtil.ToInt16(number);

            if (type.Equals(typeof(Int32)) )
                return ConvertorUtil.ToInt32(number);

            if (type.Equals(typeof(Int64)) )
                return ConvertorUtil.ToInt64(number);

            if (type.Equals(typeof(Decimal)) )
                return ConvertorUtil.ToDecimal(number);

            if (type.Equals(typeof(Boolean)))
                return ConvertorUtil.ToBoolean(number);

            return number;
        }

        public static Object Convert(Object number, string TypeName,bool ByPassNull)
        {
            if (ByPassNull && number == null)
                return null;

            if (TypeName.ToLower() == typeof(short).Name.ToLower() )
                return ConvertorUtil.ToInt16(number);

            if (TypeName.ToLower() == typeof(int).Name.ToLower() )
                return ConvertorUtil.ToInt32(number);

            if (TypeName.ToLower() == typeof(long).Name.ToLower() )
                return ConvertorUtil.ToInt64(number);

            if (TypeName.ToLower() == typeof(decimal).Name.ToLower())
                return ConvertorUtil.ToDecimal(number);

            if (TypeName.ToLower() == typeof(bool).Name.ToLower())
                return ConvertorUtil.ToBoolean(number);

            return number;
        }

        public static T Convert<T>(Object number)
        {
            if (typeof(T).Equals(typeof(Int16)) )
                return (T)System.Convert.ChangeType(ConvertorUtil.ToInt16(number), typeof(T));

            if (typeof(T).Equals(typeof(Int32)) )
                return (T)System.Convert.ChangeType(ConvertorUtil.ToInt32(number), typeof(T));

            if (typeof(T).Equals(typeof(Int64)) )
                return (T)System.Convert.ChangeType(ConvertorUtil.ToInt64(number), typeof(T));

            if (typeof(T).Equals(typeof(Decimal)) )
                return (T)System.Convert.ChangeType(ConvertorUtil.ToDecimal(number), typeof(T));

            if (typeof(T).Equals(typeof(Boolean)))
                return (T)System.Convert.ChangeType(ConvertorUtil.ToBoolean(number), typeof(T));

            return (T)System.Convert.ChangeType(number, typeof(T));
        }

        public static Int16 ToInt16(object value)
        {
            try
            {
                return System.Convert.ToInt16(value);
            }
            catch
            {
                return 0;
            }
        }

        public static Int32 ToInt32(object value)
        {
            try
            {
                return System.Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        public static Int64 ToInt64(object value)
        {
            try
            {
                return System.Convert.ToInt64(value);
            }
            catch
            {
                return 0;
            }
        }

        public static decimal ToDecimal(object value)
        {
            try
            {
                return System.Convert.ToDecimal(value);
            }
            catch
            {
                return 0;
            }
        }

        public static double ToDouble(object value)
        {
            try
            {
                return System.Convert.ToDouble(value);
            }
            catch
            {
                return 0;
            }
        }

        public static bool ToBoolean(object value)
        {
            try
            {
                return System.Convert.ToBoolean(ToInt16(value));
            }
            catch
            {
                return false;
            }
        }

        public static byte ToByte(object value)
        {
            try
            {
                return System.Convert.ToByte(value);
            }
            catch
            {
                return 0;
            }
        }
    }
}