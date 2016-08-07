using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Enums;

namespace pgc.Business.Lookup
{
    public class LotteryLookupBusiness : BaseLookupBusiness
    {
        public pgcEntities Context = new pgcEntities();

        //normal lookup
        public IQueryable GetLookupList()
        {
            var list = from L in Context.Lotteries
                       orderby L.Title 
                       select new
                       {
                           L.ID,
                           L.Title
                       };

            return list;
        }


        public IQueryable GetCompateLotteryList()
        {
            var list = from L in Context.Lotteries
                       where L.Status == (int)LotteryStatus.complete
                       orderby L.Title
                       select new
                       {
                           L.ID,
                           L.Title
                       };

            return list;
        }

        
    }
}
