using kFrameWork.Model;
using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Business;
using pgc.Model;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_User_UpdateProfile : BasePage
{
    pgc.Business.General.UserBusiness business = new pgc.Business.General.UserBusiness();
    User user;
    protected void Page_Load(object sender, EventArgs e)
    {
        user = business.RetriveUser(UserSession.UserID);
        if (!IsPostBack)
        {
            //txtFullName.Text = user.FullName;
            //txtEmail.Text = user.Email;
            //txtNationalCode.Text = user.NationalCode;
            //txtPostalCode.Text = user.PostalCode;
            //txtTel.Text = user.Tel;
            //txtMobile.Text = user.Mobile;
            //txtAddress.Text = user.Address;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        //user.FullName = txtFullName.Text;
        //user.Email = txtEmail.Text;
        //user.NationalCode = txtNationalCode.Text;
        //user.Tel = txtTel.Text;
        //user.Mobile = txtMobile.Text;
        //user.PostalCode = txtPostalCode.Text;
        //user.Address = txtAddress.Text;
        //OperationResult res = new OperationResult();
        //res = business.UpdateChanges(user);
        //UserSession.AddMessage(res.Messages);
        //if (res.Result == ActionResult.Done)
        //{

        //    Response.Redirect(GetRouteUrl("user-userprofile", null));

        //    //Profile_Change
        //    #region Profile_Change

        //    SystemEventArgs eArg = new SystemEventArgs();


        //    eArg.Related_User = user;


        //    eArg.EventVariables.Add("%user%", user.FullName);
        //    eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
        //    eArg.EventVariables.Add("%mobile%", user.Mobile);
        //    eArg.EventVariables.Add("%email%", user.Email);

        //    EventHandlerBusiness.HandelSystemEvent(SystemEventKey.Profile_Change, eArg);

        //    #endregion

        //}
        ////}
    }
}