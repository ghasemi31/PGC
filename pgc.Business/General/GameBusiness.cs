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
    public class GameBusiness
    {
        pgcEntities db = new pgcEntities();

           public IQueryable<Game> GameList()
        {
            return db.Games;
        }

        public IQueryable<Game> RetriveGameList(int id)
        {
            return db.Games.Where(f => f.Type_Enum == id).OrderByDescending(f => f.DispOrder);
        }

        public Game RetriveGame(string urlKey)
        {
            return db.Games.FirstOrDefault(f => f.UrlKey == urlKey);
        }
    }
}
