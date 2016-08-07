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
    public class AgentBranchPicBusiness:BaseEntityManagementBusiness<BranchPic,pgcEntities>
    {
        Branch branch;
        public AgentBranchPicBusiness()
        {
            Context = new pgcEntities();
            
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, AgentBranchPicPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

           

            var Result = Search_Where(Context.BranchPics,Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.ImagePath
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(AgentBranchPicPattern Pattern)
        {
            return Search_Where(Context.BranchPics, Pattern).Count();
        }

        public IQueryable<BranchPic> Search_Where(IQueryable<BranchPic> list, AgentBranchPicPattern Pattern)
        {

            list = list.Where(f => f.Branch_ID == UserSession.User.Branch_ID);

            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern
                
            if (!string.IsNullOrEmpty(Pattern.FileName))
                list = list.Where(f=>f.ImagePath.Contains(Pattern.FileName));
            

            return list;
        }


        public override OperationResult Insert(BranchPic Data)
        {
            Data.ThumbPath = IOUtil.MakeThumbnailOf(Data.ImagePath, 58, 54, "BranchThumb");
            OperationResult op= base.Insert(Data);

            if (op.Result == ActionResult.Done)
            {
                //BranchPic_Action_Agent
                #region BranchPic_Action_Agent

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%title%", Data.Branch.Title);
                eArg.EventVariables.Add("%action%", "ایجاد");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchPic_Action_Agent, eArg);

                #endregion
            }


            return op;
        }

        public override OperationResult Update(BranchPic Data)
        {
            IOUtil.DeleteFile(Data.ThumbPath,true);

            Data.ThumbPath = IOUtil.MakeThumbnailOf(Data.ImagePath, 58, 54, "BranchThumb");

            OperationResult op = base.Update(Data);

            if (op.Result == ActionResult.Done || op.Messages.Contains(UserMessageKey.Succeed))
            {
                //BranchPic_Action_Agent
                #region BranchPic_Action_Agent

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%title%", Data.Branch.Title);
                eArg.EventVariables.Add("%action%", "بروزرسانی");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchPic_Action_Agent, eArg);

                #endregion
            }

            return op;
        }

        public override OperationResult Delete(long ID)
        {
            BranchPic Data = new BranchPicBusiness().Retrieve(ID);
            OperationResult op = base.Delete(ID);

            if (op.Result == ActionResult.Done || op.Messages.Contains(UserMessageKey.Succeed))
            {
                //BranchPic_Action_Agent
                #region BranchPic_Action_Agent

                SystemEventArgs eArg = new SystemEventArgs();
                User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.Related_Doer = doer;

                eArg.EventVariables.Add("%user%", doer.FullName);
                eArg.EventVariables.Add("%username%", doer.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", doer.Mobile);
                eArg.EventVariables.Add("%email%", doer.Email);

                eArg.EventVariables.Add("%title%", Data.Branch.Title);
                eArg.EventVariables.Add("%action%", "حذف");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchPic_Action_Agent, eArg);

                #endregion
            }
            return op;
        }

    }
}