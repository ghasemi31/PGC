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
    public class BranchOrderingManagementBusiness : BaseEntityManagementBusiness<Branch, pgcEntities>
    {
        public BranchOrderingManagementBusiness()
        {
            Context = new pgcEntities();
        }

        
        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchOrderingManagementPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.Branch_BranchOrderTitle, Pattern)
               .OrderBy(f => f.DispOrder);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(BranchOrderingManagementPattern Pattern)
        {
            return Search_Where(Context.Branch_BranchOrderTitle, Pattern).Count();
        }

        public IQueryable<Branch> Search_Where(IQueryable<Branch_BranchOrderTitle> list, BranchOrderingManagementPattern Pattern)
        {
            IQueryable<Branch> branches = Context.Branches.Where(f => f.IsActive);


            if (Pattern == null)
                return branches;
           
            if (Pattern.Branch_ID > 0)
                branches = branches.Where(f => f.ID == Pattern.Branch_ID);

            if (Pattern.OrderTitle_ID > 0)
            {
                var branchIDs = Context.Branch_BranchOrderTitle.Where(f => f.BranchOrderTitle_ID == Pattern.OrderTitle_ID).Select(f=>f.Branch_ID);
                branches = branches.Where(f => branchIDs.Contains(f.ID));
            }


            return branches;
        }


        public BranchOrderTitle RetrieveBranchOrderTitle(long OrderTitle_ID)
        {
            return Context.BranchOrderTitles.SingleOrDefault(f => f.ID == OrderTitle_ID);
        }
    }
}
