using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using kFrameWork.Model;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using pgc.Business.Core;
using kFrameWork.UI;
using kFrameWork.Util;

namespace pgc.Business
{
    public class BranchFinanceLogBusiness : BaseEntityManagementBusiness<BranchFinanceLog, pgcEntities>
    {
        public BranchFinanceLogBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchFinanceLogPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.BranchFinanceLogs, Pattern)
               .OrderByDescending(f => f.Date);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(BranchFinanceLogPattern Pattern)
        {
            return Search_Where(Context.BranchFinanceLogs, Pattern).Count();
        }

        public IQueryable<BranchFinanceLog> Search_Where(IQueryable<BranchFinanceLog> list, BranchFinanceLogPattern Pattern)
        {
            if (Pattern == null)
                return list;

            if (Pattern.ID > 0)
                return list.Where(f => f.ID == Pattern.ID);

            if (Pattern.Branch_ID > 0)
                list = list.Where(f => f.Branch_ID == Pattern.Branch_ID);
            
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f =>  f.Description.Contains(Pattern.Title) ||
                                        f.UserName.Contains(Pattern.Title) ||
                                        f.BranchTitle.Contains(Pattern.Title));

            if (BasePattern.IsEnumAssigned(Pattern.ActionType))
                list = list.Where(f => f.ActionType == (int)Pattern.ActionType);

            if (BasePattern.IsEnumAssigned(Pattern.LogType))
                list = list.Where(f => f.LogType == (int)Pattern.LogType);

            if (Pattern.LogType_ID > 0)
                list = list.Where(f => f.LogType_ID == Pattern.LogType_ID);

            if (BasePattern.IsEnumAssigned(Pattern.ActionType))
                list = list.Where(f => f.ActionType == (int)Pattern.ActionType);


            switch (Pattern.PersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.PersianDate.HasFromDate && Pattern.PersianDate.HasToDate)
                        list = list.Where(f => (f.PersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0 && f.PersianDate.CompareTo(Pattern.PersianDate.ToDate) <= 0));
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => (f.PersianDate.CompareTo(Pattern.PersianDate.Date) >= 0));
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => (f.PersianDate.CompareTo(Pattern.PersianDate.Date) <= 0));
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => (f.PersianDate.CompareTo(Pattern.PersianDate.Date) == 0));
                    break;
            }

            return list;
        }

        public IQueryable<BranchFinanceLog> Search_SelectPrint(BranchFinanceLogPattern Pattern)
        {
            return Search_Where(Context.BranchFinanceLogs, Pattern).OrderByDescending(f => f.Date); ;
        }


        

        public static OperationResult InsertLog(BranchFinanceLogActionType Type, long LogType_ID,BranchFinanceLogType LogType)
        {
            try
            {

                BranchFinanceLog log = new BranchFinanceLog()
                {
                    ActionType = (int)Type,                   
                    Date = DateTime.Now,
                    LogType = (int)LogType,
                    LogType_ID = LogType_ID,
                    PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now),
                    User_ID = UserSession.UserID,
                    UserName=""
                };
             
                if (UserSession.IsUserLogined && UserSession.UserID > 0)
                    log.UserName = UserSession.User.Fname + " " + UserSession.User.Lname;



                switch (Type)
                {
                    case BranchFinanceLogActionType.BranchOrderAgentInserting:
                    case BranchFinanceLogActionType.BranchOrderAgentEditing:
                    case BranchFinanceLogActionType.BranchOrderAdminEditing:
                    case BranchFinanceLogActionType.BranchOrderAdminConfirmation:
                    case BranchFinanceLogActionType.BranchOrderAdminCanclation:
                    case BranchFinanceLogActionType.BranchOrderAdminRollingBackCanclation:
                    case BranchFinanceLogActionType.BranchOrderAdminFinalization:
                    case BranchFinanceLogActionType.BranchOrderAdminShipmentUpdate:
                    case BranchFinanceLogActionType.BranchOrderAdminRollingBackFinalized:
                    case BranchFinanceLogActionType.BranchOrderAgentDelete:
                    case BranchFinanceLogActionType.BranchOrderAdminDelete:

                        BranchOrder order = new BranchOrderBusiness().Retrieve(LogType_ID);
                        
                        log.Branch_ID = order.Branch_ID;
                        log.BranchTitle = order.Branch.Title;

                        log.Description = string.Format("کد درخواست {0} به مبلغ {1} ریال",
                                                    order.ID.ToString(),
                                                    UIUtil.GetCommaSeparatedOf(order.TotalPrice));


                        if (Type == BranchFinanceLogActionType.BranchOrderAdminShipmentUpdate && order.ShipmentStatus_ID.HasValue)
                            log.Description += string.Format("، به وضعیت {0}", order.BranchOrderShipmentState.Title);
                        
                            
                            break;


                    case BranchFinanceLogActionType.BranchOrderEditBeforConfirm:
                    case BranchFinanceLogActionType.BranchOrderAdminEditBeforConfirm:
                        BranchOrder edit_order = new BranchOrderBusiness().Retrieve(LogType_ID);

                        log.Branch_ID = edit_order.Branch_ID;
                        log.BranchTitle = edit_order.Branch.Title;

                        log.Description = string.Format("کد درخواست {0} به مبلغ {1} ریال (ویرایش قبل از تایید مدیر)",
                                                    edit_order.ID.ToString(),
                                                    UIUtil.GetCommaSeparatedOf(edit_order.TotalPrice));


                        if (Type == BranchFinanceLogActionType.BranchOrderAdminShipmentUpdate && edit_order.ShipmentStatus_ID.HasValue)
                            log.Description += string.Format("، به وضعیت {0}", edit_order.BranchOrderShipmentState.Title);
                        
                            
                            break;

                    case BranchFinanceLogActionType.BranchLackOrderAgentInserting:
                    case BranchFinanceLogActionType.BranchLackOrderAgentEditing:
                    case BranchFinanceLogActionType.BranchLackOrderAdminEditing:
                    case BranchFinanceLogActionType.BranchLackOrderAdminConfirmation:
                    case BranchFinanceLogActionType.BranchLackOrderAdminCanclation:
                    case BranchFinanceLogActionType.BranchLackOrderAdminRollingBackCanclation:
                    case BranchFinanceLogActionType.BranchLackOrderAgentDelete:
                    case BranchFinanceLogActionType.BranchLackOrderAdminDelete:

                         BranchLackOrder lackOrder = new BranchLackOrderBusiness().Retrieve(LogType_ID);

                         log.Branch_ID = lackOrder.BranchOrder.Branch_ID;
                         log.BranchTitle = lackOrder.BranchOrder.Branch.Title;

                        log.Description = string.Format("کد کسری {0} به مبلغ {1} ریال مربوط به درخواست کد {2} (مبلغ {3} ریال)",
                                                    lackOrder.ID.ToString(),
                                                    UIUtil.GetCommaSeparatedOf(lackOrder.TotalPrice),
                                                    lackOrder.BranchOrder_ID.ToString(),
                                                    UIUtil.GetCommaSeparatedOf(lackOrder.BranchOrder.TotalPrice));

                        break;





                    case BranchFinanceLogActionType.BranchReturnOrderAgentInserting:
                    case BranchFinanceLogActionType.BranchReturnOrderAgentEditing:
                    case BranchFinanceLogActionType.BranchReturnOrderAdminEditing:
                    case BranchFinanceLogActionType.BranchReturnOrderAdminConfirmation:
                    case BranchFinanceLogActionType.BranchReturnOrderAdminCanclation:
                    case BranchFinanceLogActionType.BranchReturnOrderAdminRollingBackCanclation:
                    case BranchFinanceLogActionType.BranchReturnOrderAdminFinalization:
                    case BranchFinanceLogActionType.BranchReturnOrderAgentDelete:
                    case BranchFinanceLogActionType.BranchReturnOrderAdminRollingBackFinalized:
                    case BranchFinanceLogActionType.BranchReturnOrderAdminDelete:
                    default:

                         BranchReturnOrder returnOrder = new BranchReturnOrderBusiness().Retrieve(LogType_ID);

                         log.Branch_ID = returnOrder.Branch_ID;
                         log.BranchTitle = returnOrder.Branch.Title;

                        log.Description = string.Format("کد مرجوعی {0} به مبلغ {1} ریال",
                                                    returnOrder.ID.ToString(),
                                                    UIUtil.GetCommaSeparatedOf(returnOrder.TotalPrice));


                        break;
                }



                OperationResult op= new BranchFinanceLogBusiness().Insert(log);
                op.Data.Add("ID", log.ID);
                return op;
            }
            catch (Exception)
            {
                OperationResult op = new OperationResult() { Result = ActionResult.Failed };
                op.AddMessage(UserMessageKey.BranchOrderLogInsertingFailed);
                return op;
            }
        }
    }
}
