using kFrameWork.Business;
using kFrameWork.Model;
using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Model;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Business
{
    public class AgentBranchContactBusiness : BaseEntityManagementBusiness<BranchContact, pgcEntities>
    {
        public AgentBranchContactBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, AgentBranchContactPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.BranchContacts, Pattern)
                .OrderByDescending(f => f.Date)
                .Select(f => new
                {
                    f.ID,
                    f.FullName,
                    f.Email,
                    f.Body,
                    f.Date,
                    f.Branch_ID,
                    f.PersianDate,
                    f.IsRead

                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(AgentBranchContactPattern Pattern)
        {
            return Search_Where(Context.BranchContacts, Pattern).Count();
        }

        public IQueryable<BranchContact> Search_Where(IQueryable<BranchContact> list, AgentBranchContactPattern Pattern)
        {
            list = list.Where(f => f.Branch_ID == UserSession.User.Branch_ID);

            //DefaultPattern
            if (Pattern == null)
                return list;

            if (!string.IsNullOrEmpty(Pattern.Name))
                list = list.Where(f => f.FullName.Contains(Pattern.Name) ||
                    f.Email.Contains(Pattern.Name));


            switch (Pattern.PersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.PersianDate.HasFromDate && Pattern.PersianDate.HasToDate)
                        list = list.Where(f => f.PersianDate.CompareTo(Pattern.PersianDate.FromDate) >= 0
                            && f.PersianDate.CompareTo(Pattern.PersianDate.ToDate) <= 0);
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => f.PersianDate.CompareTo(Pattern.PersianDate.Date) >= 0);
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => f.PersianDate.CompareTo(Pattern.PersianDate.Date) <= 0);
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.PersianDate.HasDate)
                        list = list.Where(f => f.PersianDate.CompareTo(Pattern.PersianDate.Date) == 0);
                    break;
            }


            return list;
        }


        public override OperationResult Update(BranchContact Data)
        {
            //BranchContact old = new BranchContactBusiness().Retrieve(Data.ID);

            OperationResult op = base.Update(Data);

            if (op.Result == ActionResult.Done)
            {
                //if (old.Branch_ID != Data.Branch_ID)
                //{
                    //Branch Changed
                    #region Event Rising

                    SystemEventArgs eArg = new SystemEventArgs();
                    //User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                    //eArg.Related_Doer = doer;
                    eArg.Related_Guest_Email = Data.Email;

                    eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID);

                    eArg.EventVariables.Add("%user%", Data.FullName);
                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    eArg.EventVariables.Add("%body%", Data.Body);

                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchContact_ReadMessage, eArg);

                    #endregion
            //    }
            }
            return op;

        }



    }
}
