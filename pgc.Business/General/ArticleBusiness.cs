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
    public class ArticleBusiness
    {
        pgcEntities db = new pgcEntities();

        public IQueryable Article_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Article_Where(db.Articles)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Summary

                });

            return Result.Skip(startRowIndex).Take(maximumRows);

        }
        public int Article_Count()
        {
            return Article_Where(db.Articles).Count();
        }
        public IQueryable<Article> Article_Where(IQueryable<Article> list)
        {
            return list;
        }



        public Article RetriveArticle(long id)
        {
            return db.Articles.Where(f => f.ID == id).SingleOrDefault();
        }

    }
}
