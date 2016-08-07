using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pgc.Model.Enums;
using pgc.Model;
using kFrameWork.Util;
using kFrameWork.Model;

namespace pgc.Business.General
{
    public class AppBusiness
    {
        pgcEntities Context ;
        public AppBusiness()
        {
            Context = new pgcEntities();
        }
        public AppSetting Retrive()
        {
            return Context.AppSettings.SingleOrDefault(o => o.ID == 1);
        }
        public OperationResult Save()
        {
            OperationResult Res = new OperationResult();
            Context.SaveChanges();
            Res.Result = ActionResult.Done;
            Res.AddMessage(UserMessageKey.Succeed);
            return Res;
        }

       
    }
}
