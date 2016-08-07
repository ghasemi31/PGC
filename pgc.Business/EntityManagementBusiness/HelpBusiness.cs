using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using kFrameWork.UI;
using pgc.Model.Enums;

namespace pgc.Business
{
    public class HelpBusiness:BaseEntityManagementBusiness<Help,pgcEntities>
    {
        public HelpBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, HelpPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

          

            var Result = Search_Where(Context.Helps, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Body,
                    
                    
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(HelpPattern Pattern)
        {
            return Search_Where(Context.Helps, Pattern).Count();
        }

        public IQueryable<Help> Search_Where(IQueryable<Help> list, HelpPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title) ||
                    f.Body.Contains(Pattern.Title));

            
            return list;
        }

        public override OperationResult Insert(Help Data)
        {
            OperationResult op = base.Insert(Data);

            if (op.Result == ActionResult.Done)
            {
                //Help_Action
                #region Help_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%body%", Data.Body);
                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%action%", " ایجاد");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Help_Action, eArg);

                #endregion
            }
            return op;
        }

        public override OperationResult Update(Help Data)
        {
            OperationResult op=base.Update(Data);

            if (op.Result == ActionResult.Done || op.Messages.Contains(UserMessageKey.Succeed))
            {
                //Help_Action
                #region Help_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%body%", Data.Body);
                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%action%", " بروزرسانی");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Help_Action, eArg);

                #endregion
            }

            return op;
        }

        public override OperationResult Delete(long ID)
        {
            Help Data = new HelpBusiness().Retrieve(ID);
            OperationResult op = base.Delete(ID);

            if (op.Result == ActionResult.Done || op.Messages.Contains(UserMessageKey.Succeed))
            {
                //Help_Action
                #region Help_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%body%", Data.Body);
                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%action%", " ایجاد");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Help_Action, eArg);

                #endregion
            }
            return op;
        }
    }
}