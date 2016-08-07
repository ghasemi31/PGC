using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using kFrameWork.UI;

namespace pgc.Business
{
    public class AdvBusiness:BaseEntityManagementBusiness<Advertisement,pgcEntities>
    {
        public AdvBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, AdvPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.Advertisements, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.RegPersianDate,
                    f.ExpirePersianDate
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(AdvPattern Pattern)
        {
            return Search_Where(Context.Advertisements, Pattern).Count();
        }

        public IQueryable<Advertisement> Search_Where(IQueryable<Advertisement> list, AdvPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            
          
            return list;
        }


        public override Advertisement Retrieve(long ID)
        {
            return Context.Advertisements.Where(a => a.ID == ID).SingleOrDefault();
        }

        public override OperationResult Delete(long ID)
        {
            Advertisement Data = new AdvBusiness().Retrieve(ID);
            OperationResult op = base.Delete(ID);

            if (op.Result == ActionResult.Done || op.Messages.Contains(UserMessageKey.Succeed))
            {
                //Advertisement_Action
                #region Advertisement_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%action%", "حذف تبلیغات");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Advertisement_Action, eArg);

                #endregion
            }


            string path = Retrieve(ID).FileAddress;
            if (op.Result == ActionResult.Done)
            {
                IOUtil.DeleteFile(path, true);
            }
            return op;
        }


        public override OperationResult Insert(Advertisement Data)
        {
            
            OperationResult op= base.Insert(Data);

            if (op.Result==ActionResult.Done)
            {
                //Advertisement_Action
                #region Advertisement_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%action%", "ثبت تبلیغات جديد");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Advertisement_Action, eArg);

                #endregion
            }



            return op;
        }


        public override OperationResult Update(Advertisement Data)
        {
            
            OperationResult op = base.Update(Data);

            if (op.Result == ActionResult.Done || op.Messages.Contains(UserMessageKey.Succeed))
            {

                //Advertisement_Action
                #region Advertisement_Action

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%action%", "بروزرسانی تبلیغات");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Advertisement_Action, eArg);

                #endregion
            }

            return op;
        }

        public override OperationResult Validate(Advertisement Data, SaveValidationMode Mode)
        {

            OperationResult res = new OperationResult();
            if (Data.ExpirePersianDate.CompareTo(Data.RegPersianDate) < 0)
            {
                res.Result = ActionResult.Error;
                res.AddMessage(UserMessageKey.InvalidExpireDate);
                return res;
            }
            return base.Validate(Data, Mode);
        }

        public PanelPage RetrivePage(long PageID)
        {

          return Context.PanelPages.FirstOrDefault(c => c.ID == PageID);

        }

    }
}