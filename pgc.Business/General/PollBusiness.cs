using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using kFrameWork.Model;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Util;

namespace pgc.Business.General
{
    public class PollBusiness
    {
        pgcEntities db = new pgcEntities();

        public IQueryable Poll_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Poll_Where(db.Polls)
                .OrderByDescending(f => f.PollPersianDate)
                .Select(f => new
                {
                    f.ID,
                    f.Title
                });

            return Result.Skip(startRowIndex).Take(maximumRows);

        }
        public int Poll_Count()
        {
            return Poll_Where(db.Polls).Count();
        }
        public IQueryable<Poll> Poll_Where(IQueryable<Poll> list)
        {
            return list = list.Where(f => f.IsActive == true);
        }


        public Poll RetrivePoll(long id)
        {
            return db.Polls.Where(f => f.ID == id).SingleOrDefault();
        }



        public OperationResult RegisterPoll(PollResult poll, string ip)
        {
            OperationResult res = new OperationResult();
            try
            {
                if(db.PollResults.Where(p => p.Poll_ID == poll.Poll_ID && p.IPAddress == ip).Count()>0)
                {
                    res.Result=ActionResult.Failed;
                    res.AddMessage(UserMessageKey.DuplicateIPAddress);
                    return res;

                }


                poll.IPAddress=ip;
                poll.ResultPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
                poll.ResultDateTime=DateTime.Now;

                db.PollResults.AddObject(poll);
                db.SaveChanges();

                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.Succeed);
                res.AddMessage(UserMessageKey.ThankUserForComment);



                //Poll_Register
                #region Poll_Register

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.EventVariables.Add("%description%", poll.PollChoise.Description);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%title%", poll.Poll.Title);
                eArg.EventVariables.Add("%resulttitle%", poll.PollChoise.Title);

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Poll_Register, eArg);

                #endregion


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
