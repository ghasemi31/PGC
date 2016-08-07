using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using kFrameWork.Model;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Util;
using pgc.Business.Core;

namespace pgc.Business.General
{
    
    public class BranchRequestBusiness
    {
        pgcEntities db = new pgcEntities();

        public BranchRequestBusiness()
        {

        }

        public OperationResult Request(BranchRequest request)
        {
            OperationResult res = new OperationResult();
            try
            {
                //if (db.Users.Where(u => u.Username == user.Username).Count() > 0)
                //{
                //    res.Result = ActionResult.Failed;
                //    res.AddMessage(UserMessageKey.DuplicateUsername);
                //    return res;
                //}

               
                request.Status = Convert.ToInt32(UserCommentStatus.UnRead);
                request.BRPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
                request.BRDate = DateTime.Now;
                try
                {
                    db.BranchRequests.AddObject(request);
                    db.SaveChanges();


                    res.Result = ActionResult.Done;
                    res.AddMessage(UserMessageKey.Succeed);
                    res.AddMessage(UserMessageKey.ThankBranchRequest);



                    //BranchRequest_New
                    #region Event Raising

                    SystemEventArgs e = new SystemEventArgs();
                    e.Related_Guest_Email = request.Email;
                    e.Related_Guest_Phone = request.Mobile;

                    e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    e.EventVariables.Add("%fullname%", request.FullName);
                    e.EventVariables.Add("%applicator%", request.ApplicatorName);

                    e.EventVariables.Add("%mobile%", request.Mobile);
                    e.EventVariables.Add("%email%", request.Email);
                    e.EventVariables.Add("%phone%", request.Tel);
                    e.EventVariables.Add("%address%", request.Address);

                    e.EventVariables.Add("%location%", request.BranchLocation);
                    e.EventVariables.Add("%locationtype%", EnumUtil.GetEnumElementPersianTitle((LocationType)request.LocationType));
                    e.EventVariables.Add("%description%", request.Description);

                    e.EventVariables.Add("%hasexperience%", (request.HaveBackgroung)?"دارم":"ندارم");

                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchRequest_New, e);

                    #endregion

                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleManualException(ex, "pgc.Business.General,BranchRequestBusiness");
                }
                
                return res;

            }

            catch
            {
                res.Result = ActionResult.Failed;
                res.AddMessage(UserMessageKey.Failed);
                return res;
            }
        }


    }
}
