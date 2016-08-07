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

public partial class Pages_Agent_UpdateProfile_Default : BasePage
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
            if (user.AccessLevel.Role == (int)Role.Agent)
                lblRole.Text = EnumUtil.GetEnumElementPersianTitle((Role)user.AccessLevel.Role);
            txtAddress.Text = user.Address;
            //lkcProvince.SetSelectedValue(user.City.Province_ID);
            //lkcCity.SetSelectedValue(user.City_ID);
            txtEmail.Text = user.Email;
            txtFax.Text = user.Fax;
            //txtfName.Text = user.Fname;
            //txtlName.Text = user.Lname;
            txtFullname.Text = user.FullName;
            txtMobile.Text = user.Mobile;
            txtNationalCode.Text = user.NationalCode;
            txtPostalCode.Text = user.PostalCode;
            txtTel.Text = user.Tel;
            //txtUsername.Text = user.Username;
            lkcGender.SetSelectedValue(user.Gender);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //user.Fname = txtfName.Text;
        //user.Lname = txtlName.Text;
        user.FullName = txtFullname.Text;
        //user.City_ID = lkcCity.GetSelectedValue<long>();
        user.Username = txtEmail.Text;
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
            Response.Redirect(GetRouteUrl("agent-default", null));

        if (res.Result == ActionResult.Done)
        {
            Response.Redirect(GetRouteUrl("user-default", null));

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
        Response.RedirectToRoute("agent-default", null);
    }
}