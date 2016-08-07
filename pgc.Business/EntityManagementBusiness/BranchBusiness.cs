using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using kFrameWork.UI;
using System.Collections.Generic;

namespace pgc.Business
{
    public class BranchBusiness : BaseEntityManagementBusiness<Branch, pgcEntities>
    {
        public BranchBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.Branches, Pattern)
                .OrderBy(f => f.DispOrder)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.UrlKey,
                    f.DispOrder,
                    f.IsActive

                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(BranchPattern Pattern)
        {
            return Search_Where(Context.Branches, Pattern).Count();
        }

        public IQueryable<Branch> Search_Where(IQueryable<Branch> list, BranchPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title) ||
                    f.Summary.Contains(Pattern.Title) || f.BranchInfo.Contains(Pattern.Title) ||
                    f.Body.Contains(Pattern.Title));

            if (!string.IsNullOrEmpty(Pattern.UrlKey))
                list = list.Where(f => f.UrlKey.Contains(Pattern.UrlKey));

            if (BasePattern.IsEnumAssigned(Pattern.AllowOnlineOrder))
            {
                bool allow = bool.Parse(Pattern.AllowOnlineOrder.ToString());
                list = list.Where(f => f.AllowOnlineOrder == allow);
            }
            return list;
        }


        public override OperationResult Insert(Branch Data)
        {
            OperationResult op = base.Insert(Data);
            if (op.Result == ActionResult.Done)
            {
                //Branch_New_Admin
                #region Branch_New_Admin

                SystemEventArgs e = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                e.Related_Doer = doer;
                e.Related_Branch = Data;

                e.EventVariables.Add("%user%", doer.FullName);
                e.EventVariables.Add("%username%", doer.Username);
                e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                e.EventVariables.Add("%mobile%", doer.Mobile);
                e.EventVariables.Add("%email%", doer.Email);

                e.EventVariables.Add("%title%", Data.Title);
                e.EventVariables.Add("%description%", Data.Description);
                e.EventVariables.Add("%body%", Data.Body);
                e.EventVariables.Add("%summary%", Data.Summary);

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Branch_New_Admin, e);

                #endregion
            }


            return op;
        }


        public override OperationResult Update(Branch Data)
        {
            OperationResult op = base.Update(Data);

            if (op.Result == ActionResult.Done || op.Messages.Contains(UserMessageKey.Succeed))
            {
                //Branch_Change_Admin
                #region Branch_Change_Admin

                SystemEventArgs e = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                e.Related_Doer = doer;
                e.Related_Branch = Data;

                e.EventVariables.Add("%user%", doer.FullName);
                e.EventVariables.Add("%username%", doer.Username);
                e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                e.EventVariables.Add("%mobile%", doer.Mobile);
                e.EventVariables.Add("%email%", doer.Email);

                e.EventVariables.Add("%title%", Data.Title);
                e.EventVariables.Add("%description%", Data.Description);
                e.EventVariables.Add("%body%", Data.Body);
                e.EventVariables.Add("%summary%", Data.Summary);

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Branch_Change_Admin, e);

                #endregion
            }

            return op;
        }


        public override OperationResult Delete(long ID)
        {
            Branch Data = new BranchBusiness().Retrieve(ID);

            OperationResult op1 = new OperationResult();
            op1.Result = ActionResult.Done;

            if (Data.BranchPics != null && Data.BranchPics.Count > 0)
            {
                op1.Result = ActionResult.Failed;
                op1.AddMessage(Model.Enums.UserMessageKey.FirstDeleteOrMoveBranchPic);
            }

            if (Data.Users != null && Data.Users.Count > 0)
            {
                op1.Result = ActionResult.Failed;
                op1.AddMessage(Model.Enums.UserMessageKey.FirstDeleteOrMoveUsers);
            }

            if (Data.BranchTransactions.Count() > 0 || Data.BranchFinanceLogs.Count() > 0 || Data.Orders.SelectMany(f => f.OnlinePayments).Count() > 0)
            {
                op1.Result = ActionResult.Failed;
                op1.AddMessage(Model.Enums.UserMessageKey.BranchDeletePreventCauseHasTransactions);
            }



            if (op1.Result == ActionResult.Failed)
                return op1;

            OperationResult op = base.Delete(ID);

            if (op.Result == ActionResult.Done || op.Messages.Contains(UserMessageKey.Succeed))
            {
                //Branch_Remove_Admin
                #region Branch_Remove_Admin

                SystemEventArgs e = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                e.Related_Doer = doer;
                e.Related_Branch = Data;

                e.EventVariables.Add("%user%", doer.FullName);
                e.EventVariables.Add("%username%", doer.Username);
                e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                e.EventVariables.Add("%mobile%", doer.Mobile);
                e.EventVariables.Add("%email%", doer.Email);

                e.EventVariables.Add("%title%", Data.Title);
                e.EventVariables.Add("%description%", Data.Description);
                e.EventVariables.Add("%body%", Data.Body);
                e.EventVariables.Add("%summary%", Data.Summary);

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Branch_Remove_Admin, e);

                #endregion
            }

            return op;
        }


        public OperationResult SetActiveOrDeactive(long ID,bool GetActive)
        {
            OperationResult op = new OperationResult();
            Branch branch = Retrieve(ID);

            if (branch.IsActive && GetActive)
            {
                op.Result = ActionResult.Error;
                op.AddMessage(UserMessageKey.BranchIsAlreadyActive);
                return op;
            }
            else if (!branch.IsActive && !GetActive)
            {
                op.Result = ActionResult.Error;
                op.AddMessage(UserMessageKey.BranchIsAlreadyDeActive);
                return op;
            }

            if (!GetActive && BranchCreditBusiness.GetBranchCredit(ID) != 0)
            {
                op.Result = ActionResult.Error;
                op.AddMessage(UserMessageKey.BranchCreditIsNotBalanced);
                return op;
            }

            branch.IsActive = GetActive;
            op = Update(branch);

            if (op.Result == ActionResult.Done && GetActive)
            {
                #region Branch_Activation

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%title%", branch.Title);

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Branch_Activation, eArg);

                #endregion
            }
            else if (op.Result == ActionResult.Done && !GetActive)
            {
                #region Branch_Activation

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%title%", branch.Title);

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Branch_DeActivation, eArg);

                #endregion
            }

            if (op.Result == ActionResult.Done)
            {
                op.Messages.Clear();

                if (GetActive)
                    op.AddMessage(UserMessageKey.Branch_ActivationIsComplete);
                else
                    op.AddMessage(UserMessageKey.Branch_DeActivationIsComplete);
            }
            return op;
        }
        public List<Branch> GetAllBranch()
        {
            return Context.Branches.Where(m => m.IsActive).OrderBy(m => m.Title).OrderBy(m => m.BranchType).ToList();
        }
    }
}