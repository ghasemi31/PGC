using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model.Enums;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using kFrameWork.Model;

public partial class Pages_Guest_Email : BasePage
{
    public UserBusiness business = new UserBusiness();
    public User user;
    public bool isEmail;
    public bool redirect;
    public string oldPass;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["login"] = null;
        long id = this.GetQueryStringValue<long>(QueryStringKeys.id);
        redirect = this.HasValidQueryString<int>(QueryStringKeys.r);
        user = business.RetriveUser(id);
        oldPass = user.pwd;
        isEmail = Regex.IsMatch(user.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        if (!IsPostBack)
        {
            if (isEmail)
            {
                txtEmail.Text = user.Email;
            } 
        }
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        OperationResult result = new OperationResult();
        if (isEmail)
        {
            result = business.UpdateEmailAndDisableUserName(user.ID,txtEmail.Text, txtPassword.Text);
            UserSession.AddMessage(result.Messages);
            if (result.Result == ActionResult.Done)
            {
                if (redirect)
                {
                    login();
                }
                else
                {
                    Orderlogin();
                }
            }
            
            
        }
        else
        {
            result = business.SetEmailAndDisableUserName(user.ID, txtEmail.Text, txtPassword.Text);
            if (result.Result == ActionResult.Done)
            {
                UserSession.AddMessage(result.Messages);
                if (redirect)
                {
                    login();
                }
                else
                {
                    Orderlogin();
                }               
            }
            else
            {
                UserSession.AddMessage(result.Messages);
            }
        }



    }

    public void login()
    {
        OperationResult Res;
        Res = UserSession.LogIn(txtEmail.Text, txtPassword.Text);
        UserSession.AddMessage(Res.Messages);
        if (UserSession.IsUserLogined)
        {
            //Login
            #region Event Raising

            pgc.Business.SystemEventArgs eARGS = new pgc.Business.SystemEventArgs();
            User user1 = new pgc.Business.UserBusiness().RetirveUser(UserSession.UserID);

            eARGS.Related_User = UserSession.User;

            eARGS.EventVariables.Add("%user%", user1.FullName);
            eARGS.EventVariables.Add("%username%", user1.Username);
            eARGS.EventVariables.Add("%date%", kFrameWork.Util.DateUtil.GetPersianDateShortString(DateTime.Now));
            eARGS.EventVariables.Add("%mobile%", user1.Mobile);
            eARGS.EventVariables.Add("%email%", user1.Email);
            eARGS.EventVariables.Add("%phone%", user1.Tel);

            pgc.Business.EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Login, eARGS);

            #endregion

            #region Event Raising

            pgc.Business.SystemEventArgs eARGS2 = new pgc.Business.SystemEventArgs();
            eARGS2.Related_User = UserSession.User;
            eARGS2.EventVariables.Add("%fullname%", user.FullName);
            eARGS2.EventVariables.Add("%oldusername%", user.Username);
            eARGS2.EventVariables.Add("%oldpassword%", oldPass);
            eARGS2.EventVariables.Add("%email%", user.Email);
            eARGS2.EventVariables.Add("%newpassword%", user.pwd);
            eARGS2.EventVariables.Add("%date%", kFrameWork.Util.DateUtil.GetPersianDateShortString(DateTime.Now));
            eARGS2.EventVariables.Add("%mobile%", user.Mobile);
            pgc.Business.EventHandlerBusiness.HandelSystemEvent(SystemEventKey.EmailAndPassConfirmation, eARGS2);

            #endregion

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
    }
    public void Orderlogin()
    {
        #region Event Raising

        pgc.Business.SystemEventArgs eARGS2 = new pgc.Business.SystemEventArgs();
        eARGS2.Related_User = UserSession.User;
        eARGS2.EventVariables.Add("%fullname%", user.FullName);
        eARGS2.EventVariables.Add("%oldusername%", user.Username);
        eARGS2.EventVariables.Add("%oldpassword%", oldPass);
        eARGS2.EventVariables.Add("%email%", user.Email);
        eARGS2.EventVariables.Add("%newpassword%", user.pwd);
        eARGS2.EventVariables.Add("%date%", kFrameWork.Util.DateUtil.GetPersianDateShortString(DateTime.Now));
        eARGS2.EventVariables.Add("%mobile%", user.Mobile);
        pgc.Business.EventHandlerBusiness.HandelSystemEvent(SystemEventKey.EmailAndPassConfirmation, eARGS2);

        #endregion
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
    }
}