using System;
using System.Collections.Generic;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class PollResultBusiness:BaseEntityManagementBusiness<PollResult,pgcEntities>
    {
        public PollResultBusiness()
        {
            Context = new pgcEntities();
        }

        public List<PollChoise> GetPollChoise(object Poll_id)
        {
            return Context.PollChoises.Where(i => i.Poll_ID == (long)Poll_id).ToList();

        }

        //public IQueryable<PollChoise> GetPollChoise2(object Poll_id)
        //{
        //    return Context.PollChoises.Where(i => i.Poll_ID == (long)Poll_id)
        //                              .OrderBy(i => i.ID)
        //                              .Select(i=> new
        //                              {
        //                                i.ID,
        //                                i.Description
        //                              });
            

        //}

        public int GetResult(object choise_id)
        {
            return Context.PollResults.Where(i => i.PollChoise_ID == (long)choise_id).Count();

        }




 
    }
}