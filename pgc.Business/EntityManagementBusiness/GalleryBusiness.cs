using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class GalleryBusiness:BaseEntityManagementBusiness<Gallery,pgcEntities>
    {
        public GalleryBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, GalleryPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.Galleries, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.DispOrder
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(GalleryPattern Pattern)
        {
            return Search_Where(Context.Galleries, Pattern).Count();
        }

        public IQueryable<Gallery> Search_Where(IQueryable<Gallery> list, GalleryPattern Pattern)
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
            Gallery gal=Retrieve(ID);
            if (gal.GalleryPics != null && gal.GalleryPics.Count > 0)
            {
                OperationResult op = new OperationResult();
                op.Result = ActionResult.Failed;
                op.AddMessage(Model.Enums.UserMessageKey.FirstDeleteGalleryPic);
                return op;
            }
            return base.Delete(ID);
        }
    }
}