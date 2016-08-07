using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pgc.Model;
using kFrameWork.Util;

namespace pgc.Business.Core
{
    public class ExceptionHandler
    {
        public static void HandleManualException(Exception Ex, string BusinessClassName)
        {
            try
            {
                //Log Exception
                long LogID = LogException(Ex, BusinessClassName);
            }
            catch (Exception exc) { }
        }

        private static long LogException(Exception Ex, string BusinessClassName)
        {
            //log Exception
            ExceptionLog Log = new ExceptionLog();

            Log.Date = DateTime.Now;
            Log.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);

            if (Ex == null)
            {
                Log.ExceptionMessage = "Ex Is Null";
                Log.ExceptionType = "Ex Is Null";
                Log.InnerException = "";
                Log.StackTrace = "";
            }
            else
            {
                Log.ExceptionMessage = string.IsNullOrEmpty(Ex.Message) ? "" : Ex.Message;
                Log.InnerException = (Ex.InnerException != null) ? Ex.InnerException.Message : "";
                Log.StackTrace = string.IsNullOrEmpty(Ex.StackTrace) ? "" : Ex.StackTrace;
                try
                {
                    Log.ExceptionType = Ex.GetType().ToString();
                }
                catch (Exception)
                {
                    Log.ExceptionType = "";
                }
            }

            if (!string.IsNullOrEmpty(BusinessClassName))
                Log.InnerException += ".  BusinessClass:" + BusinessClassName;

            pgcEntities Context = new pgcEntities();
            Context.ExceptionLogs.AddObject(Log);
            Context.SaveChanges();
            return Log.ID;
        }
    }
}
