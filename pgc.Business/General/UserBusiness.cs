using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kFrameWork.Business;
using kFrameWork.Model;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Util;
using kFrameWork.UI;
using System.Text.RegularExpressions;
using System.Web;

namespace pgc.Business.General
{
    public class UserBusiness
    {
        pgcEntities db = new pgcEntities();

        public UserBusiness()
        {

        }

        public OperationResult RegisterUser(User user)
        {
            OperationResult res = new OperationResult();
            try
            {
                if (db.Users.Where(u => u.Email == user.Email).Count() > 0)
                {
                    res.Result = ActionResult.Failed;
                    res.AddMessage(UserMessageKey.DuplicateEmail);
                    return res;
                }

                user.AccessLevel_ID = 2;
                user.ActivityStatus = Convert.ToInt32(UserActivityStatus.Enabled);
                user.SignUpPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);


                db.Users.AddObject(user);
                db.SaveChanges();


                //User_New
                #region Event Raising

                SystemEventArgs e = new SystemEventArgs();
                e.Related_User = user;

                e.EventVariables.Add("%user%", user.FullName);
                e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                e.EventVariables.Add("%email%", user.Email);
                //e.EventVariables.Add("%phone%", user.Tel);
                //e.EventVariables.Add("%mobile%", user.Mobile);
                //e.EventVariables.Add("%username%", user.Username);
                //e.EventVariables.Add("%regcode%", user.ID.ToString());
                EventHandlerBusiness.HandelSystemEvent(SystemEventKey.User_New, e);

                #endregion


                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.Succeed);
                res.AddMessage(UserMessageKey.RegisterGreeting);
                return res;
            }

            catch(Exception e)
            {
                res.Result = ActionResult.Failed;
                res.AddMessage(UserMessageKey.Failed);
                return res;
            }
        }


        public User RetriveUser(long UserID)
        {
            return db.Users.Where(u => u.ID == UserID).SingleOrDefault();
        }


        public OperationResult UpdateChanges(User user)
        {

            OperationResult res = new OperationResult();

            try
            {

                User OldUser = new UserBusiness().RetriveUser(user.ID);
                var us = db.Users.Where(u => u.Email == user.Email && u.ID != user.ID);
                if (us.Count() > 0)
                {
                    res.Result = ActionResult.Failed;
                    res.AddMessage(Model.Enums.UserMessageKey.DuplicateEmail);
                    return res;
                }

                db.SaveChanges();
                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.Succeed);


                //Password_Change
                #region Event Raising

                if (user.ID == UserSession.UserID && user.pwd != OldUser.pwd)
                {
                    SystemEventArgs e = new SystemEventArgs();
                    e.Related_User = user;

                    e.EventVariables.Add("%user%", user.FullName);
                    e.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
                    e.EventVariables.Add("%mobile%", user.Mobile);
                    e.EventVariables.Add("%email%", user.Email);
                    e.EventVariables.Add("%phone%", user.Tel);
                    e.EventVariables.Add("%password%", user.pwd);

                    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Password_Change, e);
                }
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

        public OperationResult SetEmailAndDisableUserName(long id, string email, string pass)
        {
            OperationResult res = new OperationResult();
            var us = db.Users.Where(u => u.Email == email);
            if (us.Count() > 0)
            {
                res.Result = ActionResult.Failed;
                res.AddMessage(UserMessageKey.DuplicateEmail);
                return res;
            }
            else
            {
                User user = db.Users.SingleOrDefault(u => u.ID == id);
                user.DisableUserName = true;
                user.Email = email;
                user.pwd = pass;
                db.SaveChanges();
                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.EmailAndPassConfirmation);
                res.AddMessage(UserMessageKey.LoginByEmail);
                res.AddMessage(UserMessageKey.LoginOnlyByEmail);
                return res;
            }

        }
        public OperationResult UpdateEmailAndDisableUserName(long id, string email,string pass)
        {
            OperationResult res = new OperationResult();
            var us = db.Users.Where(u => u.Email == email && u.ID != id);
            if (us.Count() > 0)
            {
                res.Result = ActionResult.Failed;
                res.AddMessage(UserMessageKey.DuplicateEmail);
                return res;
            }
            else
            {
                User user = db.Users.SingleOrDefault(u => u.ID == id);
                user.DisableUserName = true;
                user.Email = email;
                user.pwd = pass;
                db.SaveChanges();
                res.Result = ActionResult.Done;
                res.AddMessage(UserMessageKey.EmailAndPassConfirmation);
                res.AddMessage(UserMessageKey.LoginByEmail);
                res.AddMessage(UserMessageKey.LoginOnlyByEmail);
                return res;
            }
            //var us = db.Users.FirstOrDefault(u => u.ID == id);
            //us.Email = email;
            //us.pwd = pass;
            //us.DisableUserName = true;
            //db.SaveChanges();
            //res.Result = ActionResult.Done;
            //res.AddMessage(UserMessageKey.EmailAndPassConfirmation);
            //res.AddMessage(UserMessageKey.LoginByEmail);
            //res.AddMessage(UserMessageKey.LoginOnlyByEmail);
            //return res;
        }

        public void DisableLoginByUserName(User user)
        {
            var us = db.Users.Where(u => u.ID == user.ID).SingleOrDefault();
            us.DisableUserName = true;
            db.SaveChanges();
        }

        public OperationResult ValidEmailAndPass(string Email, string Password)
        {
            OperationResult Res = new OperationResult();
            User CurrentUser = null;
            CurrentUser = db.Users.SingleOrDefault(u => u.Email == Email && u.pwd == Password);
            if (CurrentUser == null)
            {
                Res.Result = ActionResult.Failed;
                Res.AddMessage(UserMessageKey.InvalidUsernameOrPassword);
                return Res;
            }
            else
            {
                Res.Result = ActionResult.Done;
                return Res;
            }
        }
        public OperationResult ValidUserNameAndPass(string UserName, string Password)
        {
            OperationResult Res = new OperationResult();
            User CurrentUser = null;
            CurrentUser = db.Users.SingleOrDefault(u => u.Username == UserName && u.pwd == Password && u.DisableUserName==false);
            if (CurrentUser == null)
            {
                Res.Result = ActionResult.Failed;
                Res.AddMessage(UserMessageKey.InvalidUsernameOrPassword);
                return Res;
            }
            else
            {
                
                    Res.Result = ActionResult.Done;
                    return Res;
                

            }
        }

        public long RetriveUserID(string username, string pass)
        {
            return db.Users.FirstOrDefault(f => f.Username == username && f.pwd == pass).ID;
        }

        public bool IsExistUser(string email)
        {
            if (db.Users.Where(u => u.Email == email).Count() > 0)
            {
                return true;
            }
            return false;
        }

        public long GetUserID(string email)
        {
            return db.Users.FirstOrDefault(u => u.Email == email).ID;
        }

        //public OperationResult RegisterUsers(
        //    string Name,
        //    string Email,
        //    string Pass)
        //{
        //    OperationResult res = new OperationResult();
        //    try
        //    {

        //        if (db.Users.Count(u => u.Username == Email) > 0)
        //        {
        //            res.Result = ActionResult.Failed;
        //            res.AddMessage(Model.Enums.UserMessageKey.DuplicateUsername);
        //            return res;
        //        }

        //        User user = new User();

        //        //Temporary hard code
        //        user.AccessLevel_ID = 2;
        //        user.City_ID = db.Cities.FirstOrDefault().ID;

        //        user.ActivityStatus = (int)UserActivityStatus.Enabled;
        //        user.Address = "";
        //        user.Email = Email;
        //        user.Fax = "";
        //        if (Name.Split(' ').Length >= 2)
        //        {
        //            user.Fname = Name.Split(' ')[0];
        //            user.Lname = Name.TrimStart(Name.Split(' ')[0].ToCharArray()).Trim();
        //        }
        //        else
        //        {
        //            user.Fname = "";
        //            user.Lname = Name;
        //        }
        //        user.Mobile = "";
        //        user.NationalCode = "";
        //        user.PostalCode = "";
        //        user.pwd = Pass;
        //        user.SignUpPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
        //        user.Tel = "";
        //        user.Username = Email;

        //        db.Users.AddObject(user);
        //        db.SaveChanges();

        //        res.Result = ActionResult.Done;
        //        res.AddMessage(UserMessageKey.Succeed);
        //        res.AddMessage(UserMessageKey.RegisterGreeting);
        //        return res;

        //    }
        //    catch (Exception ex)
        //    {
        //        res.Result = ActionResult.Error;
        //        res.AddMessage(Model.Enums.UserMessageKey.Failed);
        //        res.Data.Add("ExceptionMessage", ex.Message);
        //        if (ex.InnerException != null)
        //            res.Data.Add("InnerExceptionMessage", ex.InnerException.Message);
        //        return res;
        //    }
        //}
    }
}
