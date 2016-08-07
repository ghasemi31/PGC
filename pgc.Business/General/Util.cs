using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pgc.Model;
using pgc.Business;
using kFrameWork.Util;

namespace pgc.Business
{
    public class Util
    {
        public static string GetFullNameWithGender(long id)
        {

            User Data = new pgcEntities().Users.SingleOrDefault(f => f.ID == id);
            return GetFullNameWithGender(Data);
        }

        public static string GetFullNameWithGender(User Data)
        {
            return string.Format("{0} {1} {2}",
                        (Data.Gender == 1) ? " جناب آقای" : "سرکار خانم ",
                        Data.Fname,
                        Data.Lname
                    );
        }

        public static string GetPersianDateWithTime(DateTime Date)
        {
            return DateUtil.GetPersianDateShortString(Date) + " - " + Date.Hour.ToString().PadLeft(2, '0') + ":" + Date.Minute.ToString().PadLeft(2, '0');
        }

        internal static string ConvertStringToHtml(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("\r\n", "<br />"); // Return Key.
                text = text.Replace(System.Convert.ToChar(9).ToString(), "&nbsp;&nbsp;&nbsp; "); // TAB Key.
            }
            return text;
        }



    }
}
