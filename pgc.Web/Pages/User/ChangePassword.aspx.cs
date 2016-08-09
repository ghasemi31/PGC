using kFrameWork.Model;
using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_User_ChangePassword : BasePage
{
    UserBusiness business = new UserBusiness();
    User user;
    protected void Page_Load(object sender, EventArgs e)
    {
        user = business.RetriveUser(UserSession.UserID);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        OperationResult res = new OperationResult();

        if (user.pwd == txtPass.Text)
        {
            user.pwd = txtNewPass.Text;


            res = business.UpdateChanges(user);
            UserSession.AddMessage(res.Messages);
            if (res.Result == ActionResult.Done)
                Response.Redirect(GetRouteUrl("user-userprofile", null));
        }
        else
            res.Result = ActionResult.Failed;
        UserSession.AddMessage(UserMessageKey.InvalidOldPassword);
    }
}