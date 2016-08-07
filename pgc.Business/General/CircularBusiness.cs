using kFrameWork.UI;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.General
{
    public class CircularBusiness
    {
        pgcEntities db = new pgcEntities();


        public IQueryable Circular_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Circular_Where(db.Circulars)
                .OrderByDescending(f => f.Date)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Date
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Circular_Count()
        {
            return Circular_Where(db.Circulars).Count();
        }

        public IQueryable<Circular> Circular_Where(IQueryable<Circular> list)
        {
            return list = db.Circulars.Where(f => f.IsActiveForUser&& f.IsVisible);
        }

        public Circular RetriveCircular(long ID)
        {
            return db.Circulars.Where(f => f.ID == ID).SingleOrDefault();
        }

        public IQueryable<Circular> RetriveLastCircular()
        {
            return db.Circulars.Where(f => f.IsActiveForUser && f.IsVisible).OrderByDescending(c => c.Date).Take(3);
        }

    }
}
