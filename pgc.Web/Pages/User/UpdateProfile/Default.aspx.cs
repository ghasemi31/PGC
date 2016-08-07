using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model;
using pgc.Model.Enums;
using pgc.Business.General;
using kFrameWork.Model;
using kFrameWork.Util;
using pgc.Business;

public partial class Pages_User_UpdateProfile_Default : BasePage
{
    pgc.Business.General.UserBusiness business = new pgc.Business.General.UserBusiness();
    User user;
    protected void Page_Load(object sender, EventArgs e)
    {

        user = business.RetriveUser(UserSession.UserID);
        
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!IsPostBack)
        {
            lblSignUpPersianDate.Text = user.SignUpPersianDate;
            lblAccessLevel.Text = user.AccessLevel.Title;
            if (user.AccessLevel.Role ==(int)Role.User)
                lblRole.Text = EnumUtil.GetEnumElementPersianTitle((Role)user.AccessLevel.Role);
            txtAddress.Text = user.Address;
            //lkcProvince.SetSelectedValue(user.City.Province_ID);
            //lkcCity.SetSelectedValue(user.City_ID);
            txtEmail.Text = user.Email;
            txtFax.Text = user.Fax;
            //txtfName.Text = user.Fname;
            //txtlName.Text = user.Lname;
            txtFullName.Text = user.FullName;
            txtMobile.Text = user.Mobile;
            txtNationalCode.Text = user.NationalCode;
            txtPostalCode.Text = user.PostalCode;
            txtTel.Text = user.Tel;
            txtUsername.Text = user.Username;
            lkcGender.SetSelectedValue(user.Gender);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //user.Fname = txtfName.Text;
        //user.Lname = txtlName.Text;
        //user.City_ID = lkcCity.GetSelectedValue<long>();
        user.FullName = txtFullName.Text;
        user.Username = txtUsername.Text;
        user.Email = txtEmail.Text;
        user.NationalCode = txtNationalCode.Text;
        user.Tel = txtTel.Text;
        user.Mobile = txtMobile.Text;
        user.Fax = txtFax.Text;
        user.PostalCode = txtPostalCode.Text;
        user.Address = txtAddress.Text;
        user.Gender = lkcGender.GetSelectedValue<int>();

        OperationResult res = new OperationResult();
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

            //Profile_Change
            #region Profile_Change

            SystemEventArgs eArg = new SystemEventArgs();


            eArg.Related_User = user;


            eArg.EventVariables.Add("%user%", Util.GetFullNameWithGender(user));
            eArg.EventVariables.Add("%username%", user.Username);
            eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
            eArg.EventVariables.Add("%mobile%", user.Mobile);
            eArg.EventVariables.Add("%email%", user.Email);

            EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Profile_Change, eArg);

            #endregion

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("user-default", null);
    }
}