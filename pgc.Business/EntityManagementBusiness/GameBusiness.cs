using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using kFrameWork.UI;
using System.Collections.Generic;

namespace pgc.Business
{
    public class GameBusiness : BaseEntityManagementBusiness<Game, pgcEntities>
    {
        public GameBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, GamePattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.Games, Pattern)
                .OrderBy(f => f.DispOrder)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.UrlKey,
                    f.DispOrder

                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(GamePattern Pattern)
        {
            return Search_Where(Context.Games, Pattern).Count();
        }

        public IQueryable<Game> Search_Where(IQueryable<Game> list, GamePattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            if (!string.IsNullOrEmpty(Pattern.UrlKey))
                list = list.Where(f => f.UrlKey.Contains(Pattern.UrlKey));


            return list;
        }


        public override OperationResult Insert(Game Data)
        {
            OperationResult op = base.Insert(Data);
            if (op.Result == ActionResult.Done)
            {
                //Game_New_Admin
                #region Game_New_Admin

                //SystemEventArgs e = new SystemEventArgs();
                //User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                //e.Related_Doer = doer;
                //e.Related_Game = Data;

                //e.EventVariables.Add("%user%", doer.FullName);
                //e.EventVariables.Add("%username%", doer.Username);
                //e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                //e.EventVariables.Add("%mobile%", doer.Mobile);
                //e.EventVariables.Add("%email%", doer.Email);

                //e.EventVariables.Add("%title%", Data.Title);
                //e.EventVariables.Add("%description%", Data.Description);
                //e.EventVariables.Add("%body%", Data.Body);
                //e.EventVariables.Add("%summary%", Data.Summary);

                //EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Game_New_Admin, e);

                #endregion
            }


            return op;
        }


        public override OperationResult Update(Game Data)
        {
            OperationResult op = base.Update(Data);

            if (op.Result == ActionResult.Done || op.Messages.Contains(UserMessageKey.Succeed))
            {
                //Game_Change_Admin
                #region Game_Change_Admin

                //SystemEventArgs e = new SystemEventArgs();
                //User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                //e.Related_Doer = doer;
                //e.Related_Game = Data;

                //e.EventVariables.Add("%user%", doer.FullName);
                //e.EventVariables.Add("%username%", doer.Username);
                //e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                //e.EventVariables.Add("%mobile%", doer.Mobile);
                //e.EventVariables.Add("%email%", doer.Email);

                //e.EventVariables.Add("%title%", Data.Title);
                //e.EventVariables.Add("%description%", Data.Description);
                //e.EventVariables.Add("%body%", Data.Body);
                //e.EventVariables.Add("%summary%", Data.Summary);

                //EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Game_Change_Admin, e);

                #endregion
            }

            return op;
        }


        public override OperationResult Delete(long ID)
        {
            Game Data = new GameBusiness().Retrieve(ID);

            OperationResult op1 = new OperationResult();
            op1.Result = ActionResult.Done;

            //if (Data.GamePics != null && Data.GamePics.Count > 0)
            //{
            //    op1.Result = ActionResult.Failed;
            //    op1.AddMessage(Model.Enums.UserMessageKey.FirstDeleteOrMoveGamePic);
            //}

            //if (Data.Users != null && Data.Users.Count > 0)
            //{
            //    op1.Result = ActionResult.Failed;
            //    op1.AddMessage(Model.Enums.UserMessageKey.FirstDeleteOrMoveUsers);
            //}

            //if (Data.GameTransactions.Count() > 0 || Data.GameFinanceLogs.Count() > 0 || Data.Orders.SelectMany(f => f.OnlinePayments).Count() > 0)
            //{
            //    op1.Result = ActionResult.Failed;
            //    op1.AddMessage(Model.Enums.UserMessageKey.GameDeletePreventCauseHasTransactions);
            //}



            if (op1.Result == ActionResult.Failed)
                return op1;

            OperationResult op = base.Delete(ID);

            if (op.Result == ActionResult.Done || op.Messages.Contains(UserMessageKey.Succeed))
            {
                //Game_Remove_Admin
                #region Game_Remove_Admin

                //SystemEventArgs e = new SystemEventArgs();
                //User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                //e.Related_Doer = doer;
                //e.Related_Game = Data;

                //e.EventVariables.Add("%user%", doer.FullName);
                //e.EventVariables.Add("%username%", doer.Username);
                //e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                //e.EventVariables.Add("%mobile%", doer.Mobile);
                //e.EventVariables.Add("%email%", doer.Email);

                //e.EventVariables.Add("%title%", Data.Title);
                //e.EventVariables.Add("%description%", Data.Description);
                //e.EventVariables.Add("%body%", Data.Body);
                //e.EventVariables.Add("%summary%", Data.Summary);

                //EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Game_Remove_Admin, e);

                #endregion
            }

            return op;
        }


        public List<Game> GetAllGame()
        {
            return Context.Games.OrderBy(m => m.Title).OrderBy(m => m.DispOrder).ToList();
        }
    }
}