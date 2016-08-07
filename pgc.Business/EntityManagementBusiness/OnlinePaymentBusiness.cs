using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class OnlinePaymentBusiness : BaseEntityManagementBusiness<OnlinePayment, pgcEntities>
    {
        public OnlinePaymentBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, OnlinePaymentPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.OnlinePayments, Pattern)
                .OrderByDescending(f => f.ID);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }


        public IQueryable<OnlinePayment> Search_Select(OnlinePaymentPattern Pattern)
        {
            return Search_Where(Context.OnlinePayments, Pattern).OrderByDescending(f => f.ID);
        }

        public int Search_Count(OnlinePaymentPattern Pattern)
        {
            return Search_Where(Context.OnlinePayments, Pattern).Count();
        }

        public IQueryable<OnlinePayment> Search_Where(IQueryable<OnlinePayment> list, OnlinePaymentPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;
            
            ////Search By Pattern
            if (BasePattern.IsEnumAssigned(Pattern.Status))
            {
                string status=Pattern.Status.ToString();
                list = list.Where(f => f.TransactionState == status);
            }

            if (Pattern.Order_ID > 0)
                list = list.Where(f => f.Order_ID == Pattern.Order_ID);

            if (Pattern.Branch_ID > 0)
                list = list.Where(f => f.Order.Branch_ID == Pattern.Branch_ID);

            if (Pattern.User_ID > 0)
                list = list.Where(f => f.Order.User_ID == Pattern.User_ID);

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

        public OnlinePayment RetrieveByRefNum(string RefNum)
        {
            return Context.OnlinePayments.SingleOrDefault(f => f.RefNum == RefNum);
        }
    }
}