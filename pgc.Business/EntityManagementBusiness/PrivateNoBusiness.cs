using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using System.Collections.Generic;

namespace pgc.Business
{
    public class PrivateNoBusiness : BaseEntityManagementBusiness<PrivateNo, pgcEntities>
    {
        public PrivateNoBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, PrivateNoPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;
            
            var Result = Search_Where(Context.PrivateNoes, Pattern)
                .OrderByDescending(f => f.ID);

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(PrivateNoPattern Pattern)
        {
            return Search_Where(Context.PrivateNoes, Pattern).Count();
        }

        public IQueryable<PrivateNo> Search_Where(IQueryable<PrivateNo> list, PrivateNoPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            if (Pattern.ID > 0)
                return list.Where(f => f.ID == Pattern.ID);

            if (BasePattern.IsEnumAssigned(Pattern.Status))
                list = list.Where(f => f.Status == (int)Pattern.Status);

            ////Search By Pattern
            if (!string.IsNullOrEmpty(Pattern.Number))
                list = list.Where(f => f.Number.Contains(Pattern.Number));


            return list;
        }

        public PrivateNo GetTopPrivateNo()
        {
            return Context.PrivateNoes.First();
        }

        public override OperationResult Delete(long ID)
        {
            OperationResult op = new OperationResult();

            if (Context.ReceivedMessages != null && Context.ReceivedMessages.Any(f => f.PrivateNo_ID == ID))
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(Model.Enums.UserMessageKey.PrivateNoHasRecievSMSSoDeactive);
                return op;
            }
            else if (Context.SentSMSList != null && Context.SentSMSList.Any(f => f.PrivateNo_ID == ID))
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(Model.Enums.UserMessageKey.PrivateNoHasSentSMSSoDeactive);
                return op;
            }

            //if (Context.ScheduleEvents.Any(f => f.PrivateNo_ID == ID))
            //{
            //    op.Result = ActionResult.Failed;
            //    op.AddMessage(Model.Enums.UserMessageKey.SomeScheduleEventsBindThis);
            //}

            return base.Delete(ID);
        }

        public override OperationResult Update(PrivateNo Data)
        {
            pgcEntities newContext = new pgcEntities();
            PrivateNo OldOne = newContext.PrivateNoes.SingleOrDefault(f => f.ID == Data.ID);
            
            OperationResult op = base.Update(Data);
            
            //if (OldOne.Status == (int)PrivateNoStatus.Enabled && Data.Status == (int)PrivateNoStatus.Disabled)
            //{
            //    if (op.Result == ActionResult.Done)
            //    {
            //        List<long> SystemEventActionUseThisPrivateNo = Context.SystemEventActions.Where(f => f.PrivateNo_ID == Data.ID).Select(g => g.ID).ToList();

            //        foreach (var item in SystemEventActionUseThisPrivateNo)
            //            Context.SystemEventActions.Single(f => f.ID == item).PrivateNo_ID = null;

            //        Context.SaveChanges();
                
            //        op.AddMessage(UserMessageKey.SomeSystemEventActionDisabledForThisPrivateNo);
            //    }
            //    return op;
            //}
            return op;
        }
    }
}