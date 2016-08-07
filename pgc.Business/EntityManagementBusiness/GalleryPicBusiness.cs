using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class GalleryPicBusiness:BaseEntityManagementBusiness<GalleryPic,pgcEntities>
    {
        public GalleryPicBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, GalleryPicPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.GalleryPics, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Description,
                    f.DispOrder,
                    Gallery = f.Gallery.Title
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(GalleryPicPattern Pattern)
        {
            return Search_Where(Context.GalleryPics, Pattern).Count();
        }

        public IQueryable<GalleryPic> Search_Where(IQueryable<GalleryPic> list, GalleryPicPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern

            if (!string.IsNullOrEmpty(Pattern.Description))
                list = list.Where(f => f.Description.Contains(Pattern.Description));

            if (Pattern.Gallery_ID > 0)
                list = list.Where(f => f.Gallery_ID == Pattern.Gallery_ID);

            return list;
        }
       
    }
}