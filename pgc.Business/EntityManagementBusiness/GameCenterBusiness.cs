using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using System.Collections.Generic;
using kFrameWork.UI;

namespace pgc.Business
{
    public class GameCenterBusiness:BaseEntityManagementBusiness<GameCenter,pgcEntities>
    {
        public GameCenterBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, GameCenterPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.GameCenters, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    City = f.City.Title,
                    f.TItle,
                    f.Description
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(GameCenterPattern Pattern)
        {
            return Search_Where(Context.GameCenters, Pattern).Count();
        }

        public IQueryable<GameCenter> Search_Where(IQueryable<GameCenter> list, GameCenterPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern
            
    

            if (!string.IsNullOrEmpty(Pattern.Description))
                list = list.Where(u => u.Description.Contains(Pattern.Description)||u.TItle.Contains(Pattern.Description));

            if (Pattern.City_ID > 0)
                list = list.Where(u => u.City_ID == Pattern.City_ID);

            

            if (Pattern.Province_ID > 0)
                list = list.Where(u => u.City.Province_ID == Pattern.Province_ID);

            

            return list;
        }





        public GameCenter RetirveGameCenter(long GameCenterID)
        {
            return Context.GameCenters.Where(u => u.ID == GameCenterID).SingleOrDefault();
        }


     

    }
}