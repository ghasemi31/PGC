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
using kFrameWork.Util;
using kFrameWork.UI;

namespace pgc.Business
{
    public class BranchCreditBusiness : BaseEntityManagementBusiness<BranchTransaction, pgcEntities>
    {
        public BranchCreditBusiness()
        {
            Context = new pgcEntities();
        }

        public static long GetBranchCredit(long Branch_ID)
        {
            pgcEntities Context = new pgcEntities();
            var result = Context.BranchTransactions.Where(f => f.Branch_ID == Branch_ID);
            if (result.Count() > 0)
                return result.Sum(g => g.BranchCredit - g.BranchDebt);
            else
                return 0;
        }

        public static long GetBranchMinimumCredit(long branch_ID)
        {
            pgcEntities Context = new pgcEntities();
            return Context.Branches.FirstOrDefault(f => f.ID == branch_ID).MinimumCredit;
        }

        public OperationResult CheckCreditOfBranch(long Branch_ID, long NewPrice)
        {
            OperationResult op = new OperationResult();

            Branch branch = Context.Branches.SingleOrDefault(f => f.ID == Branch_ID);

            if (branch == null)
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.Failed);
            }

            if (BranchCreditBusiness.GetBranchCredit(Branch_ID) - NewPrice < branch.MinimumCredit)
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchOrderNewInsufficientCredit);
            }

            if (op.Result == ActionResult.Failed)
                return op;

            op.Result = ActionResult.Done;
            return op;
        }



        #region BranchCredit List

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchCreditPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.BranchTransactions, Pattern)
               .OrderBy(f => f.DisplayOrder);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public long Search_Select(BranchCreditPattern Pattern)
        {
            var Result = Search_Where(Context.BranchTransactions, Pattern);

            if (Result.Count() > 0)
                return Result.Sum(f => f.CurrentCredit - f.CurrentDebt);

            return 0;
        }

        public int Search_Count(BranchCreditPattern Pattern)
        {
            return Search_Where(Context.BranchTransactions, Pattern).Count();
        }

        public IQueryable<BranchCredit> Search_Where(IQueryable<BranchTransaction> list, BranchCreditPattern Pattern)
        {
            List<BranchCredit> result = new List<BranchCredit>();

            foreach (var item in Context.Branches.Where(f=>f.IsActive).OrderBy(f => f.ID))
            {
                BranchCredit credit = new BranchCredit();

                credit.ID = item.ID;
                credit.MinimumCredit = item.MinimumCredit;
                credit.Title = item.Title;
                credit.DisplayOrder = item.DispOrder;

                long currentCredit = 0;
                var creditList=Context.BranchTransactions.Where(f => f.Branch_ID == credit.ID);
                if (creditList.Count() > 0)
                    currentCredit = creditList.Sum(g => g.BranchCredit - g.BranchDebt);

                if (currentCredit < 0)
                {
                    credit.CurrentCredit = 0;
                    credit.CurrentDebt = Math.Abs(currentCredit);
                    credit.Status = BranchCreditStatus.InDebt;
                }
                else
                {
                    credit.CurrentCredit = currentCredit;
                    credit.CurrentDebt = 0;
                    credit.Status = BranchCreditStatus.InCredit;
                }

                result.Add(credit);
            }

            var ResultList = result.AsQueryable();

            if (Pattern == null)
                return ResultList;

            if (Pattern.Branch_ID > 0)
                ResultList = ResultList.Where(f => f.ID == Pattern.Branch_ID);

            if (BasePattern.IsEnumAssigned(Pattern.Status))
                ResultList = ResultList.Where(f => f.Status == Pattern.Status);

            return ResultList;
        }

        public IQueryable<BranchCredit> Search_SelectPrint(BranchCreditPattern Pattern)
        {
            return Search_Where(Context.BranchTransactions, Pattern).OrderBy(f => f.DisplayOrder);
        }



        public OperationResult UpdateBranchMinimumCredit(long BranchID, long MinimumCredit)
        {
            OperationResult op = new OperationResult();
            try
            {
                BranchBusiness business = new BranchBusiness();
                Branch branch = business.Retrieve(BranchID);
                long oldMinimumCredit = branch.MinimumCredit;
                branch.MinimumCredit = MinimumCredit;
                op = business.Update(branch);


                if (op.Result == ActionResult.Done)
                {
                    //Event Rasing
                    #region BranchMinimumCredit_Change

                    SystemEventArgs eArg = new SystemEventArgs();

                    eArg.Related_Branch = branch;
                    eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                    eArg.EventVariables.Add("%username%", Util.GetFullNameWithGender(UserSession.UserID));

                    eArg.EventVariables.Add("%newcredit%", UIUtil.GetCommaSeparatedOf(branch.MinimumCredit) + " ریال");
                    eArg.EventVariables.Add("%oldcredit%", UIUtil.GetCommaSeparatedOf(oldMinimumCredit) + " ریال");
                    eArg.EventVariables.Add("%branchtitle%", eArg.Related_Branch.Title);

                    
                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchMinimumCredit_Change, eArg);
                    #endregion
                }


                return op;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleManualException(ex, "pgc.Business.BranchCreditBusiness.UpdateBranchMinimumCredit");
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.Failed);
                return op;
            }
        }

        public long GetMinimumCredit(long Branch_ID)
        {
            return new BranchBusiness().Retrieve(Branch_ID).MinimumCredit;
        }

        #endregion

    }

    public class BranchCredit
    {
        public BranchCredit()
        {
            Title = "";
        }
        public long ID { get; set; }
        public string Title { get; set; }
        public long DisplayOrder { get; set; }
        public long MinimumCredit { get; set; }
        public long CurrentCredit { get; set; }
        public long CurrentDebt { get; set; }
        public BranchCreditStatus Status { get; set; }
    }
}
