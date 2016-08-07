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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using kFrameWork.UI;

namespace pgc.Business
{
    public class BranchManualChargeBusiness : BaseEntityManagementBusiness<BranchTransaction, pgcEntities>
    {
        public BranchManualChargeBusiness()
        {
            Context = new pgcEntities();
        }


        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchManualChargePattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.BranchTransactions, Pattern)
                .OrderByDescending(f => f.RegDate)
                .Select(f => new
                {
                    f.ID,
                    Branch=f.Branch.Title,
                    f.Description,
                    f.RegDate,
                    f.RegPersianDate,
                    f.BranchCredit,
                    f.BranchDebt
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(BranchManualChargePattern Pattern)
        {
            return Search_Where(Context.BranchTransactions, Pattern).Count();
        }

        public IQueryable<BranchTransaction> Search_Where(IQueryable<BranchTransaction> list, BranchManualChargePattern Pattern)
        {
           list = list.Where(f => f.TransactionType == (int)BranchTransactionType.ManualCharge);

            //DefaultPattern
            if (Pattern == null)
                return list;


            if (Pattern.Branch_ID > 0)
                list = list.Where(f => f.Branch_ID == Pattern.Branch_ID);

            if (Pattern.Status>0)
                list = list.Where(f => f.BranchCredit == (int)Pattern.Status);


            

            switch (Pattern.PersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.PersianDate.HasFromDate && Pattern.PersianDate.HasToDate)
                        list = list.Where(f => (f.RegPersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0 && f.RegPersianDate.CompareTo(Pattern.PersianDate.ToDate) <= 0));
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => (f.RegPersianDate.CompareTo(Pattern.PersianDate.Date) >= 0));
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => (f.RegPersianDate.CompareTo(Pattern.PersianDate.Date) <= 0));
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => (f.RegPersianDate.CompareTo(Pattern.PersianDate.Date) == 0));
                    break;
            }

            return list;
        }

        public IQueryable<BranchTransaction> Search_SelectPrint(BranchManualChargePattern Pattern)
        {
            return Search_Where(Context.BranchTransactions, Pattern).OrderByDescending(f => f.RegDate);
        }


        public override OperationResult Insert(BranchTransaction Data)
        {
            OperationResult op = new OperationResult();
            op = new BranchTransactionBusiness().InsertBranchManualCharge(Data);

            return op;
        }

    }
}
