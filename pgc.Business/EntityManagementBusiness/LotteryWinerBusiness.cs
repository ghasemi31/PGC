using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class LotteryWinerBusiness:BaseEntityManagementBusiness<LotteryWiner,pgcEntities>
    {
        public LotteryWinerBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, LotteryWinerPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.LotteryWiners, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(w => new
                {
                    w.ID,
                    Name=w.FName + " " + w.LName,
                    w.Rank,
                    Lottery = w.Lottery.Title
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(LotteryWinerPattern Pattern)
        {
            return Search_Where(Context.LotteryWiners, Pattern).Count();
        }

        public IQueryable<LotteryWiner> Search_Where(IQueryable<LotteryWiner> list, LotteryWinerPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern

            if (!string.IsNullOrEmpty(Pattern.Name))
                list = list.Where(w => w.FName.Contains(Pattern.Name) ||
                                       w.LName.Contains(Pattern.Name) ||
                                       w.Description.Contains(Pattern.Name));

            if (Pattern.Lottery_ID > 0)
                list = list.Where(w => w.LotteryID== Pattern.Lottery_ID);

            if (Pattern.Rank > 0)
                list = list.Where(w => w.Rank == Pattern.Rank);

            return list;
        }


        public LotteryDetail RetriveLotteryDetail(long ID)
        {
            return Context.LotteryDetails.SingleOrDefault(l => l.ID == ID);
        }

    }
}