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

public partial class Pages_Guest_Register :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UserSession.IsUserLogined && (Role)UserSession.User.AccessLevel.Role == Role.User)
        {
            //Response.Redirect("~/Pages/User/Default.aspx");
            Response.Redirect(this.Page.GetRouteUrl("user-userprofile", null));
        }

        if (UserSession.IsUserLogined && (Role)UserSession.User.AccessLevel.Role == Role.Admin)
        {
            //Response.Redirect("~/Pages/Admin/Default.aspx");
            Response.Redirect(this.Page.GetRouteUrl("admin-default", null));
        }
        if (UserSession.IsUserLogined && (Role)UserSession.User.AccessLevel.Role == Role.Agent)
        {
            Response.Redirect(this.Page.GetRouteUrl("agent-default", null));
        }
       
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Captcha6.UserValidated)
        {

           User user = new User();
            OperationResult result = new OperationResult();

            user.FullName = txtFullName.Text;
            user.pwd = txtPassword.Text;
            user.Email = txtEmail.Text;
            user.Username = txtEmail.Text;
            user.NationalCode = txtNationalCode.Text;
            user.City_ID = lkcCity.GetSelectedValue<long>();
            user.Gender = lkpSexStatus.GetSelectedValue<int>();
            user.FatherName = txtFatherName.Text;
            user.Address = txtAddress.Text;
            user.Tel = txtTel.Text;
            user.Mobile = txtMobile.Text;
          

          
            UserBusiness uBusiness = new UserBusiness();
            result = uBusiness.RegisterUser(user);
            UserSession.AddMessage(result.Messages);

            if (result.Result == ActionResult.Done)
            {
                UserSession.LogIn(user.Username, user.pwd);
                if (!string.IsNullOrEmpty(Request.QueryString["redirecturl"]))

                    Response.Redirect(Request.QueryString["redirecturl"]);

                else
                    Response.Redirect(GetRouteUrl("user-userprofile", null));
            }
        }

    }


    protected void LogOut(object sender, EventArgs e)
    {
        UserSession.LogOut();
        Session["login"] = null;
        //Response.Redirect("~/Pages/Guest/Default.aspx");
        Response.Redirect(this.Page.GetRouteUrl("guest-default", null));
    }

    protected void CPanelClick(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["redirecturl"]))
        {
            Response.Redirect(Request.QueryString["redirecturl"]);
        }
        else
        {
            if (UserSession.IsUserLogined && (Role)UserSession.User.AccessLevel.Role == Role.User)
            {
                //Response.Redirect("~/Pages/User/Default.aspx");
                Response.Redirect(this.Page.GetRouteUrl("user-userprofile", null));
            }

            if (UserSession.IsUserLogined && (Role)UserSession.User.AccessLevel.Role == Role.Admin)
            {
                //Response.Redirect("~/Pages/Admin/Default.aspx");
                Response.Redirect(this.Page.GetRouteUrl("admin-default", null));
            }
            if (UserSession.IsUserLogined && (Role)UserSession.User.AccessLevel.Role == Role.Agent)
            {
                Response.Redirect(this.Page.GetRouteUrl("agent-default", null));
            }
        }
    }

    protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
    {
        Captcha6.ValidateCaptcha(txtCaptcha.Text.Trim());
        e.IsValid = Captcha6.UserValidated;
    }
}