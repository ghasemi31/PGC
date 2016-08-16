using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;

namespace pgc.Business
{
    public class PaymentBusiness : BaseEntityManagementBusiness<pgc.Model.Payment, pgcEntities>
    {
        public PaymentBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, PaymentPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.Payments, Pattern)
                .OrderByDescending(f => f.ID);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }


        public IQueryable<pgc.Model.Payment> Search_Select(PaymentPattern Pattern)
        {
            return Search_Where(Context.Payments, Pattern).OrderByDescending(f => f.ID);
        }

        public int Search_Count(PaymentPattern Pattern)
        {
            return Search_Where(Context.Payments, Pattern).Count();
        }

        public IQueryable<pgc.Model.Payment> Search_Where(IQueryable<pgc.Model.Payment> list, PaymentPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

           
                if (BasePattern.IsEnumAssigned(Pattern.Status)) { 
                    switch (Pattern.Status)
                    {
                        case GameOrderPaymentStatus.OnlineSucced:


                            // asan pardakht here
                            list = list.Where(f => (f.BankGeway_Enum==(int)OnlineGetway.MellatBankGateWay&& f.State == (int)MellatOnlinePaymentState.OK) );
                            break;
                        case GameOrderPaymentStatus.OnlineFailed:
                            list = list.Where(f => (f.BankGeway_Enum == (int)OnlineGetway.MellatBankGateWay && f.State != (int)MellatOnlinePaymentState.OK));
                            // asan pardakht here
                            break;
                        default:
                            break;
                    }
                }
            

            if (Pattern.Order_ID > 0)
                list = list.Where(f => f.Order_ID == Pattern.Order_ID);

            if (Pattern.Game_ID > 0)
                list = list.Where(f => f.GameOrder.Game_ID == Pattern.Game_ID);

            if (Pattern.User_ID > 0)
                list = list.Where(f => f.GameOrder.User_ID == Pattern.User_ID);

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


       
        public pgc.Model.Payment RetrieveByRefNum(string RefNum)
        {
            return Context.Payments.SingleOrDefault(f => f.RefNum == RefNum);
        }
    }
}