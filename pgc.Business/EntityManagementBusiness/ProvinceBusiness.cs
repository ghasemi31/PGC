using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class ProvinceBusiness:BaseEntityManagementBusiness<Province,pgcEntities>
    {
        public ProvinceBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, ProvincePattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.Provinces, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(ProvincePattern Pattern)
        {
            return Search_Where(Context.Provinces, Pattern).Count();
        }

        public IQueryable<Province> Search_Where(IQueryable<Province> list, ProvincePattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            return list;
        }

        public override OperationResult Delete(long ID)
        {
            Province Data = Retrieve(ID);
            if (Data.Cities != null && Data.Cities.Count > 0)
            {
                OperationResult op = new OperationResult();
                op.Result = ActionResult.Failed;
                op.AddMessage(Model.Enums.UserMessageKey.FirstDeletCities);
                return op;
            }

            return base.Delete(ID);
        }
    }
}