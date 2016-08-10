using kFrameWork.Business;
using kFrameWork.Model;
using kFrameWork.Util;
using pgc.Model;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace pgc.Business.Payment.OnlinePay
{
    public class PaymentBusiness
    {
        protected pgcEntities Context = new pgcEntities();

        /// <summary>
        /// Step 1 , business for generatin Reservation Number and Posting to bank web site
        /// </summary>
        /// <param name="GameOrder"></param>
        /// <returns></returns>
        public OperationResult CreatePayment(long orderID,OnlineGetway getway)
        {
            OperationResult res = new OperationResult();
            try
            {
                pgc.Model.Payment p = new pgc.Model.Payment();
                p.Amount = 0;
                p.Date = DateTime.Now;
                p.IPAddress = HttpContext.Current.Request.UserHostAddress;
                p.Order_ID = orderID;
                p.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
                p.RefNum = "";
                p.State = -1; //NoReturnfrombank
                p.BankGeway_Enum = (int)getway;

                Context.Payments.AddObject(p);
                Context.SaveChanges();
                res.Result = ActionResult.Done;
                res.Data["ResNum"] = p.ID;
                return res;
            }
            catch
            {
                res.AddMessage(UserMessageKey.Failed);
                res.Result = ActionResult.Failed;
                return res;
            }
        }


        public void ChangeOrderState(long pId, int state)
        {
            try
            {
                pgc.Model.Payment p = RetrievePayment(pId);
                if (p != null)
                {
                    p.State = state;
                    Context.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }

        public pgc.Model.Payment RetrievePayment(long pId)
        {
            return Context.Payments.SingleOrDefault(p => p.ID == pId);
        }

    

        
    }
}
