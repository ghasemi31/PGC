using kFrameWork.Model;
using kFrameWork.Util;
using pgc.Model;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.General
{
    public class CommentBusiness
    {
        pgcEntities db = new pgcEntities();

        public OperationResult AddComment(Comment comment)
        {
            OperationResult res = new OperationResult();
            try
            {


                db.Comments.AddObject(comment);
                db.SaveChanges();
                res.AddMessage(UserMessageKey.Succeed);
                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.ThankUserForComment);

                //Comment
                #region Event Raising

                SystemEventArgs e = new SystemEventArgs();
                e.Related_Guest_Email = comment.SenderEmail;
                if (comment.Branch_ID.HasValue)
                {
                    e.Related_Branch = new BranchBusiness().RetirveBranchID(comment.Branch_ID.Value);
                    e.EventVariables.Add("%branch%", comment.Branch.Title);
                }
                else
                {
                    e.EventVariables.Add("%product%", comment.Product.Title);
                }                  
                e.EventVariables.Add("%fullname%", comment.SenderName);
                e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                e.EventVariables.Add("%email%", comment.SenderEmail);
                e.EventVariables.Add("%body%", comment.Body);
                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Comment, e);

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
