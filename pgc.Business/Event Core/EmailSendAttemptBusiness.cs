using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using pgc.Business.Lookup;
using System.Collections.Generic;

namespace pgc.Business
{
    public class EmailSendAttemptBusiness : BaseEntityManagementBusiness<EmailSendAttempt, pgcEntities>
    {
        public EmailSendAttemptBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, EmailSendAttemptPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.EmailSendAttempts, Pattern)
                            .OrderByDescending(f => f.Date);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(EmailSendAttemptPattern Pattern)
        {
            return Search_Where(Context.EmailSendAttempts, Pattern).Count();
        }

        public IQueryable<EmailSendAttempt> Search_Where(IQueryable<EmailSendAttempt> list, EmailSendAttemptPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            if (Pattern.ID > 0)
                return list.Where(lst => lst.ID == Pattern.ID);

            if (BasePattern.IsEnumAssigned(Pattern.EventType))
                list = list.Where(f => f.EventType == (int)Pattern.EventType);

            if (Pattern.OccuredEventID > 0)
                list = list.Where(f => f.OccuredEvent_ID == Pattern.OccuredEventID);

            if (!string.IsNullOrEmpty(Pattern.Recipient))
                list = list.Where(f => f.Recipients.Contains(Pattern.Recipient));

            #region Only For Event Title
            if (!string.IsNullOrEmpty(Pattern.EventTitle))
            {
                if (BasePattern.IsEnumAssigned(Pattern.EventType))
                {
                    //if (Pattern.EventType == EventType.Schedule)
                    //{
                    //    List<long> occuredIDs = new List<long>();

                    //    foreach (var tempList in Context.ScheduleEvents.Where(f => f.Title.Contains(Pattern.EventTitle) || f.Description.Contains(Pattern.EventTitle)).Select(g => g.OccuredScheduleEvents))
                    //    {
                    //        foreach (var item in tempList)
                    //        {
                    //            occuredIDs.Add(item.ID);
                    //        }
                    //    }
                    //    list = list.Where(f => f.EventType == (int)Pattern.EventType && occuredIDs.Contains(f.OccuredEvent_ID.Value));
                    //}
                    //else if (Pattern.EventType == EventType.System)
                    if (Pattern.EventType == EventType.System)
                    {
                        List<long> occuredIDs = new List<long>();

                        foreach (var tempList in Context.SystemEvents.Where(f => f.Title.Contains(Pattern.EventTitle) || f.Description.Contains(Pattern.EventTitle)).Select(g => g.OccuredSystemEvents))
                        {
                            foreach (var item in tempList)
                            {
                                occuredIDs.Add(item.ID);
                            }
                        }
                        list = list.Where(f => f.EventType == (int)Pattern.EventType && occuredIDs.Contains(f.OccuredEvent_ID.Value));
                    }
                }
                else
                {
                    List<long> occuredScheduleIDs = new List<long>();
                    //foreach (var tempList in Context.ScheduleEvents.Where(f => f.Title.Contains(Pattern.EventTitle) || f.Description.Contains(Pattern.EventTitle)).Select(g => g.OccuredScheduleEvents))
                    //{
                    //    foreach (var item in tempList)
                    //    {
                    //        occuredScheduleIDs.Add(item.ID);
                    //    }
                    //}

                    List<long> occuredSystemIDs = new List<long>();
                    foreach (var tempList in Context.SystemEvents.Where(f => f.Title.Contains(Pattern.EventTitle) || f.Description.Contains(Pattern.EventTitle)).Select(g => g.OccuredSystemEvents))
                    {
                        foreach (var item in tempList)
                        {
                            occuredSystemIDs.Add(item.ID);
                        }
                    }

                    list = list.Where(f => (f.EventType == (int)EventType.Schedule && occuredScheduleIDs.Contains(f.OccuredEvent_ID.Value)) ||
                                            (f.EventType == (int)EventType.System && occuredSystemIDs.Contains(f.OccuredEvent_ID.Value)));
                }
            }
            #endregion

            if (BasePattern.IsEnumAssigned(Pattern.Status))
            {
                switch (Pattern.Status)
                {
                    case EmailSendAttemptStatus.AllSent:
                        list = list.Where(f => f.TotalEmail_Count == f.SentEmail_Count);
                        break;
                    case EmailSendAttemptStatus.NoSent:
                        list = list.Where(f => f.SentEmail_Count == 0);
                        break;
                    case EmailSendAttemptStatus.SomeSent:
                        list = list.Where(f => f.SentEmail_Count > 0 && f.SentEmail_Count < f.TotalEmail_Count);
                        break;
                    default:
                        break;
                }

            }

            if (!string.IsNullOrEmpty(Pattern.Time) && Pattern.Time != "00:00")
            {
                try
                {
                    TimeSpan time0 = new TimeSpan(int.Parse(Pattern.Time.Substring(0, 2)), int.Parse(Pattern.Time.Substring(3, 2)), 0);
                    TimeSpan time1 = time0.Add(new TimeSpan(0, 1, 0));
                    TimeSpan time2 = time1.Add(new TimeSpan(0, 1, 0));

                    var temp = list.ToList();

                    List<EmailSendAttempt> tempList = new List<EmailSendAttempt>();

                    foreach (var item in temp)
                    {
                        if (((item.Date.TimeOfDay.Hours == time0.Hours) && (item.Date.TimeOfDay.Minutes == time0.Minutes)) ||
                            ((item.Date.TimeOfDay.Hours == time1.Hours) && (item.Date.TimeOfDay.Minutes == time1.Minutes)) ||
                            ((item.Date.TimeOfDay.Hours == time2.Hours) && (item.Date.TimeOfDay.Minutes == time2.Minutes)))
                            tempList.Add(item);
                    }

                    list = tempList.AsQueryable();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            //Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f =>
                    f.Body.Contains(Pattern.Title) ||
                    f.Subject.Contains(Pattern.Title));
                        
            switch (Pattern.PersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.PersianDate.HasFromDate && Pattern.PersianDate.HasToDate)
                        list = list.Where(f => f.PersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0
                            && f.PersianDate.CompareTo(Pattern.PersianDate.ToDate) <= 0);
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => f.PersianDate.CompareTo(Pattern.PersianDate.Date) >= 0);
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => f.PersianDate.CompareTo(Pattern.PersianDate.Date) <= 0);
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => f.PersianDate.CompareTo(Pattern.PersianDate.Date) == 0);
                    break;
            }

            //Only SentEmail
            switch (Pattern.SentEmail_Count.Type)
            {
                case RangeType.Between:
                    if (Pattern.SentEmail_Count.HasFirstNumber && Pattern.SentEmail_Count.HasSecondNumber)
                        list = list.Where(f => f.SentEmail_Count >= Pattern.SentEmail_Count.FirstNumber
                            && f.SentEmail_Count <= Pattern.SentEmail_Count.SecondNumber);
                    break;
                case RangeType.GreatherThan:
                    if (Pattern.SentEmail_Count.HasFirstNumber)
                        list = list.Where(f => f.SentEmail_Count >= Pattern.SentEmail_Count.FirstNumber);
                    break;
                case RangeType.LessThan:
                    if (Pattern.SentEmail_Count.HasSecondNumber)
                        list = list.Where(f => f.SentEmail_Count <= Pattern.SentEmail_Count.SecondNumber);
                    break;
                case RangeType.EqualTo:
                    if (Pattern.SentEmail_Count.HasFirstNumber)
                        list = list.Where(f => f.SentEmail_Count == Pattern.SentEmail_Count.FirstNumber);
                    break;
                case RangeType.Nothing:
                default:
                    break;
            }

            return list;
        }

        public IQueryable<EmailSendAttempt> RetrieveByOccuredEvent(EventType eventType, long OccuredEventID)
        {
            return Context.EmailSendAttempts.Where(f => f.EventType == (int)eventType && f.OccuredEvent_ID == OccuredEventID);
        }
    }
}