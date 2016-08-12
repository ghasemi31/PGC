using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model.Enums;
using kFrameWork.Model;
using pgc.Business;
using pgc.Model;
using kFrameWork.Util;
using System.Text.RegularExpressions;

public partial class UserControls_Project_LoginControl : BaseUserControl
{
    public bool IsPageRefresh = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UserSession.IsUserLogined)
        {
            mlv.ActiveViewIndex = 1;
        }
        else
        {
            mlv.ActiveViewIndex = 0;
        }
        txtPass.Attributes.Add("onkeypress", "return controlEnter('" + btnLogin.UniqueID + "', event)");
        txtCaptcha.Attributes.Add("onkeypress", "return controlEnter('" + btnLogin.UniqueID + "', event)");


        //if (!IsPostBack)
        //{
        //    ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
        //    Session["SessionId"] = ViewState["ViewStateId"].ToString();
        //}
        //else
        //{
        //    if (ViewState["ViewStateId"].ToString() != Session["SessionId"].ToString())
        //    {
        //        IsPageRefresh = true;
        //    }
        //    Session["SessionId"] = System.Guid.NewGuid().ToString();
        //    ViewState["ViewStateId"] = Session["SessionId"].ToString();
        //}

        //if (IsPageRefresh)
        //{
        //    Session["login"] = null;
        //    captchavalidation.Style["display"] = "none";
        //}
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
            Response.Redirect(Request.QueryString["redirecturl"].Replace("~",""));
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

    protected void Login(object sender, EventArgs e)
    {
        captchavalidation.Style["display"] = "none";
        if (string.IsNullOrEmpty(txtEmail.Value) || string.IsNullOrEmpty(txtPass.Value))
        {
            UserSession.AddMessage(UserMessageKey.InvalidEmailOrPassword);
            loginCaptcha.Style["display"] = "inline";
            Session["login"] = "login";
        }
        else
        {
            if (Session["login"] == null)
            {
                Session["login"] = "login";
                IsLogin();
            }
            else
            {
                if (string.IsNullOrEmpty(txtCaptcha.Text))
                {
                    captchavalidation.Style["display"] = "inline";
                }
                else
                {
                    if (Captcha1.UserValidated)
                    {
                        IsLogin();
                    }
                }

            }
        }
    }
    protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
    {
        if (Session["login"] == "login" && !string.IsNullOrEmpty(txtCaptcha.Text.Trim()))
        {
            Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
            e.IsValid = Captcha1.UserValidated;
        }

    }

    public void IsLogin()
    {
        pgc.Business.General.UserBusiness business = new pgc.Business.General.UserBusiness();
        
       
        OperationResult Res;

            OperationResult ResEmailPass = business.ValidEmailAndPass(txtEmail.Value, txtPass.Value);
            //is email format check for email and pass validation
            if (ResEmailPass.Result == ActionResult.Done)
            {
                //email $ pass is valid so login user
                Res = UserSession.LogIn(txtEmail.Value, txtPass.Value);
                UserSession.AddMessage(Res.Messages);
            }
            else
            {
                //email & pass is not valid show message               
                UserSession.AddMessage(ResEmailPass.Messages);
            }
        

        //OperationResult Res = UserSession.LogIn(txtEmail.Value, txtPass.Value);
        //UserSession.AddMessage(Res.Messages);
        //if (Res.Result == ActionResult.Done)
        if (UserSession.IsUserLogined)
        {

            //Login
            #region Event Raising

            SystemEventArgs eARGS = new SystemEventArgs();
            User user = new UserBusiness().RetirveUser(UserSession.UserID);

            eARGS.Related_User = UserSession.User;

            eARGS.EventVariables.Add("%user%", Util.GetFullNameWithGender(user));
            eARGS.EventVariables.Add("%username%", user.Username);
            eARGS.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
            eARGS.EventVariables.Add("%mobile%", user.Mobile);
            eARGS.EventVariables.Add("%email%", user.Email);
            eARGS.EventVariables.Add("%phone%", user.Tel);

            EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Login, eARGS);

            #endregion






            //SaveCookie(txtUserName.Text, txtPass.Text, chkRemember.Checked);
            //Suceed
            if (!string.IsNullOrEmpty(Request.QueryString["redirecturl"]))
            {
                Response.Redirect(Request.QueryString["redirecturl"]);
            }
            else
            {
                if ((Role)UserSession.User.AccessLevel.Role == Role.Admin)
                {
                    Response.Redirect(this.Page.GetRouteUrl("admin-default", null));
                }
                else if ((Role)UserSession.User.AccessLevel.Role == Role.User)
                {
                    Response.Redirect(this.Page.GetRouteUrl("user-userprofile", null));
                }
                else if ((Role)UserSession.User.AccessLevel.Role == Role.Agent)
                {
                    Response.Redirect(this.Page.GetRouteUrl("agent-default", null));
                }
            }
        }
        else
        {
            loginCaptcha.Style["display"] = "inline";
        }
    }

    protected void OrderListClick(object sender, EventArgs e)
    {
        Response.Redirect(this.Page.GetRouteUrl("guest-orderlist", null));
    }


}