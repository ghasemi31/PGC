using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using System.Text.RegularExpressions;
using pgc.Model.Enums;

namespace pgc.Business
{
    public class SystemEventBusiness : BaseEntityManagementBusiness<SystemEvent, pgcEntities>
    {
        public SystemEventBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, SystemEventPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.SystemEvents, Pattern)
                .OrderByDescending(f => f.ID);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(SystemEventPattern Pattern)
        {
            return Search_Where(Context.SystemEvents, Pattern).Count();
        }

        public IQueryable<SystemEvent> Search_Where(IQueryable<SystemEvent> list, SystemEventPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            if (Pattern.ID > 0)
                return list.Where(n => n.ID == Pattern.ID);


            ////Manual
            //if ((int)(Pattern.Support_Manual_Email) > -1)
            //    list = list.Where(f => f.Support_Manual_Email == ((int)Pattern.Support_Manual_Email == 1) ? true : false);

            //if ((int)(Pattern.Support_Manual_SMS) > -1)
            //    list = list.Where(f => f.Support_Manual_SMS == ((int)Pattern.Support_Manual_SMS == 1) ? true : false);

            ////Every
            //if ((int)(Pattern.Support_Every_Admin_Email) > -1)
            //    list = list.Where(f => f.Support_Every_Admin_Email == ((int)Pattern.Support_Every_Admin_Email == 1) ? true : false);

            //if ((int)(Pattern.Support_Every_Admin_SMS) > -1)
            //    list = list.Where(f => f.Support_Every_Admin_SMS == ((int)Pattern.Support_Every_Admin_SMS == 1) ? true : false);

            //if ((int)(Pattern.Support_Every_User_Email) > -1)
            //    list = list.Where(f => f.Support_Every_User_Email == ((int)Pattern.Support_Every_User_Email == 1) ? true : false);

            //if ((int)(Pattern.Support_Every_User_SMS) > -1)
            //    list = list.Where(f => f.Support_Every_User_SMS == ((int)Pattern.Support_Every_User_SMS == 1) ? true : false);

            ////Related
            //if ((int)(Pattern.Support_Related_Guest_Email) > -1)
            //    list = list.Where(f => f.Support_Related_Guest_Email == ((int)Pattern.Support_Related_Guest_Email == 1) ? true : false);

            //if ((int)(Pattern.Support_Related_User_Email) > -1)
            //    list = list.Where(f => f.Support_Related_User_Email == ((int)Pattern.Support_Related_User_Email == 1) ? true : false);

            //if ((int)(Pattern.Support_Related_ImagingCenter_Email) > -1)
            //    list = list.Where(f => f.Support_Related_ImagingCenter_Email == ((int)Pattern.Support_Related_ImagingCenter_Email == 1) ? true : false);

            //if ((int)(Pattern.Support_Related_Department_Email) > -1)
            //    list = list.Where(f => f.Support_Related_Department_Email == ((int)Pattern.Support_Related_Department_Email == 1) ? true : false);

            //if ((int)(Pattern.Support_Related_Guest_SMS) > -1)
            //    list = list.Where(f => f.Support_Related_Guest_SMS == ((int)Pattern.Support_Related_Guest_SMS == 1) ? true : false);

            //if ((int)(Pattern.Support_Related_User_SMS) > -1)
            //    list = list.Where(f => f.Support_Related_User_SMS == ((int)Pattern.Support_Related_User_SMS == 1) ? true : false);

            //if ((int)(Pattern.Support_Related_ImagingCenter_SMS) > -1)
            //    list = list.Where(f => f.Support_Related_ImagingCenter_SMS == ((int)Pattern.Support_Related_ImagingCenter_SMS == 1) ? true : false);

            //if ((int)(Pattern.Support_Related_Department_SMS) > -1)
            //    list = list.Where(f => f.Support_Related_Department_SMS == ((int)Pattern.Support_Related_Department_SMS == 1) ? true : false);

            //Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f =>
                    f.Title.Contains(Pattern.Title) ||
                    f.Description.Contains(Pattern.Title) ||
                    f.Template_Admin_Email.Contains(Pattern.Title) ||
                    f.Template_Admin_SMS.Contains(Pattern.Title) ||
                    f.Template_User_Email.Contains(Pattern.Title) ||
                    f.Template_User_SMS.Contains(Pattern.Title));

            if (Pattern.PrivateNo_ID > 0)
                list = list.Where(f => f.SystemEventAction.PrivateNo_ID == Pattern.PrivateNo_ID);
            
            return list;
        }

        public override OperationResult Update(SystemEvent Data)
        {
            string emailResult = "";

            if (Data.SystemEventAction != null)
            {
                foreach (string dest in Data.SystemEventAction.ManualEmail.Split('\n'))
                {

                    string text = dest.Trim();

                    //Bypass Empty Lines
                    if (string.IsNullOrEmpty(text))
                        continue;

                    string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
                    Match match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);

                    if (!match.Success)
                    {
                        continue;
                    }

                    emailResult += text + "\n";
                }
                Data.SystemEventAction.ManualEmail = emailResult;

                string smsResult = "";
                foreach (string dest in Data.SystemEventAction.ManualSMS.Split('\n'))
                {
                    string text = dest.Trim();

                    if (string.IsNullOrEmpty(text))
                        continue;

                    double f = 0;
                    if (double.TryParse(text, out f))
                        smsResult += text + "\n";
                }
                Data.SystemEventAction.ManualSMS = smsResult;

                Data.Template_Admin_Email = Util.ConvertStringToHtml(Data.Template_Admin_Email);
                Data.Template_User_Email = Util.ConvertStringToHtml(Data.Template_User_Email);

            }
            return base.Update(Data);
        }

        public SystemEvent RetrieveByKey(SystemEventKey key)
        {
            string eventKey=key.ToString();
            return Context.SystemEvents.Single(f => f.EventKey == eventKey);
        }
    }
}