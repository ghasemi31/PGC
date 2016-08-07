using kFrameWork.Business;
using kFrameWork.Util;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business
{
    public class CircularBusiness : BaseEntityManagementBusiness<Circular, pgcEntities>
    {
        public CircularBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, CircularPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.Circulars, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(CircularPattern Pattern)
        {
            return Search_Where(Context.Circulars, Pattern).Count();
        }

        public IQueryable<Circular> Search_Where(IQueryable<Circular> list, CircularPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title) ||
                    f.Body.Contains(Pattern.Title));
            return list;
        }

        public Branch retriveBranch(long branchId)
        {
            return Context.Branches.SingleOrDefault(m => m.ID == branchId);
        }

        public List<Branch> retriveCircularBranch(Circular circular)
        {
            return circular.Branches.ToList();
        }

    }
}