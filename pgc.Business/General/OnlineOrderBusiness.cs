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
    public class OnlineOrderBusiness
    {
        pgcEntities db = new pgcEntities();

        public OnlineOrderBusiness()
        {
        }

        public OperationResult SaveChanges(Dictionary<long, bool> ProductList, List<Branch> BranchList, bool IsOnlineAllow, string UserMessage)
        {
            OperationResult op = new OperationResult();

            try
            {
                if (IsOnlineAllow)
                {
                    foreach (var item in ProductList)
                    {
                        db.Products.Single(f => f.ID == item.Key).AllowOnlineOrder = item.Value;
                    }
                    db.SaveChanges();

                    foreach (var item in BranchList)
                    {
                        db.Branches.Single(f => f.ID == item.ID).AllowOnlineOrder = item.AllowOnlineOrder;
                        db.Branches.Single(f => f.ID == item.ID).AllowOnlineOrderTimeFrom = item.AllowOnlineOrderTimeFrom;
                        db.Branches.Single(f => f.ID == item.ID).AllowOnlineOrderTimeTo = item.AllowOnlineOrderTimeTo;
                    }

                    db.SaveChanges();
                }

                string optionKey=OptionKey.AllowOnlineOrdering.ToString();
                db.Options.Single(f => f.Key == optionKey).Value = (IsOnlineAllow)?"1":"0";
                db.SaveChanges();

                optionKey = OptionKey.MessageForOnlineOrderIsSuspended.ToString();
                db.Options.Single(f => f.Key == optionKey).Value = UserMessage;
                db.SaveChanges();
            }
            catch (Exception)
            {
                op.Result = ActionResult.Failed;
                op.AddMessage(UserMessageKey.Failed);
                return op;
            }
            op.Result = ActionResult.Done;
            op.AddMessage(UserMessageKey.Succeed);
            return op;
        }

        public IQueryable<Branch> GetAllBranches()
        {
            return db.Branches.OrderBy(f => f.DispOrder);
        }
    }
}
