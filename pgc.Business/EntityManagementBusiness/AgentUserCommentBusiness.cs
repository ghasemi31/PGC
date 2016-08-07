using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using kFrameWork.UI;

namespace pgc.Business
{
    public class AgentUserCommentBusiness:BaseEntityManagementBusiness<UserComment,pgcEntities>
    {
        public AgentUserCommentBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, AgentUserCommentPattern Pattern)
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

        public int Search_Count(AgentUserCommentPattern Pattern)
        {
            return Search_Where(Context.UserComments, Pattern).Count();
        }

        public IQueryable<UserComment> Search_Where(IQueryable<UserComment> list, AgentUserCommentPattern Pattern)
        {
            list = list.Where(f => f.Branch_ID == UserSession.User.Branch_ID);

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