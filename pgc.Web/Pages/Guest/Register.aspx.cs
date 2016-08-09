using kFrameWork.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Guest_Register :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (Captcha6.UserValidated)
        //{
        //    User user = new User();
        //    OperationResult result = new OperationResult();

        //    user.FullName = txtFullName.Text;
        //    user.pwd = txtPassword.Text;
        //    user.Email = txtEmail.Text;
        //    user.Username = txtEmail.Text;

        //    UserBusiness uBusiness = new UserBusiness();
        //    result = uBusiness.RegisterUser(user);
        //    UserSession.AddMessage(result.Messages);

        //    if (result.Result == ActionResult.Done)
        //    {
        //        UserSession.LogIn(user.Username, user.pwd);
        //        if (!string.IsNullOrEmpty(Request.QueryString["redirecturl"]))

        //            Response.Redirect(Request.QueryString["redirecturl"]);

        //        else
        //            Response.Redirect(GetRouteUrl("user-userprofile", null));
        //    }
        //}

    }

    protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
    {
        //Captcha6.ValidateCaptcha(txtCaptcha.Text.Trim());
        //e.IsValid = Captcha6.UserValidated;
    }
}