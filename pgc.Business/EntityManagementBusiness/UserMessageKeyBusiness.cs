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
    public class UserMessageKeyBusiness
    {
        public static string GetUserMessageDescription(UserMessageKey data)
        {
            pgcEntities context = new pgcEntities();
            string key=data.ToString();
            return context.UserMessages.SingleOrDefault(f => f.Key == key).Description;
        }
    }
}
