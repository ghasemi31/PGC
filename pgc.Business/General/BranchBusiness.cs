using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using kFrameWork.Model;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Util;
using kFrameWork.UI;

namespace pgc.Business.General
{
    public class BranchBusiness
    {
        pgcEntities db = new pgcEntities();

        public List<BranchPic> GetThumbBranchPic(object branch_id)
        {
            return db.BranchPics.Where(b => b.Branch_ID == (long)branch_id).OrderBy(b => b.ID).ToList();

        }

        public List<Branch> GetTehranBranch()
        {
            return db.Branches.Where(b => b.BranchType == 1 && b.IsActive==true).ToList();
        }

        public List<Branch> GetIranBranch()
        {
            return db.Branches.Where(b => b.BranchType == 2 && b.IsActive == true).ToList();
        }

        public List<Branch> GetWorldBranch()
        {
            return db.Branches.Where(b => b.BranchType == 3 && b.IsActive == true).ToList();
        }


        public IQueryable Branch_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Branch_Where(db.Branches)
                .OrderBy(f => f.DispOrder)
                .Select(f => new
                {
                    f.ID,
                    f.UrlKey,
                    f.Title,
                    f.Description,
                    f.ThumbListPath


                });

            return Result.Skip(startRowIndex).Take(maximumRows);

        }
        public int Branch_Count()
        {
            return Branch_Where(db.Branches).Count();
        }
        public IQueryable<Branch> Branch_Where(IQueryable<Branch> list)
        {
            return list.Where(f => f.IsActive);
        }



        public Branch RetriveBranch(string url)
        {
            return db.Branches.Where(f => f.UrlKey == url).SingleOrDefault();
        }

        public Branch RetirveBranchID(long BranchID)
        {
            return db.Branches.Where(b => b.ID == BranchID).SingleOrDefault();

        }

        public OperationResult UpdateChanges(Branch branch)
        {
            OperationResult res = new OperationResult();

            try
            {

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

        public OperationResult UpdateAllowOnlineOrderChanges(Branch branch)
        {
            OperationResult res = new OperationResult();

            try
            {
                if (new BranchBusiness().RetirveBranchID(branch.ID).AllowOnlineOrder == false &&
                   branch.AllowOnlineOrder == true)
                {

                    string optionKey = OptionKey.AllowOnlineOrdering.ToString();
                    if (db.Options.Single(f => f.Key == optionKey).Value == "0")
                        res.AddMessage(UserMessageKey.OnlineOrderSuspendByAdmin);
                }


                db.Branches.Single(f => f.ID == branch.ID).AllowOnlineOrder = branch.AllowOnlineOrder;
                db.Branches.Single(f => f.ID == branch.ID).AllowOnlineOrderTimeFrom = branch.AllowOnlineOrderTimeFrom;
                db.Branches.Single(f => f.ID == branch.ID).AllowOnlineOrderTimeTo = branch.AllowOnlineOrderTimeTo;

                db.SaveChanges();



                //ChangeOnlineOrderTimeByAgent
                #region Event Raising
                SystemEventArgs eArg = new SystemEventArgs();

                User user = new UserBusiness().RetriveUser(UserSession.UserID);

                eArg.Related_Branch = branch;
                eArg.Related_Doer = user;

                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%time%", DateTime.Now.TimeOfDay.ToString().Substring(0, 5));
                eArg.EventVariables.Add("%branchtitle%", branch.Title);
                eArg.EventVariables.Add("%allow%", branch.AllowOnlineOrder ? "پذیرای سفارش" : "عدم پذیرش سفارش");
                eArg.EventVariables.Add("%timeFrom%", branch.AllowOnlineOrderTimeFrom);
                eArg.EventVariables.Add("%timeTo%", branch.AllowOnlineOrderTimeTo);

                eArg.EventVariables.Add("%user%", user.FullName);
                eArg.EventVariables.Add("%username%", user.Username);
                eArg.EventVariables.Add("%email%", user.Email);

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchOnlineOrderChangeTime, eArg);

                #endregion

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

        public List<Branch> GetBranchList()
        {
            return db.Branches.Where(f => f.IsActive).OrderBy(c => c.DispOrder).ToList();
        }

        public List<Comment> GetProductComment(Branch b)
        {
            List<Comment> res = new List<Comment>();
            var comments=b.Comments.Where(c => c.IsDisplay == true).OrderBy(c => c.Date);
            if (comments != null && comments.Any())
                res = comments.ToList();
            return res;
        }
    }
}
