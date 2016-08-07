using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kFrameWork.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Model;

namespace kFrameWork.UI
{
    public class UserSession
    {

        #region Login/Logout

        public static long UserID
        {
            get
            {
                return User.ID;
            }
        }
        public static bool IsUserLogined
        {
            get
            {
                return (User != null);
            }
        }
        public static User User
        {
            get
            {
                if (HttpContext.Current.Session["CurrentUser"] == null)
                {
                    return null;
                }
                return HttpContext.Current.Session["CurrentUser"] as User;
            }
        }
        public static OperationResult LogIn(string Email, string Password)
        {
            OperationResult Res = new OperationResult();

            if (IsUserLogined)
            {
                Res.Result = ActionResult.Failed;
                Res.AddMessage(UserMessageKey.UserAlreadyLoggedIn);
                return Res;
            }

            User CurrentUser = null;
            pgcEntities db = new pgcEntities();
            CurrentUser = db.Users.SingleOrDefault(u => u.Email == Email && u.pwd == Password);
            if (CurrentUser == null)
            {
                    Res.Result = ActionResult.Failed;
                    Res.AddMessage(UserMessageKey.InvalidUsernameOrPassword);
                    return Res;
               }
            else if ((UserActivityStatus)CurrentUser.ActivityStatus != UserActivityStatus.Enabled)
            {
                Res.Result = ActionResult.Failed;
                Res.AddMessage(UserMessageKey.InactiveUser);
                Res.AddMessage(UserMessageKey.ContactAdminInErrorPersistance);
                return Res;
            }
            else if (CurrentUser.Branch_ID.HasValue && !CurrentUser.Branch.IsActive)
            {
                Res.Result = ActionResult.Failed;
                Res.AddMessage(UserMessageKey.InactiveUser);
                Res.AddMessage(UserMessageKey.ContactAdminInErrorPersistance);
                return Res;
            }

            HttpContext.Current.Session["CurrentUser"] = CurrentUser;
            HttpContext.Current.Session["login"] = null;
            Res.Result = ActionResult.Done;

            return Res;
        }
        public static OperationResult LogOut()
        {
            OperationResult Res = new OperationResult();

            if (!IsUserLogined)
            {
                Res.Result = ActionResult.Failed;
                Res.AddMessage(UserMessageKey.UserAlreadyLoggedOut);
                return Res;
            }

            HttpContext.Current.Session["CurrentUser"] = null;
            Res.Result = ActionResult.Done;
            return Res;
        }

        #endregion

        #region Messaging

        public static List<UserMessageKey> CurrentMessages
        {
            get
            {
                if (HttpContext.Current.Session["CurrentMessages"] == null)
                {
                    return new List<UserMessageKey>();
                }
                return HttpContext.Current.Session["CurrentMessages"] as List<UserMessageKey>;
            }
            private set
            {
                HttpContext.Current.Session["CurrentMessages"] = value;
            }
        }

        public static Dictionary<object,object> CurrentData
        {
            get
            {
                if (HttpContext.Current.Session["CurrentData"] == null)
                {
                    return new Dictionary<object, object>();
                }
                return HttpContext.Current.Session["CurrentData"] as Dictionary<object, object>;
            }
            set
            {
                HttpContext.Current.Session["CurrentData"] = value;
            }
        }

        public static void AddData(object Key , object Value)
        {
            Dictionary<object, object> temp = CurrentData;
            temp.Add(Key, Value);
            CurrentData = temp;
        }

        public static void AddMessage(List<UserMessageKey> Keys)
        {
            foreach (UserMessageKey Key in Keys)
            {
                AddMessage(Key);
            }
        }

        public static void AddMessage(UserMessageKey Key)
        {
            List<UserMessageKey> TempMessages = CurrentMessages;
            TempMessages.Add(Key);
            CurrentMessages = TempMessages;
        }



        public static void ClearMessages()
        {
            CurrentMessages = null;
        }

        #endregion


    }
}