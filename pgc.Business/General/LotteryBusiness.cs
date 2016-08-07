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
    public class LotteryBusiness
    {
        pgcEntities db = new pgcEntities();

        public IQueryable Lottery_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;
            
            var Result = Lottery_Where(db.Lotteries)
                .OrderByDescending(f => f.RegDate)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Status
                });

            return Result.Skip(startRowIndex).Take(maximumRows);

        }
        public int Lottery_Count()
        {
            return Lottery_Where(db.Lotteries).Count();
        }
        public IQueryable<Lottery> Lottery_Where(IQueryable<Lottery> list)
        {
            return list= list.Where(f => f.Status == (int)LotteryStatus.flow);
        }




        public IQueryable LotteryCompate_List(int startRowIndex, int maximumRows)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            var Result = LotteryCompate_Where(db.Lotteries)
                .OrderByDescending(f => f.RegDate)
                .Select(f => new
                {
                    f.ID,
                    f.Title,
                    f.Status
                });

            return Result.Skip(startRowIndex).Take(maximumRows);

        }
        public int LotteryCompate_Count()
        {
            return LotteryCompate_Where(db.Lotteries).Count();
        }
        public IQueryable<Lottery> LotteryCompate_Where(IQueryable<Lottery> list)
        {
            return list = list.Where(f => f.Status == (int)LotteryStatus.complete);
        }

        public Lottery RetriveLottery(long id)
        {
            return db.Lotteries.Where(f => f.ID == id).SingleOrDefault();
        }

        public List<LotteryWiner> RetriveLotteryWiners(long id)
        {
            return db.LotteryWiners.Where(f => f.LotteryID == id).OrderBy(f=>f.Rank).ToList(); ;
        }

        //public IQueryable LotteryWiner_List(int startRowIndex, int maximumRows)
        //{
        //    if (startRowIndex == 0 && maximumRows == 0)
        //        return null;

        //    var Result = LotteryWiner_Where(db.LotteryWiners)
        //        .OrderByDescending(f => f.RegDate)
        //        .Select(f => new
        //        {
        //            f.ID,
        //            f.Title,
        //            f.Status
        //        });

        //    return Result.Skip(startRowIndex).Take(maximumRows);

        //}
        //public int Lotterywiner_Count()
        //{
        //    return LotteryWiner_Where(db.LotteryWiners).Count();
        //}
        //public IQueryable<LotteryWiner> LotteryWiner_Where(IQueryable<LotteryWiner> list)
        //{
        //    return list = list.Where(f => f);
        //}


        public OperationResult RegisterLotteryDetail(LotteryDetail detail)
        {
            OperationResult res = new OperationResult();
            try
            {
                if (db.LotteryDetails.Count(d => d.Code == detail.Code) > 0)
                {
                    res.Result = ActionResult.Failed;
                    res.AddMessage(Model.Enums.UserMessageKey.DuplicateLotteryCode);
                    return res;
                }


                db.LotteryDetails.AddObject(detail);
                db.SaveChanges();

                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.Succeed);
                res.AddMessage(UserMessageKey.ThankForParticipateLottery);


                //Lottery_Register
                #region Lottery_Register

                SystemEventArgs eArg = new SystemEventArgs();

                eArg.Related_Guest_Email = detail.Email;
                
                eArg.EventVariables.Add("%user%", detail.FName + " " + detail.LName);
                eArg.EventVariables.Add("%description%", detail.Description);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%title%", detail.Lottery.Title);
                eArg.EventVariables.Add("%code%", detail.Code.ToString());
                eArg.EventVariables.Add("%email%", detail.Email);

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Lottery_Register, eArg);

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
