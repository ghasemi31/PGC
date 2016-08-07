using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.General
{
    public class BranchOrderTitleBusiness
    {
        pgcEntities db = new pgcEntities();
        public IQueryable<BranchOrderTitle> RetriveBranchOrder()
        {
            return db.BranchOrderTitles.Where(o => o.IsOrderForAllBranch);
        }
    }
}
