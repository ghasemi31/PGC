using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model
{
    [Serializable]
    public class BasePattern
    {
        /// <summary>
        /// Checks whether Enum instanse are assigned or not
        /// </summary>
        /// <param name="Enum">Enum to evaluate</param>
        /// <returns></returns>
        public static bool IsEnumAssigned(object Enum)
        {
            return IsEnumAssigned(Enum, false);
        }

        /// <summary>
        /// Checks whether Enum instanse are assigned or not
        /// </summary>
        /// <param name="Enum">Enum to evaluate</param>
        /// <param name="EnumHasZeroValue">Whether Enum has item with 0 value or not , Default is False . e.g. for SendStatus this param should be set to True</param>
        /// <returns></returns>
        public static bool IsEnumAssigned(object Enum,bool EnumHasZeroValue)
        {
            if (Enum == null)
                return false;

            if (Enum.ToString() == "-1")
                return false;

            if (!EnumHasZeroValue && Enum.ToString() == "0")
                return false;

            return true;
        }
    }
}
