using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class SentSMSBusiness : BaseEntityManagementBusiness<SentSMS,pgcEntities>
    {
        public SentSMSBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, SentSMSPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.SentSMSList, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.PersianDate,
                    f.Date,
                    f.RecipientNumber,
                    f.SendStatus,
                    f.Body,
                    f.SMSCount
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(SentSMSPattern Pattern)
        {
            return Search_Where(Context.SentSMSList, Pattern).Count();
        }

        public IQueryable<SentSMS> Search_Where(IQueryable<SentSMS> list, SentSMSPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;


            ////Search By Pattern
            if (Pattern.SMSSendAttempt_ID > 0)
                list = list.Where(f => f.SMSSendAttempt_ID == Pattern.SMSSendAttempt_ID);

            //Chone meghdare 0 dar in enum Unknown hast va sharte  BasePattern.IsEnumAssigned(Pattern.SendStatus) hatman true mishavad
            //if (BasePattern.IsEnumAssigned(Pattern.SendStatus))
            //    list = list.Where(Msg => Msg.SendStatus.Equals((int)Pattern.SendStatus));

            if (BasePattern.IsEnumAssigned(Pattern.MessageType))
                list = list.Where(Msg => Msg.MessageType.Equals((int)Pattern.MessageType));

            if (!string.IsNullOrEmpty(Pattern.Message))
                list = list.Where(Msg => Msg.Body.Contains(Pattern.Message));

            if (!string.IsNullOrEmpty(Pattern.RecipientNumber))
                list = list.Where(Msg => Msg.RecipientNumber.Contains(Pattern.RecipientNumber));

            switch (Pattern.SendDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.SendDate.HasFromDate && Pattern.SendDate.HasToDate)
                        list = list.Where(Msg => Msg.PersianDate.CompareTo(Pattern.SendDate.FromDate) >= 0 && Msg.PersianDate.CompareTo(Pattern.SendDate.ToDate) <= 0);
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.SendDate.HasDate)
                        list = list.Where(Msg => Msg.PersianDate.CompareTo(Pattern.SendDate.Date) >= 0);
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.SendDate.HasDate)
                        list = list.Where(Msg => Msg.PersianDate.CompareTo(Pattern.SendDate.Date) <= 0);
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.SendDate.HasDate)
                        list = list.Where(Msg => Msg.PersianDate.CompareTo(Pattern.SendDate.Date) == 0);
                    break;
            }
            return list;
        }
    }
}