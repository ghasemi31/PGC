using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using kFrameWork.Model;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Util;

namespace pgc.Business.General
{
    public class GalleryBusiness
    {
        pgcEntities db = new pgcEntities();

        public IQueryable Gallery_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            //var Result = Gallery_Where(db.Galleries).OrderByDescending(f => f.DispOrder);
            //    //.Select(f => new
            //    //{
            //    //    f.ID,
            //    //    f.Title,
            //    //    f.GalleryThumbImagePath
            //    //});

            //return Result.Skip(startRowIndex).Take(maximumRows).ToList();

            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Gallery_Where(db.Galleries)
                .OrderByDescending(f => f.DispOrder)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.GalleryThumbImagePath,
                    count = f.GalleryPics.Count
                });

            return Result.Skip(startRowIndex).Take(maximumRows);

        }
        public int Gallery_Count()
        {
            return Gallery_Where(db.Galleries).Count();
        }

        public IQueryable<Gallery> Gallery_Where(IQueryable<Gallery> list)
        {
            return list;
        }


        public List<GalleryPic> GetGalleryPic(int gallery_id)
        {
            return db.GalleryPics.Where(g => g.Gallery_ID == gallery_id).OrderBy(g => g.DispOrder).ToList();
        }

        public Gallery GetGallery(long id)
        {
            return db.Galleries.FirstOrDefault(g=>g.ID==id);
        }

    }
}
