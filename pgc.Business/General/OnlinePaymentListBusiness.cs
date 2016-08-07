using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business.General
{
    public class OnlinePaymentListBusiness
    {
        pgcEntities db = new pgcEntities();

        public string RetriveBranchName(string onlineResNum)
        {
            return db.BranchPayments.FirstOrDefault(b => b.OnlineResNum == onlineResNum).Branch.Title;
        }

        public IQueryable<OnlinePaymentList> RetriveOnlinePaymentList()
        {
            return db.OnlinePaymentLists.OrderByDescending(d=>d.Date);
        }
    }
}
