using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace kFrameWork.Util
{
    public class EnumUtil
    {
        public static string GetEnumPersianTitle(object Enum)
        {
            return Enum.GetType().GetCustomAttributes(true)[0].ToString();
        }

        public static string GetEnumElementPersianTitle(object EnumElement)
        {
            if (EnumElement.GetType().GetField(EnumElement.ToString()) == null)
                return "";
            return EnumElement.GetType().GetField(EnumElement.ToString()).GetCustomAttributes(true)[0].ToString();            
        }

        public static List<string> GentEnumElementListPersianTitle(object Enum)
        {
            return Enum.GetType().GetFields().Skip(1).Select(E => E.GetCustomAttributes(true)[0].ToString()).ToList();
        }

        public static string ConvertToEnumElementByPersianTitle(string PersianTitle,object Enum)
        {
            return Enum.GetType().GetFields().Skip(1).Where(E => E.GetCustomAttributes(true)[0].ToString().CompareTo(PersianTitle) == 0).Select(O => new {O.Name }).First().Name;
        }

        // NEW
        public static IQueryable GetEnumLookup(Type EnumType)
        {
            return EnumType.GetFields().Where(e => e.GetType().Name.StartsWith("Md")).Select(E => new { ID = ConvertorUtil.ToInt32(E.GetValue(null)), Title = E.GetCustomAttributes(true)[0].ToString() }).AsQueryable();
        }

        // SHOULD BE OMITEED
        public static IQueryable GentEnumElementComboListPersianTitle(object EnumInstance)
        {
            return EnumInstance.GetType().GetFields().Where(e => e.GetType().Name.StartsWith("Md")).Select(E => new { Value = E.Name, Text = E.GetCustomAttributes(true)[0].ToString() }).AsQueryable();
        }

        // SHOULD BE OMITEED
        public static IQueryable GentEnumElementComboListPersianTitle(object EnumInstance, bool IsSearchCombo)
        {
            if (IsSearchCombo)
            {
                var Value = EnumInstance.GetType().GetFields().Where(e => e.GetType().Name.StartsWith("Md")).Select(E => new { Value = E.Name, Text = E.GetCustomAttributes(true)[0].ToString() }).AsQueryable();

                Value.ToList().Insert(0, new { Value = "-1", Text = "فرقی نمی کند" });
                return Value;
            }
            else
                return EnumInstance.GetType().GetFields().Where(e => e.GetType().Name.StartsWith("Md")).Select(E => new { Value = E.Name, Text = E.GetCustomAttributes(true)[0].ToString() }).AsQueryable();
        }
    }
}
