using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model.Enums;
using pgc.Model;
using kFrameWork.Util;
using pgc.Business.General;
using kFrameWork.Model;

public partial class Pages_Guest_BranchRequest : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Captcha1.UserValidated)
        {
            BranchRequest branchrequest = new BranchRequest();
            OperationResult result = new OperationResult();

            branchrequest.FullName = txtFullName.Text;
            branchrequest.Address = txtAddress.InnerText;
            branchrequest.ApplicatorName = txtApplicatorName.Text;
            branchrequest.BranchLocation = txtLocation.Text;
            branchrequest.Description = txtDesc.InnerText;
            branchrequest.Email = txtEmail.Text;
            branchrequest.Mobile = txtMobile.Text;
            branchrequest.Tel = txtPhone.Text;
            if (rbLeased.Checked == true)
                branchrequest.LocationType = Convert.ToInt32(rbLeased.Value);
            else if (rbPersonal.Checked == true)
                branchrequest.LocationType = Convert.ToInt32(rbPersonal.Value);

            if (chkBachground.Checked == true)
                branchrequest.HaveBackgroung = Convert.ToBoolean(chkBachground.Checked);


            BranchRequestBusiness brBusiness = new BranchRequestBusiness();
            result = brBusiness.Request(branchrequest);

            UserSession.AddMessage(result.Messages);

            if (result.Result == ActionResult.Done)
            {
                txtFullName.Text = string.Empty;
                txtAddress.InnerText = string.Empty;
                txtApplicatorName.Text = string.Empty;
                txtLocation.Text = string.Empty;
                txtDesc.InnerText = string.Empty;
                txtEmail.Text = string.Empty;
                txtMobile.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtCaptcha.Text = string.Empty;
            }
        }
        

    }

    protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
    {
        Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
        e.IsValid = Captcha1.UserValidated;
    }

}