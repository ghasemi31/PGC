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
    public class SentEmailBlockBusiness : BaseEntityManagementBusiness<SentEmailBlock, pgcEntities>
    {
        public SentEmailBlockBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, SentEmailBlockPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.SentEmailBlocks, Pattern)
                .OrderByDescending(f => f.Date)
                .Select(g => new
                {
                    g.Date,
                    g.EmailSendAttempt,
                    g.EmailSentAttempt_ID,
                    g.ID,
                    g.IsSent,
                    g.PersianDate,
                    g.RecipientMailAddress,
                    g.Size,
                    Body = g.EmailSendAttempt.Body,
                    EventType = g.EmailSendAttempt.EventType,
                    OccuredEvent_ID = g.EmailSendAttempt.OccuredEvent_ID,
                    Subject = g.EmailSendAttempt.Subject
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(SentEmailBlockPattern Pattern)
        {
            return Search_Where(Context.SentEmailBlocks, Pattern).Count();
        }

        public IQueryable<SentEmailBlock> Search_Where(IQueryable<SentEmailBlock> list, SentEmailBlockPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            if (Pattern.ID > 0)
                return list.Where(n => n.ID == Pattern.ID);

            if (BasePattern.IsEnumAssigned(Pattern.EventType))
                list = list.Where(f => f.EmailSendAttempt.EventType == (int)Pattern.EventType);

            if (!string.IsNullOrEmpty(Pattern.EventTitle))
            {
                if (BasePattern.IsEnumAssigned(Pattern.EventType))
                {
                    //if (Pattern.EventType == EventType.Schedule)
                    //{
                    //    IQueryable<long> scheduleIDs = Context.OccuredScheduleEvents.Where(f => f.ScheduleEvent.Title.Contains(Pattern.EventTitle) || f.ScheduleEvent.Description.Contains(Pattern.EventTitle)).Select(g => g.ID);
                    //    list = list.Where(f => scheduleIDs.Contains(f.EmailSendAttempt.OccuredEvent_ID.Value));
                    //}
                    //else
                    if (Pattern.EventType == EventType.System)
                    {
                        IQueryable<long> systemIDs = Context.OccuredSystemEvents.Where(f => f.SystemEvent.Title.Contains(Pattern.EventTitle) || f.SystemEvent.Description.Contains(Pattern.EventTitle)).Select(g => g.ID);
                        list = list.Where(f => systemIDs.Contains(f.EmailSendAttempt.OccuredEvent_ID.Value));
                    }
                }
                else
                {
                    //IQueryable<long> scheduleIDs = Context.OccuredScheduleEvents.Where(f => f.ScheduleEvent.Title.Contains(Pattern.EventTitle) || f.ScheduleEvent.Description.Contains(Pattern.EventTitle)).Select(g => g.ID);
                    //IQueryable<long> systemIDs = Context.OccuredSystemEvents.Where(f => f.SystemEvent.Title.Contains(Pattern.EventTitle) || f.SystemEvent.Description.Contains(Pattern.EventTitle)).Select(g => g.ID);

                    //IQueryable<SentEmailBlock> ScheduleTempList = list.Where(f => f.EmailSendAttempt.EventType == (int)EventType.Schedule && scheduleIDs.Contains(f.EmailSendAttempt.OccuredEvent_ID.Value));
                    //IQueryable<SentEmailBlock> SystemTempList = list.Where(f => f.EmailSendAttempt.EventType == (int)EventType.System && systemIDs.Contains(f.EmailSendAttempt.OccuredEvent_ID.Value));

                    //list = ScheduleTempList.Union(SystemTempList);

                    IQueryable<long> systemIDs = Context.OccuredSystemEvents.Where(f => f.SystemEvent.Title.Contains(Pattern.EventTitle) || f.SystemEvent.Description.Contains(Pattern.EventTitle)).Select(g => g.ID);
                    list = list.Where(f => f.EmailSendAttempt.EventType == (int)EventType.System && systemIDs.Contains(f.EmailSendAttempt.OccuredEvent_ID.Value));
                }
            }
            
            if (Pattern.EmailSentAttempt_ID > 0)
                list = list.Where(f => f.EmailSentAttempt_ID== Pattern.EmailSentAttempt_ID);

            if (BasePattern.IsEnumAssigned(Pattern.IsSent))
                list = list.Where(f => f.IsSent == ((int)Pattern.IsSent == 2) ? true : false);

            if (!string.IsNullOrEmpty(Pattern.RecipientMailAddress))
                list = list.Where(f => f.RecipientMailAddress.Contains(Pattern.RecipientMailAddress));

            //Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f =>
                                            f.EmailSendAttempt.Body.Contains(Pattern.Title) ||
                                            f.EmailSendAttempt.Subject.Contains(Pattern.Title));

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


            switch (Pattern.Size.Type)
            {
                case RangeType.Between:
                    if (Pattern.Size.HasFirstNumber && Pattern.Size.HasSecondNumber)
                        list = list.Where(f => f.Size >= Pattern.Size.FirstNumber
                            && f.Size <= Pattern.Size.SecondNumber);
                    break;
                case RangeType.GreatherThan:
                    if (Pattern.Size.HasFirstNumber)
                        list = list.Where(f => f.Size >= Pattern.Size.FirstNumber);
                    break;
                case RangeType.LessThan:
                    if (Pattern.Size.HasSecondNumber)
                        list = list.Where(f => f.Size <= Pattern.Size.SecondNumber);
                    break;
                case RangeType.EqualTo:
                    if (Pattern.Size.HasFirstNumber)
                        list = list.Where(f => f.Size == Pattern.Size.FirstNumber);
                    break;
                case RangeType.Nothing:
                default:
                    break;
            }

            return list;
        }
    }
}