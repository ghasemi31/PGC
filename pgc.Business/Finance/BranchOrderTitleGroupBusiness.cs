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
    public class BranchOrderTitleGroupBusiness:BaseEntityManagementBusiness<BranchOrderTitleGroup,pgcEntities>
    {
        public BranchOrderTitleGroupBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchOrderTitleGroupPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.BranchOrderTitleGroups, Pattern)
                .OrderBy(f => f.DisplayOrder);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(BranchOrderTitleGroupPattern Pattern)
        {
            return Search_Where(Context.BranchOrderTitleGroups, Pattern).Count();
        }

        public IQueryable<BranchOrderTitleGroup> Search_Where(IQueryable<BranchOrderTitleGroup> list, BranchOrderTitleGroupPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern

            if (Pattern.ID > 0)
                list = list.Where(f => f.ID== Pattern.ID);

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title));

            return list;
        }

        public override OperationResult Insert(BranchOrderTitleGroup Data)
        {

            OperationResult op = new OperationResult();

            if (Context.BranchOrderTitleGroups.Any(f => f.Title == Data.Title))
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchOrderTitleGroup_DuplicateTitle);
                return op;
            }

            return base.Insert(Data);
        }

        public override OperationResult Update(BranchOrderTitleGroup Data)
        {

            OperationResult op = new OperationResult();

            if (Context.BranchOrderTitleGroups.Any(f => f.Title == Data.Title && f.ID != Data.ID))
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchOrderTitleGroup_DuplicateTitle);
                return op;
            }

            return base.Update(Data);
        }

        public override OperationResult Delete(long ID)
        {
            BranchOrderTitleGroup Data = Retrieve(ID);

            OperationResult op = new OperationResult();

            if (Data.BranchOrderTitles.Count()>0)
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.BranchOrderTitleGroupPreventDeleteCauseHasOrderTitle);
                return op;
            }


            op = base.Delete(ID);

            return op;
        }
    }
}