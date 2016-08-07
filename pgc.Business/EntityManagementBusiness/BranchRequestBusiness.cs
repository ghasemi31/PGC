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
    public class BranchRequestBusiness:BaseEntityManagementBusiness<BranchRequest,pgcEntities>
    {
        public BranchRequestBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, BranchRequestPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.BranchRequests, Pattern)
                .OrderByDescending(f => f.BRDate)
                .Select(f => new
                {
                    f.ID,
                    Name = f.Fname + " " + f.Lname,
                    f.Status,
                    f.BRPersianDate,
                    f.FullName
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(BranchRequestPattern Pattern)
        {
            return Search_Where(Context.BranchRequests, Pattern).Count();
        }

        public IQueryable<BranchRequest> Search_Where(IQueryable<BranchRequest> list, BranchRequestPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern
            
            if (BasePattern.IsEnumAssigned(Pattern.Status))
                list = list.Where(f => f.Status == (int)Pattern.Status);

            if (!string.IsNullOrEmpty(Pattern.Contact))
                list = list.Where(f => f.Address.Contains(Pattern.Contact) ||
                    f.Email.Contains(Pattern.Contact) || f.Mobile.Contains(Pattern.Contact)
                    || f.Tel.Contains(Pattern.Contact));


            if (!string.IsNullOrEmpty(Pattern.Name))
                list = list.Where(f => (f.Fname + " " + f.Lname).Contains(Pattern.Name)
                    || f.ApplicatorName.Contains(Pattern.Name));


            if (BasePattern.IsEnumAssigned(Pattern.Status))
                list = list.Where(f => f.Status == (int)Pattern.Status);

            switch (Pattern.BRPersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.BRPersianDate.HasFromDate && Pattern.BRPersianDate.HasToDate)
                        list = list.Where(f => f.BRPersianDate.CompareTo(Pattern.BRPersianDate.FromDate) >= 0
                            && f.BRPersianDate.CompareTo(Pattern.BRPersianDate.ToDate) <= 0);
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.BRPersianDate.HasDate)
                        list = list.Where(f => f.BRPersianDate.CompareTo(Pattern.BRPersianDate.Date) >= 0);
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.BRPersianDate.HasDate)
                        list = list.Where(f => f.BRPersianDate.CompareTo(Pattern.BRPersianDate.Date) <= 0);
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.BRPersianDate.HasDate)
                        list = list.Where(f => f.BRPersianDate.CompareTo(Pattern.BRPersianDate.Date) == 0);
                    break;
            }

            return list;
        }

        public override OperationResult Delete(long ID)
        {
            BranchRequest request = new BranchRequestBusiness().Retrieve(ID);
            OperationResult op=base.Delete(ID);

            if (op.Result==ActionResult.Done|| op.Messages.Contains(UserMessageKey.Succeed))
            {
            //BranchRequest_Remove_Admin
            #region BranchRequest_Remove_Admin

            SystemEventArgs e = new SystemEventArgs();
            User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

            e.Related_Doer = doer;
            e.Related_Guest_Email = request.Email;
            e.Related_Guest_Phone = request.Mobile;

            e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));

            e.EventVariables.Add("%lname%", request.Lname);
            e.EventVariables.Add("%fname%", request.Fname);
            e.EventVariables.Add("%applicator%", request.ApplicatorName);

            e.EventVariables.Add("%mobile%", request.Mobile);
            e.EventVariables.Add("%email%", request.Email);
            e.EventVariables.Add("%phone%", request.Tel);
            e.EventVariables.Add("%address%", request.Address);

            e.EventVariables.Add("%location%", request.BranchLocation);
            e.EventVariables.Add("%locationtype%", EnumUtil.GetEnumElementPersianTitle((LocationType)request.LocationType));
            e.EventVariables.Add("%description%", request.Description);

            e.EventVariables.Add("%hasexperience%", (request.HaveBackgroung) ? "دارم" : "ندارم");


            EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchRequest_Remove_Admin, e);

            #endregion
            }

            return op;
        }


        public override OperationResult Update(BranchRequest Data)
        {
            BranchRequest old = new BranchRequestBusiness().Retrieve(Data.ID);
            OperationResult op=base.Update(Data);

            if (op.Result == ActionResult.Done || op.Messages.Contains(UserMessageKey.Succeed))
            {
                if (old.Status != Data.Status)
                {
                    //BranchRequest_Change_Admin
                    #region BranchRequest_Change_Admin

                    SystemEventArgs e = new SystemEventArgs();
                    User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);

                    e.Related_Doer = doer;
                    e.Related_Guest_Email = old.Email;
                    e.Related_Guest_Phone = old.Mobile;

                    e.EventVariables.Add("%status%", EnumUtil.GetEnumElementPersianTitle((UserCommentStatus)old.Status));

                    e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));

                    e.EventVariables.Add("%lname%", old.Lname);
                    e.EventVariables.Add("%fname%", old.Fname);
                    e.EventVariables.Add("%applicator%", old.ApplicatorName);

                    e.EventVariables.Add("%mobile%", old.Mobile);
                    e.EventVariables.Add("%email%", old.Email);
                    e.EventVariables.Add("%phone%", old.Tel);
                    e.EventVariables.Add("%address%", old.Address);

                    e.EventVariables.Add("%location%", old.BranchLocation);
                    e.EventVariables.Add("%locationtype%", EnumUtil.GetEnumElementPersianTitle((LocationType)old.LocationType));
                    e.EventVariables.Add("%description%", old.Description);

                    e.EventVariables.Add("%hasexperience%", (old.HaveBackgroung) ? "دارم" : "ندارم");


                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchRequest_Change_Admin, e);

                    #endregion
                }
            }
            return op;
        }


        //public override OperationResult Validate(BranchRequest Data, SaveValidationMode Mode)
        //{
        //    OperationResult res = new OperationResult();

        //    if (Mode == SaveValidationMode.Add)
        //    {
        //        if (Context.BranchRequests.Count(u => u.BranchRequestname == Data.BranchRequestname) > 0)
        //        {
        //            res.AddMessage(Model.Enums.BranchRequestMessageKey.DuplicateBranchRequestname);
        //            return res;
        //        }
        //    }
        //    else if (Mode == SaveValidationMode.Edit)
        //    {
        //        if (Context.BranchRequests.Count(u => u.BranchRequestname == Data.BranchRequestname && u.ID != Data.ID) > 0)
        //        {
        //            res.AddMessage(Model.Enums.BranchRequestMessageKey.DuplicateBranchRequestname);
        //            return res;
        //        }
        //    }

        //    return base.Validate(Data, Mode);
        //}
    }
}