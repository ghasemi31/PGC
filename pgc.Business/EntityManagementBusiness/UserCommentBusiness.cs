using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using kFrameWork.UI;
using pgc.Model.Enums;

namespace pgc.Business
{
    public class UserCommentBusiness:BaseEntityManagementBusiness<UserComment,pgcEntities>
    {
        public UserCommentBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, UserCommentPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.UserComments, Pattern)
                .OrderByDescending(f => f.UCDate)
                .Select(f => new
                {
                    f.ID,
                    f.Name,
                    f.Topic,
                    f.Status,
                    f.UCPersianDate, 
             
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(UserCommentPattern Pattern)
        {
            return Search_Where(Context.UserComments, Pattern).Count();
        }

        public IQueryable<UserComment> Search_Where(IQueryable<UserComment> list, UserCommentPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            if (BasePattern.IsEnumAssigned(Pattern.Status))
            {
                list = list.Where(f => f.Status == (int)Pattern.Status);
            }

            if (BasePattern.IsEnumAssigned(Pattern.Type))
            {
                list = list.Where(f => f.Type == (int)Pattern.Type);
            }

            if (Pattern.Branch_ID > 0)
                list = list.Where(f => f.Branch_ID == Pattern.Branch_ID);

            if (!string.IsNullOrEmpty(Pattern.BranchTitle))
                list = list.Where(f => f.BranchTitle.Contains(Pattern.BranchTitle));


            if (!string.IsNullOrEmpty(Pattern.Name))
                list = list.Where(f => f.Name.Contains(Pattern.Name) ||
                    f.Email.Contains(Pattern.Name) || f.Topic.Contains(Pattern.Name));


            switch (Pattern.UCPersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.UCPersianDate.HasFromDate && Pattern.UCPersianDate.HasToDate)
                        list = list.Where(f => f.UCPersianDate.CompareTo(Pattern.UCPersianDate.FromDate) >= 0
                            && f.UCPersianDate.CompareTo(Pattern.UCPersianDate.ToDate) <= 0);
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.UCPersianDate.HasDate)
                        list = list.Where(f => f.UCPersianDate.CompareTo(Pattern.UCPersianDate.Date) >= 0);
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.UCPersianDate.HasDate)
                        list = list.Where(f => f.UCPersianDate.CompareTo(Pattern.UCPersianDate.Date) <= 0);
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.UCPersianDate.HasDate)
                        list = list.Where(f => f.UCPersianDate.CompareTo(Pattern.UCPersianDate.Date) == 0);
                    break;
            }


            return list;
        }

        public override OperationResult Update(UserComment Data)
        {
            UserComment old = new UserCommentBusiness().Retrieve(Data.ID);

            OperationResult op = base.Update(Data);

            if (op.Result == ActionResult.Done)
            {
                if (old.Branch_ID != Data.Branch_ID)
                {
                    //Branch Changed
                    #region Event Rising

                    SystemEventArgs eArg = new SystemEventArgs();
                    User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                    eArg.Related_Doer = doer;
                    eArg.Related_Guest_Email = old.Email;
                    eArg.Related_Guest_Phone = old.Phone;
                    if (old.Branch_ID.HasValue && old.Branch_ID.Value > 0)
                        eArg.Related_Branch = new BranchBusiness().Retrieve(old.Branch_ID.Value);

                    eArg.EventVariables.Add("%user%", old.Name);
                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    eArg.EventVariables.Add("%phone%", old.Phone);
                    eArg.EventVariables.Add("%topic%", old.Topic);
                    eArg.EventVariables.Add("%body%", old.Body);

                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.UserComment_Change_Branch, eArg);

                    #endregion
                }
            }
            return op;
        }

        //public OperationResult UpdateIsDone(long UserComment_ID)
        //{
        //    UserComment Data = this.Retrieve(UserComment_ID);
        //    OperationResult Res = new OperationResult();
        //    if (Data != null)
        //    {
        //        Data.IsDone = !Data.IsDone;
        //        Res=this.Update(Data);
        //    }

        //    return Res;
        //}
    }
}