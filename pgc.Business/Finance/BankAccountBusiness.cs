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
    public class BankAccountBusiness : BaseEntityManagementBusiness<BankAccount, pgcEntities>
    {
        public BankAccountBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BankAccountPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.BankAccounts, Pattern)
                .OrderBy(f => f.Title);
            
            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(BankAccountPattern Pattern)
        {
            return Search_Where(Context.BankAccounts, Pattern).Count();
        }

        public IQueryable<BankAccount> Search_Where(IQueryable<BankAccount> list, BankAccountPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            if (Pattern.ID > 0)
                return list.Where(n => n.ID == Pattern.ID);

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            if (!string.IsNullOrEmpty(Pattern.Description))
                list=list.Where(f=>f.Description.Contains(Pattern.Description));

            if (BasePattern.IsEnumAssigned(Pattern.Status))
                list = list.Where(f => f.Status == (int)Pattern.Status);

            return list;
        }

        public override OperationResult Insert(BankAccount Data)
        {
            if (Context.BankAccounts.Any(f => f.Title == Data.Title))
            {
                OperationResult op = new OperationResult();
                op.Result = ActionResult.Error;
                op.AddMessage(UserMessageKey.DuplicateBankAccount);
                return op;
            }
            return base.Insert(Data);
        }

        public override OperationResult Update(BankAccount Data)
        {
            if (Context.BankAccounts.Any(f => f.ID != Data.ID && f.Title == Data.Title))
            {
                OperationResult op = new OperationResult();
                op.Result = ActionResult.Error;
                op.AddMessage(UserMessageKey.DuplicateBankAccount);
                return op;
            }
            return base.Update(Data);
        }

        public override OperationResult Delete(long ID)
        {
            if (Context.BranchPayments.Any(f => f.OfflineBankAccount_ID == ID &&
                (f.OfflinePaymentStatus == (int)BranchOfflinePaymentStatus.Paid ||
                f.OfflinePaymentStatus == (int)BranchOfflinePaymentStatus.Pending)))
            {
                OperationResult op = new OperationResult();
                op.Result = ActionResult.Error;
                op.AddMessage(UserMessageKey.PreventDeleteBankAccountCauseCascading);
                return op;
            }
            return base.Delete(ID);
        }
    }
}