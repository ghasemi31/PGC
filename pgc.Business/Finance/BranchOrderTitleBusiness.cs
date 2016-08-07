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
    public class BranchOrderTitleBusiness:BaseEntityManagementBusiness<BranchOrderTitle,pgcEntities>
    {
        public BranchOrderTitleBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchOrderTitlePattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.BranchOrderTitles, Pattern)
                .OrderBy(f => f.BranchOrderTitleGroup.DisplayOrder)
                .ThenBy(f=>f.DisplayOrder)
                .Select(f=> new {
                    GroupTitle= f.BranchOrderTitleGroup.Title,
                    f.DisplayOrder,
                    f.Price,
                    f.Title,
                    f.ID,
                    f.Status
                });                

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public IQueryable<BranchOrderTitle> Search_SelectPrint(BranchOrderTitlePattern Pattern)
        {
            var Result = Search_Where(Context.BranchOrderTitles, Pattern)
                .OrderBy(f => f.BranchOrderTitleGroup.DisplayOrder)
                .ThenBy(f => f.DisplayOrder);

            return Result;
        }


        public int Search_Count(BranchOrderTitlePattern Pattern)
        {
            return Search_Where(Context.BranchOrderTitles, Pattern).Count();
        }

        public IQueryable<BranchOrderTitle> Search_Where(IQueryable<BranchOrderTitle> list, BranchOrderTitlePattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern

            if (Pattern.ID > 0)
                list = list.Where(f => f.ID== Pattern.ID);

            if (Pattern.Group_ID > 0)
                list = list.Where(f => f.BranchOrderTitleGroup_ID == Pattern.Group_ID);

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            if (BasePattern.IsEnumAssigned(Pattern.Status))
                list = list.Where(f => f.Status == (int)Pattern.Status);

            switch (Pattern.Price.Type)
            {
                case RangeType.Between:
                    if (Pattern.Price.HasFirstNumber && Pattern.Price.HasSecondNumber)
                        list = list.Where(f => f.Price >= Pattern.Price.FirstNumber
                            && f.Price <= Pattern.Price.SecondNumber);
                    break;
                case RangeType.GreatherThan:
                    if (Pattern.Price.HasFirstNumber)
                        list = list.Where(f => f.Price >= Pattern.Price.FirstNumber);
                    break;
                case RangeType.LessThan:
                    if (Pattern.Price.HasFirstNumber)
                        list = list.Where(f => f.Price <= Pattern.Price.FirstNumber);
                    break;
                case RangeType.EqualTo:
                    if (Pattern.Price.HasFirstNumber)
                        list = list.Where(f => f.Price == Pattern.Price.FirstNumber);
                    break;
                case RangeType.Nothing:
                default:
                    break;
            }

            return list;
        }

        public IQueryable<BranchOrderTitle> GetAllOrderTitle()
        {
            var result = Context.BranchOrderTitles.Where(f => f.Status == (int)BranchOrderTitleStatus.Enabled).OrderBy(g => g.Title);
            if (result.Count() > 0)
                return result.OrderBy(g => g.Title);
            else
                return new List<BranchOrderTitle>().AsQueryable();
        }

        public IQueryable<BranchOrderTitle> GetAllOrderTitleByGroup(long group_ID)
        {
            var result=Context.BranchOrderTitles.Where(f => f.Status == (int)BranchOrderTitleStatus.Enabled && f.BranchOrderTitleGroup_ID == group_ID);
            if (result.Count() > 0)
                return result.OrderBy(g => g.Title);
            else
                return new List<BranchOrderTitle>().AsQueryable();
        }



        public override OperationResult Insert(BranchOrderTitle Data)
        {
            OperationResult op = new OperationResult();

            if (Context.BranchOrderTitles.Any(f => f.Title == Data.Title))
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchOrderTitle_DuplicateTitle);
                return op;
            }

            op = base.Insert(Data);



            //Event Rasing
            if (op.Result == ActionResult.Done)
            {
                #region BranchOrderTitle_Action

                SystemEventArgs eArg = new SystemEventArgs();
                
                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%action%", "ایجاد");

                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.Price) + " ریال");
                eArg.EventVariables.Add("%status%", EnumUtil.GetEnumElementPersianTitle((BranchOrderTitleStatus)Data.Status));

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrderTitle_Action, eArg);

                #endregion
            }


            return op;
        }

        public override OperationResult Update(BranchOrderTitle Data)
        {


            OperationResult op = new OperationResult();

            if (Context.BranchOrderTitles.Any(f => f.Title == Data.Title && f.ID != Data.ID))
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchOrderTitle_DuplicateTitle);
                return op;
            }


            op = base.Update(Data);




            //Event Rasing
            if (op.Result == ActionResult.Done)
            {
                #region BranchOrderTitle_Action

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%action%", "ویرایش");

                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.Price) + " ریال");
                eArg.EventVariables.Add("%status%", EnumUtil.GetEnumElementPersianTitle((BranchOrderTitleStatus)Data.Status));

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrderTitle_Action, eArg);

                #endregion
            }


            return op;
        }

        public override OperationResult Delete(long ID)
        {
            BranchOrderTitle Data = Retrieve(ID);

            OperationResult op = new OperationResult();

            if (Context.BranchOrderDetails.Any(f => f.BranchOrderTitle_ID == ID) ||
                Context.BranchLackOrderDetails.Any(f => f.BranchOrderTitle_ID == ID) ||
                Context.BranchReturnOrderDetails.Any(f => f.BranchOrderTitle_ID == ID))
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchOrderTitlePreventDeleteCauseHasDetails);
                return op;
            }


            op = base.Delete(ID);




            //Event Rasing
            if (op.Result == ActionResult.Done)
            {
                #region BranchOrderTitle_Action

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));

                eArg.EventVariables.Add("%action%", "حذف");

                eArg.EventVariables.Add("%title%", Data.Title);
                eArg.EventVariables.Add("%cost%", UIUtil.GetCommaSeparatedOf(Data.Price) + " ریال");
                eArg.EventVariables.Add("%status%", EnumUtil.GetEnumElementPersianTitle((BranchOrderTitleStatus)Data.Status));

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOrderTitle_Action, eArg);

                #endregion
            }


            return op;
        }
    }
}