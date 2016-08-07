using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model;
using pgc.Business.General;
using kFrameWork.Model;
using pgc.Business;
using kFrameWork.Util;
using pgc.Model.Enums;

public partial class Pages_Agent_BranchInfo_Default : BasePage
{
    pgc.Business.General.BranchBusiness business = new pgc.Business.General.BranchBusiness();
    Branch branch;
    protected void Page_Load(object sender, EventArgs e)
    {

        branch = business.RetriveBranch(UserSession.User.Branch.UrlKey);
        //branch = business.RetirveBranchID(UserSession.User.Branch_ID);

        if (!IsPostBack)
        {
            txtTitle.Text = branch.Title;
            txtCode.Text = branch.CityCode;
            txtPhoneNumbers.Text = branch.PhoneNumbers;
            txtAddress.Text = branch.Address;
            txtOrdering.Text = branch.HoursOrdering;
            txtServing.Text = branch.HoursServingFood;
            txtChair.Text = branch.NumberOfChair.ToString();
            txtTransportCost.Text = branch.TransportCost;
            txtPageTitle.Text = branch.PageTitle;
            txtPageDescription.Text = branch.PageDescription;
            txtPageKeywords.Text = branch.PageKeywords.Replace(", ", "\n");
            //txtSummary.Text = branch.Summary;
            //txtBody.Text = branch.Body;
            //txtDesc.Text = branch.Description;
            //txtInfo.Text = branch.BranchInfo;
            txtLongitude.Text = branch.Longitude.ToString();
            txtlatitude.Text = branch.latitude.ToString();           
            fupThumbListPic.FilePath = branch.ThumbListPath;
            //fupBranch.FilePath = branch.LargeThumbImagePath;
            //fupLargPic.FilePath = branch.LargeImagePath;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        branch.Title = txtTitle.Text;
        branch.CityCode = txtCode.Text;
        branch.PhoneNumbers = txtPhoneNumbers.Text;
        //branch.Summary = txtSummary.Text;
        //branch.Body = txtBody.Text;
        //branch.Description = txtDesc.Text;
        //branch.BranchInfo = txtInfo.Text;
        branch.Longitude = Convert.ToDouble(txtLongitude.Text);
        branch.latitude = Convert.ToDouble(txtlatitude.Text);
        branch.ThumbListPath = fupThumbListPic.FilePath;
        branch.PageTitle = txtPageTitle.Text;
        branch.PageDescription = txtPageDescription.Text;
        branch.PageKeywords = txtPageKeywords.Text.Replace("\n", ", ");
        //branch.LargeThumbImagePath = fupBranch.FilePath;
        //branch.LargeImagePath = fupLargPic.FilePath;

        OperationResult res = new OperationResult();
        res = business.UpdateChanges(branch);



        //BranchPage_Edti
        #region Event Raising

        SystemEventArgs eArg = new SystemEventArgs();
        User user = new pgc.Business.UserBusiness().Retrieve(UserSession.UserID);
        
        eArg.Related_Doer = user;
        eArg.Related_Branch = branch;

        eArg.EventVariables.Add("%user%", Util.GetFullNameWithGender(user));
        eArg.EventVariables.Add("%username%", user.Username);
        eArg.EventVariables.Add("%date%", DateUtil.GetPersianDateShortString(DateTime.Now));
        eArg.EventVariables.Add("%mobile%", user.Mobile);
        eArg.EventVariables.Add("%email%", user.Email);

        eArg.EventVariables.Add("%title%", branch.Title);
        
        EventHandlerBusiness.HandelSystemEvent(SystemEventKey.BranchPage_Edit, eArg);

        #endregion

        
        
        
        UserSession.AddMessage(res.Messages);
        if (res.Result == ActionResult.Done)
            Response.Redirect(GetRouteUrl("agent-default", null));
     
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("agent-default", null);
    }
}