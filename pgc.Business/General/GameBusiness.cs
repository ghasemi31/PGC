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
    public class GameBusiness
    {
        pgcEntities db = new pgcEntities();

        public IQueryable<Game> RetriveGameList(int id)
        {
            return db.Games.Where(f => f.Type_Enum == id).OrderByDescending(f => f.DispOrder);
        }

        public Game RetriveGame(string urlKey)
        {
            return db.Games.FirstOrDefault(f => f.UrlKey == urlKey);
        }


        public long AddNewGameGroup(string title)
        {

            Group group = new Group();
            group.Title = title;
            db.Groups.AddObject(group);
            db.SaveChanges();
            return group.ID;
        }

        public OperationResult AddNewGamerToGroup(long groupID, long gamerID)
        {

            OperationResult res = new OperationResult();
            Group group = db.Groups.FirstOrDefault(g => g.ID == groupID);
            User gamer = db.Users.FirstOrDefault(g => g.ID == gamerID);

            try
            {
                group.Users.Add(gamer);

                db.SaveChanges();
                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.Succeed);


                return res;

            }
            catch
            {
                res.Result = ActionResult.Failed;
                res.AddMessage(UserMessageKey.Failed);
                return res;
            }
        }


        public OperationResult removeGamerFromGroup(long groupID, long gamerID)
        {

            OperationResult res = new OperationResult();
            Group group = db.Groups.FirstOrDefault(g => g.ID == groupID);
            User gamer = db.Users.FirstOrDefault(g => g.ID == gamerID);

            try
            {
                group.Users.Remove(gamer);

                db.SaveChanges();
                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.Succeed);


                return res;

            }
            catch
            {
                res.Result = ActionResult.Failed;
                res.AddMessage(UserMessageKey.Failed);
                return res;
            }
        }
    }
}
