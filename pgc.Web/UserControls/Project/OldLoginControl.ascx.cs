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

public partial class UserControls_Project_LoginControl : BaseUserControl
{
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
    }

    protected void LogOut(object sender, EventArgs e)
    {
        UserSession.LogOut();
        //Response.Redirect("~/Pages/Guest/Default.aspx");
        Response.Redirect(this.Page.GetRouteUrl("guest-default",null));
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
                Response.Redirect(this.Page.GetRouteUrl("user-default", null));
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
        if (string.IsNullOrEmpty(txtUserName.Value) || string.IsNullOrEmpty(txtPass.Value))
        {
            UserSession.AddMessage(UserMessageKey.InvalidUsernameOrPassword);
        }
        else
        {
            OperationResult Res = UserSession.LogIn(txtUserName.Value, txtPass.Value);
            UserSession.AddMessage(Res.Messages);

            if (Res.Result == ActionResult.Done)
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
                    //redirect to cpanel or messages page of his role
                    if ((Role)UserSession.User.AccessLevel.Role == Role.Admin)
                    {
                        //Response.Redirect("~/Pages/Admin/Default.aspx");
                        Response.Redirect(this.Page.GetRouteUrl("admin-default", null));
                    }
                    else if ((Role)UserSession.User.AccessLevel.Role == Role.User)
                    {
                        //Response.Redirect("~/Pages/User/Default.aspx");
                        Response.Redirect(this.Page.GetRouteUrl("user-default", null));
                    }
                    else if ((Role)UserSession.User.AccessLevel.Role == Role.Agent)
                    {
                        Response.Redirect(this.Page.GetRouteUrl("agent-default", null));
                    }
                }
            }
        }
    }

    protected void OrderListClick(object sender, EventArgs e)
    {
        Response.Redirect(this.Page.GetRouteUrl("guest-orderlist", null));
    }
}