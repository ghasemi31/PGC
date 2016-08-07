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
    public class CmsBusiness
    {
        pgcEntities db = new pgcEntities();

        public IQueryable Faq_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Faq_Where(db.Faqs)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Summery,

                });

             return Result.Skip(startRowIndex).Take(maximumRows);

        }
        public int Faq_Count()
        {
            return Faq_Where(db.Faqs).Count();
        }
        public IQueryable<Faq> Faq_Where(IQueryable<Faq> list)
        {
            return list;
        }


        public Faq RetriveFaq(long ID)
        {
            return db.Faqs.Where(f => f.ID == ID).SingleOrDefault();
        }


        public OperationResult AdduserComment(UserComment comment)
        {
            OperationResult res = new OperationResult();
            try
            {


                db.UserComments.AddObject(comment);
                db.SaveChanges();
                res.AddMessage(UserMessageKey.Succeed);
                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.ThankUserForComment);

                //UserComment_New
                #region Event Raising

                SystemEventArgs e = new SystemEventArgs();
                e.Related_Guest_Email = comment.Email;
                if (comment.Branch_ID.HasValue)
                    e.Related_Branch = new BranchBusiness().RetirveBranchID(comment.Branch_ID.Value);

                e.EventVariables.Add("%user%", comment.Name);
                e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                e.EventVariables.Add("%email%", comment.Email);
                e.EventVariables.Add("%phone%", comment.Phone);
                e.EventVariables.Add("%body%", comment.Body);
                e.EventVariables.Add("%type%", EnumUtil.GetEnumElementPersianTitle((UserCommentType)comment.Type));
 
                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.UserComment_New, e);

                #endregion


                return res;
            }
            catch
            {
                //log exc
                res.AddMessage(UserMessageKey.Failed);
                res.Result = ActionResult.Error;
                return res;
            }
        }
    }
}
