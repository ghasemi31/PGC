using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using kFrameWork.UI;

namespace pgc.Business
{
    public class LotteryBusiness:BaseEntityManagementBusiness<Lottery,pgcEntities>
    {
        public LotteryBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, LotteryPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.Lotteries, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Status
                    
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(LotteryPattern Pattern)
        {
            return Search_Where(Context.Lotteries, Pattern).Count();
        }

        public IQueryable<Lottery> Search_Where(IQueryable<Lottery> list, LotteryPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title) ||
                    f.Description.Contains(Pattern.Title));


            if (BasePattern.IsEnumAssigned(Pattern.Status))
                list = list.Where(f => f.Status == (int)Pattern.Status);



            switch (Pattern.RegPersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.RegPersianDate.HasFromDate && Pattern.RegPersianDate.HasToDate)
                        list = list.Where(f => f.RegPersianDate.CompareTo(Pattern.RegPersianDate.FromDate) >= 0
                            && f.RegPersianDate.CompareTo(Pattern.RegPersianDate.ToDate) <= 0);
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.RegPersianDate.HasDate)
                        list = list.Where(f => f.RegPersianDate.CompareTo(Pattern.RegPersianDate.Date) >= 0);
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.RegPersianDate.HasDate)
                        list = list.Where(f => f.RegPersianDate.CompareTo(Pattern.RegPersianDate.Date) <= 0);
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.RegPersianDate.HasDate)
                        list = list.Where(f => f.RegPersianDate.CompareTo(Pattern.RegPersianDate.Date) == 0);
                    break;
            }
            return list;
        }


        public Lottery RetriveLottery(long ID)
        {
            return Context.Lotteries.SingleOrDefault(l => l.ID == ID);
        }

        public override OperationResult Delete(long ID)
        {
            Lottery Data = new LotteryBusiness().Retrieve(ID);

            //if ((Data.LotteryDetails != null && Data.LotteryDetails.Count > 0) ||
            //    (Data.LotteryWiners != null && Data.LotteryWiners.Count > 0))
            //{
            //    OperationResult op1 = new OperationResult();
            //    op1.Result = ActionResult.Failed;
            //    op1.AddMessage(Model.Enums.UserMessageKey.JustDeactiveLottery);
            //    return op1;
            //}

            OperationResult op = base.Delete(ID);

            if (op.Result == ActionResult.Done)
            {
                //Lottery_Action
                #region Lottery_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%description%", Data.Description);
                eArg.EventVariables.Add("%status%", EnumUtil.GetEnumElementPersianTitle((LotteryStatus)Data.Status));
                eArg.EventVariables.Add("%action%", "حذف قرعه کشی");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Lottery_Action, eArg);

                #endregion
            }
            return op;
        }

        public override OperationResult Insert(Lottery Data)
        {

            OperationResult op = base.Insert(Data);

            if (op.Result == ActionResult.Done)
            {
                //Lottery_Action
                #region Lottery_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%action%", "ثبت قرعه کشی جديد");
                eArg.EventVariables.Add("%description%", Data.Description);
                eArg.EventVariables.Add("%status%", EnumUtil.GetEnumElementPersianTitle((LotteryStatus)Data.Status));
                
                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Lottery_Action, eArg);

                #endregion
            }



            return op;
        }
        
        public override OperationResult Update(Lottery Data)
        {
            OperationResult op = base.Update(Data);

            if (op.Result == ActionResult.Done)
            {
                //Lottery_Action
                #region Lottery_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%action%", "بروزرسانی قرعه کشی");
                eArg.EventVariables.Add("%description%", Data.Description);
                eArg.EventVariables.Add("%status%", EnumUtil.GetEnumElementPersianTitle((LotteryStatus)Data.Status));

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Lottery_Action, eArg);

                #endregion
            }
            return op;
        }

    }
}