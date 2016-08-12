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
    public class FeedbackBusiness
    {
        pgcEntities db = new pgcEntities();

        public OperationResult AddFeedback(Feedback feed)
        {
            OperationResult res = new OperationResult();
            try
            {
                feed.Date = DateTime.Now;
                feed.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
                db.Feedbacks.AddObject(feed);
                db.SaveChanges();
                res.AddMessage(UserMessageKey.Succeed);
                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.ThankUserForComment);

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
