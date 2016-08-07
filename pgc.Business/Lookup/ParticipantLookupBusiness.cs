using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;

namespace pgc.Business.Lookup
{
    public class ParticipantLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        public IQueryable GetLookupList(object Lottery_ID)
        {
            if (Lottery_ID == null)
                return null;

            var list = from P in Context.LotteryDetails.Where(d=>d.LotteryID == (long)Lottery_ID)
                       orderby P.ID descending
                       select new
                       {
                           P.ID,
                           Title=P.FName + " " + P.LName
                       };

            return list;
        }

        
    }
}
