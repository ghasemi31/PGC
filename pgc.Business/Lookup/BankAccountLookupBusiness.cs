using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Enums;

namespace pgc.Business.Lookup
{
    public class BankAccountLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        //normal lookup
        public IQueryable GetLookupList()
        {
            var list = from P in Context.BankAccounts
                       orderby P.ID descending
                       where P.Status == (int)OfflineBankAccountStatus.Enabled
                       select new
                       {
                           P.ID,
                           Title = P.Title
                       };

            return list;
        }

        //add default user id
        public IQueryable GetLookupList(long UserID)
        {
            var list = from P in Context.BankAccounts
                       orderby P.ID descending                       
                       select new
                       {
                           P.ID,
                           Title = P.Title
                       };

            return list;
        }        
    }
}
