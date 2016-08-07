using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model;
using pgc.Business.General;
using kFrameWork.Model;
using pgc.Model.Enums;

public partial class Pages_User_ChangePwd_Default : BasePage
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

        if (user.pwd == txtOldPassword.Text)
        {
            user.pwd = txtNewPassword.Text;


            res = business.UpdateChanges(user);
            UserSession.AddMessage(res.Messages);
            if (res.Result == ActionResult.Done)
            {
                if ((Role)UserSession.User.AccessLevel.Role == Role.User)
                {
                    Response.Redirect(GetRouteUrl("user-userprofile", null));
                }
                if ((Role)UserSession.User.AccessLevel.Role == Role.Admin)
                {
                    Response.Redirect(GetRouteUrl("admin-default", null));
                }
                if ((Role)UserSession.User.AccessLevel.Role == Role.Agent)
                {
                    Response.Redirect(GetRouteUrl("agent-default", null));
                }
            }
        }
        else
            res.Result = ActionResult.Failed;
        UserSession.AddMessage(UserMessageKey.InvalidOldPassword);

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("user-default", null);
    }
}