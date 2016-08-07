using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Business;
using kFrameWork.Model;
using pgc.Model.Enums;
using pgc.Model;

public partial class Pages_Admin_ResetPwd_Default : BasePage
{
    public User user;//=new User()
    public UserBusiness business = new UserBusiness();
    public long UserID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.id))
        {
            UserID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.id);
        }

        user = business.RetirveUser(UserID);
        if (user == null)
        {
            Response.Redirect(GetRouteUrl("admin-user", null));
        }

        lblName.Text = user.Fname + " " + user.Lname;
        lblUserName.Text = user.Username;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        OperationResult res = new OperationResult();
        res = business.ResetPassword(UserID, txtPassword.Text);
        UserSession.AddMessage(res.Messages);
        if (res.Result == ActionResult.Done)
            Response.Redirect(GetRouteUrl("admin-user", null));
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("admin-user", null);
    }
}