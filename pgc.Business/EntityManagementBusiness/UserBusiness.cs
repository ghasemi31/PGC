using System;
using System.Linq;
using kFrameWork.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using System.Collections.Generic;
using kFrameWork.UI;

namespace pgc.Business
{
    public class UserBusiness:BaseEntityManagementBusiness<User,pgcEntities>
    {
        public UserBusiness()
        {
            Context = new pgcEntities();
        }

        public IQueryable Search_Select(int startRowIndex, int maximumRows, UserPattern Pattern)
        {
            if (startRowIndex == 0 && maximumRows == 0)
                return null;

            string Today = DateUtil.GetPersianDateShortString(DateTime.Now);

            var Result = Search_Where(Context.Users, Pattern)
                .OrderByDescending(f => f.SignUpPersianDate)
                .Select(f => new
                {
                    f.ID,
                    Name = f.Fname + " " + f.Lname,
                    f.Username,
                    City = f.City.Title,
                    Role = f.AccessLevel.Role,
                    AccessLevel =  f.AccessLevel.Title,
                    f.ActivityStatus,
                    f.SignUpPersianDate,
                    f.Mobile,
                    f.Email,
                    f.FullName
                });

            return Result.Skip(startRowIndex).Take(maximumRows);
        }

        public int Search_Count(UserPattern Pattern)
        {
            return Search_Where(Context.Users, Pattern).Count();
        }

        public IQueryable<User> Search_Where(IQueryable<User> list, UserPattern Pattern)
        {
            //DefaultPattern
            if (Pattern == null)
                return list;

            //Search By Pattern
            
            if (Pattern.AccessLevel_ID > 0)
                list = list.Where(u => u.AccessLevel_ID == Pattern.AccessLevel_ID);

            if (BasePattern.IsEnumAssigned(Pattern.ActivityStatus))
                list = list.Where(u => u.ActivityStatus == (int)Pattern.ActivityStatus);

            if (!string.IsNullOrEmpty(Pattern.Address))
                list = list.Where(u => u.Address.Contains(Pattern.Address));

            if (Pattern.City_ID > 0)
                list = list.Where(u => u.City_ID == Pattern.City_ID);

            if (!string.IsNullOrEmpty(Pattern.Email))
                list = list.Where(u => u.Email.Contains(Pattern.Email));

            if (!string.IsNullOrEmpty(Pattern.Fax))
                list = list.Where(u => u.Fax.Contains(Pattern.Fax));

            if (!string.IsNullOrEmpty(Pattern.Mobile))
                list = list.Where(u => u.Mobile.Contains(Pattern.Mobile));

            if (!string.IsNullOrEmpty(Pattern.BranchTitle))
                list = list.Where(u => u.Branch_ID !=null && u.Branch.Title.Contains(Pattern.BranchTitle));

            if (!string.IsNullOrEmpty(Pattern.Name))
                list = list.Where(u => (u.Fname + " " + u.Lname).Contains(Pattern.Name));

            if (!string.IsNullOrEmpty(Pattern.NationalCode))
                list = list.Where(u => u.NationalCode.Contains(Pattern.NationalCode));

            if (!string.IsNullOrEmpty(Pattern.PostalCode))
                list = list.Where(u => u.PostalCode.Contains(Pattern.PostalCode));

            if (Pattern.Province_ID > 0)
                list = list.Where(u => u.City.Province_ID == Pattern.Province_ID);

            if (BasePattern.IsEnumAssigned(Pattern.Role))
                list = list.Where(u => u.AccessLevel.Role == (int)Pattern.Role);
            
            if (BasePattern.IsEnumAssigned(Pattern.Gender))
                list = list.Where(u => u.Gender == (int)Pattern.Gender);

            if (!string.IsNullOrEmpty(Pattern.Tel))
                list = list.Where(u => u.Tel.Contains(Pattern.Tel));

            if (!string.IsNullOrEmpty(Pattern.Username))
                list = list.Where(u => u.Username.Contains(Pattern.Username));
            
            if (Pattern.Branch_ID> 0)
                list = list.Where(u => u.Branch_ID == Pattern.Branch_ID);

            switch (Pattern.SignUpPersianDate.SearchMode)
            {
                case DateRangePattern.SearchType.Between:
                    if (Pattern.SignUpPersianDate.HasFromDate && Pattern.SignUpPersianDate.HasToDate)
                        list = list.Where(f => f.SignUpPersianDate.CompareTo(Pattern.SignUpPersianDate.FromDate) >= 0
                            && f.SignUpPersianDate.CompareTo(Pattern.SignUpPersianDate.ToDate) <= 0);
                    break;
                case DateRangePattern.SearchType.Greater:
                    if (Pattern.SignUpPersianDate.HasDate)
                        list = list.Where(f => f.SignUpPersianDate.CompareTo(Pattern.SignUpPersianDate.Date) >= 0);
                    break;
                case DateRangePattern.SearchType.Less:
                    if (Pattern.SignUpPersianDate.HasDate)
                        list = list.Where(f => f.SignUpPersianDate.CompareTo(Pattern.SignUpPersianDate.Date) <= 0);
                    break;
                case DateRangePattern.SearchType.Equal:
                    if (Pattern.SignUpPersianDate.HasDate)
                        list = list.Where(f => f.SignUpPersianDate.CompareTo(Pattern.SignUpPersianDate.Date) == 0);
                    break;
            }

            return list;
        }




        public override OperationResult Validate(User Data, SaveValidationMode Mode)
        {
            OperationResult res = new OperationResult();

            if (Mode == SaveValidationMode.Add)
            {
                if (Context.Users.Count(u => u.Email == Data.Email) > 0)
                {
                    res.AddMessage(Model.Enums.UserMessageKey.DuplicateEmail);
                    return res;
                }
            }
            else if (Mode == SaveValidationMode.Edit)
            {
                if (Context.Users.Count(u => u.Email == Data.Email && u.ID != Data.ID) > 0)
                {
                    res.AddMessage(Model.Enums.UserMessageKey.DuplicateEmail);
                    return res;
                }
            }

            if (Data.AccessLevel_ID.HasValue)
            {
                AccessLevel accessLevel = new AccessLevelBusiness().Retrieve(Data.AccessLevel_ID.Value);
                if (accessLevel.Role == (int)pgc.Model.Enums.Role.Agent && Data.Branch_ID.HasValue && Data.Branch_ID == -1)
                {
                    res.Result = ActionResult.Failed;
                    res.AddMessage(UserMessageKey.UnselectedBranch);
                    return res;

                }
            }
            return base.Validate(Data, Mode);
        }

        public User RetirveUser(long UserID)
        {
            return Context.Users.Where(u => u.ID == UserID).SingleOrDefault();
        }

        public OperationResult ResetPassword(long User_ID, string NewPwd)
        {
            User user ;//= new User();
            OperationResult res = new OperationResult();

            try
            {

                user = Context.Users.Where(u => u.ID == User_ID).SingleOrDefault();


                user.pwd = NewPwd;
                Context.SaveChanges();

                if (user.pwd != NewPwd)
                {
                    //User_Action_Admin
                    #region User_RstPassword_Admin

                    SystemEventArgs eArg = new SystemEventArgs();
                    User doer = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                    eArg.Related_Doer = doer;
                    eArg.Related_User = user;
                    if (user.Branch_ID.HasValue && user.Branch_ID.Value > 0)
                        eArg.Related_Branch = new BranchBusiness().Retrieve(user.Branch_ID.Value);

                    eArg.EventVariables.Add("%user%", user.FullName);
                    eArg.EventVariables.Add("%username%", user.Username);
                    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    eArg.EventVariables.Add("%mobile%", user.Mobile);
                    eArg.EventVariables.Add("%email%", user.Email);
                    eArg.EventVariables.Add("%newpwd%", "رمز عبور جدید");
                    eArg.EventVariables.Add("%action%", "بازنشانی رمز عبور");
                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.User_RstPassword_Admin, eArg);

                    #endregion
                }


                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.Succeed);
                return res;
            }

            catch
            {
                res.Result = ActionResult.Failed;
                res.AddMessage(UserMessageKey.Failed);
                return res;
            }
        }


        public override OperationResult Update(User Data)
        {
            OperationResult op = base.Update(Data);

            if (op.Result == ActionResult.Done)
            {
                //User_Action_Admin
                #region User_Action_Admin

                SystemEventArgs eArg = new SystemEventArgs();
                User user = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                eArg.Related_Doer = user;
                eArg.Related_User = Data;
                if (Data.Branch_ID.HasValue && Data.Branch_ID.Value > 0)
                    eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID.Value);

                eArg.EventVariables.Add("%user%",Data.FullName);
                eArg.EventVariables.Add("%username%", Data.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", Data.Mobile);
                eArg.EventVariables.Add("%email%", Data.Email);
                eArg.EventVariables.Add("%action%", "بروزرسانی");
                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.User_Action_Admin, eArg);

                #endregion
            }


            return op;
        }


        public override OperationResult Insert(User Data)
        {
            OperationResult op = base.Insert(Data);

            if (op.Result == ActionResult.Done)
            {            
                //BranchPage_Edti
                #region Order_Remove_Admin

                SystemEventArgs eArg = new SystemEventArgs();
                User user = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);
                

                eArg.Related_Doer = user;
                eArg.Related_User = Data;
                if (Data.Branch_ID.HasValue && Data.Branch_ID.Value > 0)
                    eArg.Related_Branch = new BranchBusiness().Retrieve(Data.Branch_ID.Value);

                eArg.EventVariables.Add("%user%",Data.FullName);
                eArg.EventVariables.Add("%username%", Data.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", Data.Mobile);
                eArg.EventVariables.Add("%email%", Data.Email);
                eArg.EventVariables.Add("%action%", "حذف");

                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.User_Action_Admin, eArg);

                #endregion
            }

            return op;
        }

        public override OperationResult Delete(long ID)
        {
            User oldUser = new UserBusiness().Retrieve(ID);
            OperationResult op1 = new OperationResult();
            op1.Result = ActionResult.Done;

            if (oldUser.Orders != null &&
                oldUser.Orders.Count > 0 &&
                oldUser.Orders.Any(g => g.OnlinePayments != null && g.OnlinePayments.Count > 0))
            {
                op1.Result = ActionResult.Failed;
                op1.AddMessage(Model.Enums.UserMessageKey.UserHasOnlineTransactionOnlyCanDeactive);
            }
            
            //if (oldUser.BranchDemands != null && oldUser.BranchDemands.Count > 0)
            //{
            //    op1.Result = ActionResult.Failed;
            //    op1.AddMessage(Model.Enums.UserMessageKey.FirstDeleteBranchDemands);
            //}

            if (op1.Result == ActionResult.Failed)
                return op1;

            OperationResult op = base.Delete(ID);

            if (op.Result == ActionResult.Done)
            {
                //User_Action_Admin
                #region User_Action_Admin

                SystemEventArgs eArg = new SystemEventArgs();
                User user = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);


                eArg.Related_Doer = user;
                eArg.Related_User = oldUser;
                if (oldUser.Branch_ID.HasValue && oldUser.Branch_ID.Value > 0)
                    eArg.Related_Branch = new BranchBusiness().Retrieve(oldUser.Branch_ID.Value);

                eArg.EventVariables.Add("%user%", oldUser.FullName);
                eArg.EventVariables.Add("%username%", oldUser.Username);
                eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                eArg.EventVariables.Add("%mobile%", oldUser.Mobile);
                eArg.EventVariables.Add("%email%", oldUser.Email);
                eArg.EventVariables.Add("%action%", "حذف");
                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.User_Action_Admin, eArg);

                #endregion
            }
            return op;
        }

        public List<User> Search_Select(UserPattern Pattern)
        {
            var Result = Search_Where(Context.Users, Pattern)
                .OrderByDescending(f => f.SignUpPersianDate);


            return Result.ToList();
        }
    }
}