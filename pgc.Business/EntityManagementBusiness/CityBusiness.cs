using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class CityBusiness:BaseEntityManagementBusiness<City,pgcEntities>
    {
        public CityBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, CityPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.Cities, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    Province = f.Province.Title
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(CityPattern Pattern)
        {
            return Search_Where(Context.Cities, Pattern).Count();
        }

        public IQueryable<City> Search_Where(IQueryable<City> list, CityPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            if (Pattern.Province_ID > 0)
                list = list.Where(f => f.Province_ID == Pattern.Province_ID);

            return list;
        }

        public override OperationResult Delete(long ID)
        {
            City Data = Retrieve(ID);
            if (Data.Users != null && Data.Users.Count > 0)
            {
                OperationResult op = new OperationResult();
                op.Result = ActionResult.Failed;
                op.AddMessage(Model.Enums.UserMessageKey.UsersUseCitySoCantDelete);
                return op;
            }

            return base.Delete(ID);
        }
    }
}