using kFrameWork.Business;
using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business
{
    public class AgentCircularBusiness : BaseEntityManagementBusiness<Circular, pgcEntities>
    {
        public AgentCircularBusiness()
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
            Branch branch = new BranchBusiness().Retrieve(UserSession.User.Branch_ID.Value);
            list = branch.Circulars.AsQueryable().Where(c=>c.IsVisible);
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title) ||
                    f.Body.Contains(Pattern.Title));
            return list;
        }


        public IQueryable<Circular> RetriveLastCircular()
        {
            Branch branch = new BranchBusiness().Retrieve(UserSession.User.Branch_ID.Value);
            return branch.Circulars.AsQueryable().Where(c => c.IsVisible).OrderByDescending(c=>c.Date).Take(3);
        }
    }
}
