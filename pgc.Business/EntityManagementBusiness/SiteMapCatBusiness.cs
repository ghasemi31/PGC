using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class SiteMapCatBusiness:BaseEntityManagementBusiness<SiteMapCat,pgcEntities>
    {
        public SiteMapCatBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, SiteMapCatPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.SiteMapCats, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.NavigateUrl,
                    f.IsVisible
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(SiteMapCatPattern Pattern)
        {
            return Search_Where(Context.SiteMapCats, Pattern).Count();
        }

        public IQueryable<SiteMapCat> Search_Where(IQueryable<SiteMapCat> list, SiteMapCatPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            return list;
        }

        public override OperationResult Delete(long ID)
        {
            SiteMapCat Data = Retrieve(ID);
            if (Data.SiteMapItems != null && Data.SiteMapItems.Count > 0)
            {
                OperationResult op = new OperationResult();
                op.Result = ActionResult.Failed;
                op.AddMessage(Model.Enums.UserMessageKey.FirstDeletOrMoveSitemapItems);
                return op;
            }

            return base.Delete(ID);
        }
    }
}