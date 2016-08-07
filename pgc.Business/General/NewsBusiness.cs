using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using kFrameWork.Model;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Util;
using kFrameWork.UI;

namespace pgc.Business.General
{
    public class NewsBusiness
    {
        pgcEntities db = new pgcEntities();

        public IQueryable News_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = News_Where(db.News)
                            .OrderByDescending(f => f.ID);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int News_Count()
        {
            return News_Where(db.News).Count();
        }
        public IQueryable<News> News_Where(IQueryable<News> list)
        {
            if (!(UserSession.IsUserLogined && UserSession.User.AccessLevel.Role == (int)Role.Admin))
                return list.Where(f => f.Status == (int)NewsStatus.Show);
            else
                return list.Where(f => f.Status == (int)NewsStatus.Show || f.Status==(int)NewsStatus.Preview); 
        }



        public News RetriveNews(long ID)
        {
            return db.News.Where(f => f.ID == ID).SingleOrDefault();
        }


        public static List<News> GetTopNews()
        {
            try
            {
                return new pgcEntities().News.Where(f => f.Status == (int)NewsStatus.Show).OrderByDescending(f => f.NewsDate).Take(OptionBusiness.GetInt(OptionKey.NewsNumberInHomePage)).ToList<News>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
