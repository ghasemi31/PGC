using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using pgc.Business.Lookup;
using System.Collections.Generic;

namespace pgc.Business
{
    public class BranchOrderShipmentStateBusiness : BaseEntityManagementBusiness<BranchOrderShipmentState, pgcEntities>
    {
        public BranchOrderShipmentStateBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchOrderShipmentStatePattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.BranchOrderShipmentStates, Pattern)
                .OrderBy(f => f.Title);
            
            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(BranchOrderShipmentStatePattern Pattern)
        {
            return Search_Where(Context.BranchOrderShipmentStates, Pattern).Count();
        }

        public IQueryable<BranchOrderShipmentState> Search_Where(IQueryable<BranchOrderShipmentState> list, BranchOrderShipmentStatePattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            if (Pattern.ID > 0)
                return list.Where(n => n.ID == Pattern.ID);
           
            return list;
        }

        public override OperationResult Delete(long ID)
        {

            BranchOrderShipmentState data = Retrieve(ID);

            if (data.BranchOrders.Count()>0)
            {
                OperationResult op = new OperationResult();
                op.Result = ActionResult.Error;
                op.AddMessage(UserMessageKey.PreventDeleteBranchOrderShipmentStateCauseHaveOrders);
                return op;
            }

            return base.Delete(ID);
        }
    }
}