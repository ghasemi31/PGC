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

        public IQueryable<Game> GameList()
        {
            return db.Games.OrderBy(f => f.DispOrder);
        }



        public IQueryable gamer_List(int startRowIndex, int maximumRows, long ID)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var order = db.GameOrders.SingleOrDefault(g => g.ID == ID);

            if (order == null || order.Group_ID == null)
                return null;

            var Result = order.Group.Users.AsQueryable()
                .OrderBy(f=>f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.FullName,
                    f.Email,
                    f.NationalCode,

                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int gamer_count(long ID)
        {

            var order = db.GameOrders.SingleOrDefault(g => g.ID == ID);

            if (order == null || order.Group_ID == null)
                return 0;

            return order.Group.Users.Count();

        }



        public User RetriveGamer(string nationalCode)
        {
            return db.Users.SingleOrDefault(u => u.NationalCode == nationalCode);
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



        public OperationResult Validate(long userID, long gameID)
        {
            OperationResult res = new OperationResult();
            Game game = db.Games.FirstOrDefault(g => g.ID == gameID);
            User user = db.Users.FirstOrDefault(g => g.ID == userID);

            if (user.Gender == (int)Gender.Male)
            {
                res.Result = ActionResult.Done;
                return res;
            }
            else
            {

                var groupid = user.Groups.Select(g => g.ID).ToList();

                int gameCount = db.GameOrders.Where(o => o.User_ID == userID || (o.Group_ID != null && groupid.Contains((long)o.Group_ID))).Count();

                if (game.Type_Enum != (int)GameType.Mobile)
                {
                    res.Result = ActionResult.Failed;
                    res.AddCompeleteMessage(UserMessage.CreateUserMessage(0, "msg", 4, 2, 1, "بازیکنان خانم فقط می توانند در بازی های موبایلی شرکت کنند."));
                    return res;
                }

                else if (gameCount >= 3)
                {
                    res.Result = ActionResult.Failed;
                    res.AddCompeleteMessage(UserMessage.CreateUserMessage(0, "msg", 4, 2, 1, "بازیکنان خانم فقط می توانند در 3 بازی شرکت کنند."));
                    return res;
                }

                else
                {

                    res.Result = ActionResult.Done;
                    return res;
                }


            }
        }
        public IQueryable<Game> GetAllGame()
        {
            return db.Games;
        }

        public GameCenter RetriveGameCenter(long id)
        {
            return db.GameCenters.SingleOrDefault(g => g.ID == id);
        }
    }
}
