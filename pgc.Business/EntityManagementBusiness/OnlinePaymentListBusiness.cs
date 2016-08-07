using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class OnlinePaymentListBusiness : BaseEntityManagementBusiness<OnlinePaymentList, pgcEntities>
    {
        public OnlinePaymentListBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, OnlinePaymentListPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.OnlinePaymentLists, Pattern)
                .OrderByDescending(f => f.Date);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }


        public IQueryable<OnlinePaymentList> Search_Select(OnlinePaymentListPattern Pattern)
        {
            return Search_Where(Context.OnlinePaymentLists, Pattern).OrderByDescending(f => f.Date);
        }

        public int Search_Count(OnlinePaymentListPattern Pattern)
        {
            return Search_Where(Context.OnlinePaymentLists, Pattern).Count();
        }

        public IQueryable<OnlinePaymentList> Search_Where(IQueryable<OnlinePaymentList> list, OnlinePaymentListPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;
            
            ////Search By Pattern
            int st = (int)Pattern.Status;
            if (Pattern.Status==pgc.Model.Enums.OnlinePaymentStatus.Branches)
            {
                list = list.Where(f => f.ResNum.Contains("b"));
            }
            if (Pattern.Status == pgc.Model.Enums.OnlinePaymentStatus.User)
            {
                list = list.Where(f => !f.ResNum.Contains("b"));
            }

            switch (Pattern.PersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.PersianDate.HasFromDate && Pattern.PersianDate.HasToDate)
                        list = list.Where(f =>f.PersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0
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

            switch (Pattern.Amount.Type)
            {
                case RangeType.Between:
                    if (Pattern.Amount.HasFirstNumber && Pattern.Amount.HasSecondNumber)
                        list = list.Where(f => f.Amount >= Pattern.Amount.FirstNumber
                            && f.Amount <= Pattern.Amount.SecondNumber);
                    break;
                case RangeType.GreatherThan:
                    if (Pattern.Amount.HasFirstNumber)
                        list = list.Where(f => f.Amount >= Pattern.Amount.FirstNumber);
                    break;
                case RangeType.LessThan:
                    if (Pattern.Amount.HasSecondNumber)
                        list = list.Where(f => f.Amount <= Pattern.Amount.SecondNumber);
                    break;
                case RangeType.EqualTo:
                    if (Pattern.Amount.HasFirstNumber)
                        list = list.Where(f => f.Amount == Pattern.Amount.FirstNumber);
                    break;
                case RangeType.Nothing:
                default:
                    break;
            }


            return list;
        }

        public long TotalAmount(OnlinePaymentListPattern Pattern)
        {
            var Result = Search_Where(Context.OnlinePaymentLists, Pattern);
            if (Result.Count() > 0)
                return Result.Sum(f => f.Amount);
            else
                return 0;
        }

        public OnlinePaymentList RetrieveByRefNum(string RefNum)
        {
            return Context.OnlinePaymentLists.SingleOrDefault(f => f.RefNum == RefNum);
        }


        public string RetriveBranchName(string onlineResNum)
        {
            return Context.BranchPayments.FirstOrDefault(b => b.OnlineResNum == onlineResNum).Branch.Title;
        }
    }
}
   