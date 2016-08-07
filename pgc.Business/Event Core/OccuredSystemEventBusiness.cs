using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Model;
using pgc.Model.Enums;

namespace pgc.Business
{
    public class OccuredSystemEventBusiness : BaseEntityManagementBusiness<OccuredSystemEvent, pgcEntities>
    {
        public OccuredSystemEventBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, OccuredSystemEventPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.OccuredSystemEvents, Pattern)
                .OrderByDescending(f => f.Date);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(OccuredSystemEventPattern Pattern)
        {
            return Search_Where(Context.OccuredSystemEvents, Pattern).Count();
        }

        public IQueryable<OccuredSystemEvent> Search_Where(IQueryable<OccuredSystemEvent> list, OccuredSystemEventPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern

            if (Pattern.ID> 0)
                list = list.Where(f => f.ID==Pattern.ID);

            //if (BasePattern.IsEnumAssigned(Pattern.SystemEventKey))
            //{
            //    long SystemEventID = new SystemEventBusiness().RetrieveByKey(Pattern.SystemEventKey).ID;
            //    list = list.Where(f => f.SystemEvent_ID == SystemEventID);
            //}

            if (Pattern.SystemEvent_ID > 0)
                list = list.Where(f => f.SystemEvent_ID == Pattern.SystemEvent_ID);

            if (!string.IsNullOrEmpty(Pattern.Time) && Pattern.Time != "00:00")
            {
                try
                {
                    //list = list.Where(f => f.ScheduleEvent.SchedulePlans.Any(g => g.Time == Pattern.Time) || f.ScheduleEvent.ScheduleDates.Any(h => h.Time==Pattern.Time));
                    TimeSpan time0 = new TimeSpan(int.Parse(Pattern.Time.Substring(0, 2)), int.Parse(Pattern.Time.Substring(3, 2)), 0);
                    TimeSpan time1 = time0.Add(new TimeSpan(0, 1, 0));
                    TimeSpan time2 = time1.Add(new TimeSpan(0, 1, 0));

                    var temp = list.ToList();

                    List<OccuredSystemEvent> tempList = new List<OccuredSystemEvent>();

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

            if (BasePattern.IsEnumAssigned(Pattern.ActionType))
            {
                if (Pattern.ActionType == EventActionType.Email)
                {
                    List<long> occuredIDs = Context.EmailSendAttempts.Where(f => list.Select(g => g.ID).Contains(f.OccuredEvent_ID.Value) && f.EventType == (int)EventType.System).Select(g => g.OccuredEvent_ID.Value).ToList();
                    list = list.Where(f => occuredIDs.Contains(f.ID));
                }
                else if (Pattern.ActionType == EventActionType.SMS)
                {
                    List<long> occuredIDs = Context.SMSSendAttempts.Where(f => list.Select(g => g.ID).Contains(f.OccuredEvent_ID.Value) && f.EventType == (int)EventType.System).Select(g => g.OccuredEvent_ID.Value).ToList();
                    list = list.Where(f => occuredIDs.Contains(f.ID));
                }
            }

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
            return list;
        }
    }
}
