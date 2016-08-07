using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarsiLibrary.Utils;

namespace kFrameWork.Util
{
    public class DateUtil
    {
        public static string GetPersianDateWithTime(DateTime OriginalValue)
        {
            string strHour = OriginalValue.Hour + ":" + OriginalValue.Minute;
            string strDay = "";
            string strDirection = "rtl";
            if (OriginalValue.Date.Subtract(DateTime.Now.Date).Days == 0 )
                strDay = "امروز";
            else if (OriginalValue.Date.Subtract(DateTime.Now.Date).Days == -1 )
                strDay = "دیروز";
            else
            {
                strDay = GetPersianDateShortString(OriginalValue) ;
                strDirection = "rtl";
            }

            if (strHour == "0:0")
                strHour = "";


            string strResult = "";
            strResult = string.Format("<span title=\"{1}\">{0}</span>",
                string.Format("<span dir=\"{2}\">{0} &nbsp;&nbsp; {1}</span>", strDay, strHour, strDirection),
                DateUtil.GetPersianDate(OriginalValue) + "    " + strHour);
            return strResult;
        }

        public static string GetPersianDateWithTimeRaw(DateTime OriginalValue)
        {
            string strHour = OriginalValue.Hour + ":" + OriginalValue.Minute;
            string strDay = "";
 
            if (OriginalValue.Date.Subtract(DateTime.Now.Date).Days == 0)
                strDay = "امروز";
            else if (OriginalValue.Date.Subtract(DateTime.Now.Date).Days == -1)
                strDay = "دیروز";
            else
            {
                strDay = GetPersianDateShortString(OriginalValue);
          
            }

            if (strHour == "0:0")
                strHour = "";


            string strResult =DateUtil.GetPersianDate(OriginalValue) + "    " + strHour;
            return strResult;
        }

        public static string GetPersianDateWithTimeWithoutDayText(DateTime OriginalValue)
        {
            string strHour = OriginalValue.Hour + ":" + OriginalValue.Minute;
            string strDay = "";
            if (OriginalValue.Date.Subtract(DateTime.Now.Date).Days == 0)
                strDay = "";
            else if (OriginalValue.Date.Subtract(DateTime.Now.Date).Days == -1)
                strDay = "";
            else
            {
                strDay = GetPersianDateShortString(OriginalValue);
            }

            if (strHour == "0:0")
                strHour = "";


            string strResult = "";
            if (strDay != "")
                strDay += "<br/>";
            if (strHour != "")
                strHour = "ساعت " + strHour;
            strResult = string.Format("<span title=\"{1}\">{0}</span>",
                string.Format("{0}{1}</span>", strDay, strHour),
                DateUtil.GetPersianDate(OriginalValue) + "    " + strHour);
            return strResult;
        }

        public static string GetPersianDateShortString(DateTime Date)
        {
            return PersianDateConverter.ToPersianDate(Date).ToString().Substring(0, 10);
        }

        public static string GetPersianDateFull(DateTime Date)
        {
            return string.Format("{0} ساعت {1}",
                GetPersianDate(Date),
                Date.Hour + ":" + Date.Minute);
        }

        public static string GetPersianDate(DateTime Date)
        {
            return PersianDateConverter.ToPersianDate(Date).ToWritten();
        }

        public static DateTime GetEnglishDateTime(string Date)
        {
            return PersianDateConverter.ToGregorianDateTime(Date);
        }

        //public static DateTime GetEnglishDateTime(PersianDate Date)
        //{
        //    return PersianDateConverter.ToGregorianDateTime(Date);
        //}

        public static string GetPlainDateString(DateTime Date)
        {
            return Date.Year.ToString() + Date.Month.ToString().PadLeft(2, '0') + Date.Day.ToString().PadLeft(2, '0');
            
        }

        public static string GetPlainTimeString(DateTime Date)
        {
            return Date.Hour.ToString().PadLeft(2, '0') + Date.Minute.ToString().PadLeft(2, '0') + Date.Second.ToString().PadLeft(2, '0');
        }

        public static string GetPersinMonthName(string Month)
        {
            switch (Month)
            {
                case "1":
                case "01":return "فروردین";
                case "2":
                case "02": return "اردیبهشت";
                case "3":
                case "03": return "خرداد";
                case "4":
                case "04": return "تیر";
                case "5":
                case "05": return "مرداد";
                case "6":
                case "06": return "شهریور";
                case "7":
                case "07": return "مهر";
                case "8":
                case "08": return "آبان";
                case "9":
                case "09": return "آذر";
                case "10": return "دی";
                case "11": return "بهمن";
                case "12": return "اسفند";
            }
            return "";

        }

        public static string GetPersianDateWithMonthName(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0} {1} {2}", pc.GetDayOfMonth(date), GetPersinMonthName(pc.GetMonth(date).ToString()), pc.GetYear(date));
        }
    }
}
