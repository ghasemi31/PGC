using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using System.Collections.Generic;

namespace pgc.Business
{
    public class SMSSendAttemptBusiness : BaseEntityManagementBusiness<SMSSendAttempt, pgcEntities>
    {
        public SMSSendAttemptBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, SMSSendAttemptPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.SMSSendAttempts, Pattern)
                .OrderByDescending(f => f.ID);


            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(SMSSendAttemptPattern Pattern)
        {
            return Search_Where(Context.SMSSendAttempts, Pattern).Count();
        }

        public IQueryable<SMSSendAttempt> Search_Where(IQueryable<SMSSendAttempt> list, SMSSendAttemptPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (Pattern.ID > 0)
                return list.Where(f => f.ID == Pattern.ID);

            if (BasePattern.IsEnumAssigned(Pattern.MessageType))
                list = list.Where(f => f.MessageType == (int)Pattern.MessageType);

            if (!string.IsNullOrEmpty(Pattern.Message))
                list = list.Where(f => f.Body.Contains(Pattern.Message));

            if (!string.IsNullOrEmpty(Pattern.RecipientNumber))
                list = list.Where(f => f.Recipients.Contains(Pattern.RecipientNumber));

            switch (Pattern.PersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.PersianDate.HasFromDate && Pattern.PersianDate.HasToDate)
                        list = list.Where(f => f.PersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0 && f.PersianDate.CompareTo(Pattern.PersianDate.ToDate) <= 0);
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
            
            if (BasePattern.IsEnumAssigned(Pattern.EventType))
                list = list.Where(f => f.EventType == (int)Pattern.EventType);

            if (Pattern.OccuredEventID > 0)
                list = list.Where(f => f.OccuredEvent_ID == Pattern.OccuredEventID);

            if (!string.IsNullOrEmpty(Pattern.RecipientNumber))
                list = list.Where(f => f.Recipients.Contains(Pattern.RecipientNumber));

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
                    case SMSSendAttemptStatus.AllSent:
                        list = list.Where(f => f.Total_SucceedCount == f.Total_SumCount);
                        break;
                    case SMSSendAttemptStatus.NoSent:
                        list = list.Where(f => f.Total_SucceedCount == 0);
                        break;
                    case SMSSendAttemptStatus.SomeSent:
                        list = list.Where(f => f.Total_SucceedCount > 0 && f.Total_SucceedCount < f.Total_SumCount);
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

                    List<SMSSendAttempt> tempList = new List<SMSSendAttempt>();

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
            return list;
        }

        public IQueryable<SMSSendAttempt> RetrieveByOccuredEvent(EventType eventType, long occured_ID)
        {
            return Context.SMSSendAttempts.Where(f => f.EventType == (int)eventType && f.OccuredEvent_ID == occured_ID);
        }
    }
}