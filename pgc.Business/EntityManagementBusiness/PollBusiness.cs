using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class PollBusiness:BaseEntityManagementBusiness<Poll,pgcEntities>
    {
        public PollBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, PollPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.Polls, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Description

                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(PollPattern Pattern)
        {
            return Search_Where(Context.Polls, Pattern).Count();
        }

        public IQueryable<Poll> Search_Where(IQueryable<Poll> list, PollPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern
           

            if (!string.IsNullOrEmpty(Pattern.Title))
                list = list.Where(f => f.Title.Contains(Pattern.Title) ||
                    f.Description.Contains(Pattern.Title));


            return list;
        }

        public override OperationResult Delete(long ID)
        {
            Poll data = Retrieve(ID);
            if ((data.PollChoises != null && data.PollChoises.Count > 0)||
                (data.PollResults != null && data.PollResults.Count > 0))
            {
                OperationResult op = new OperationResult();
                op.Result = ActionResult.Failed;
                op.AddMessage(Model.Enums.UserMessageKey.FirstDeletePollChoisesANDResults);
                return op;
            }
            return base.Delete(ID);
        }



        //public override OperationResult Validate(Poll Data, SaveValidationMode Mode)
        //{
        //    OperationResult res = new OperationResult();

        //    if (Mode == SaveValidationMode.Add)
        //    {
        //        if (Context.Polls.Count(u => u.Pollname == Data.Pollname) > 0)
        //        {
        //            res.AddMessage(Model.Enums.PollMessageKey.DuplicatePollname);
        //            return res;
        //        }
        //    }
        //    else if (Mode == SaveValidationMode.Edit)
        //    {
        //        if (Context.Polls.Count(u => u.Pollname == Data.Pollname && u.ID != Data.ID) > 0)
        //        {
        //            res.AddMessage(Model.Enums.PollMessageKey.DuplicatePollname);
        //            return res;
        //        }
        //    }

        //    return base.Validate(Data, Mode);
        //}
    }
}