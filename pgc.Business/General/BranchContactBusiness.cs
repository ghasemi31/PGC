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
    public class BranchContactBusiness
    {
        pgcEntities db = new pgcEntities();

        public OperationResult AddBranchContact(BranchContact contact)
        {
            OperationResult res = new OperationResult();
            try
            {
                contact.Date = DateTime.Now;
                contact.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);

                db.BranchContacts.AddObject(contact);
                db.SaveChanges();
                res.AddMessage(UserMessageKey.Succeed);
                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.ThankUserForComment);
                //contact
                #region Event Raising

                SystemEventArgs e = new SystemEventArgs();
                e.Related_Guest_Email = contact.Email;
                e.EventVariables.Add("%fullname%", contact.FullName);
                e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                e.EventVariables.Add("%email%", contact.Email);
                e.EventVariables.Add("%body%", contact.Body);
                e.EventVariables.Add("%branch%", contact.Branch.Title);

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
