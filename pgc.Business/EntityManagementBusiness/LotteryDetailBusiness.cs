using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business
{
    public class LotteryDetailBusiness:BaseEntityManagementBusiness<LotteryDetail,pgcEntities>
    {
        public LotteryDetailBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, LotteryDetailPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = Search_Where(Context.LotteryDetails, Pattern)
                .OrderByDescending(f => f.ID)
                .Select(d => new
                {
                    d.ID,
                    Name=d.FName + " " + d.LName,
                    d.Code,
                    Lottery = d.Lottery.Title
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(LotteryDetailPattern Pattern)
        {
            return Search_Where(Context.LotteryDetails, Pattern).Count();
        }

        public IQueryable<LotteryDetail> Search_Where(IQueryable<LotteryDetail> list, LotteryDetailPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern

            if (!string.IsNullOrEmpty(Pattern.Name))
                list = list.Where(d => d.FName.Contains(Pattern.Name) ||
                                       d.LName.Contains(Pattern.Name) ||  
                                       d.Description.Contains(Pattern.Name) ||
                                       d.Email.Contains(Pattern.Name));

            if (Pattern.Lottery_ID > 0)
                list = list.Where(d => d.LotteryID== Pattern.Lottery_ID);

            if (Pattern.Code > 0)
                list = list.Where(d => d.Code == Pattern.Code);

            return list;
        }

        public override OperationResult Validate(LotteryDetail Data, SaveValidationMode Mode)
        {
            OperationResult res = new OperationResult();

            if (Mode == SaveValidationMode.Add || Mode == SaveValidationMode.Edit)
            {
                if (Context.LotteryDetails.Count(d => d.Code == Data.Code) > 0)
                {
                    res.AddMessage(Model.Enums.UserMessageKey.DuplicateLotteryCode);
                    return res;
                }
            }
            return base.Validate(Data, Mode);
        }
    }
}