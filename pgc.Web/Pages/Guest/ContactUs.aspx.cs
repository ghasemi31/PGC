using kFrameWork.Model;
using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Business.General;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Guest_ContactUs : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Feedback feed = new Feedback();
        feed.FullName = txtFullName.Text;
        feed.Email = txtEmail.Text;
        feed.Mobile = txtMobile.Text;
        feed.Body = txtBody.Value;
        feed.GameManager = ConvertorUtil.ToInt32(ddlGameManager.SelectedValue);

        OperationResult result = new OperationResult();

        FeedbackBusiness business = new FeedbackBusiness();
        result = business.AddFeedback(feed);
        UserSession.AddMessage(result.Messages);
        if (result.Result == ActionResult.Done)
        {
            txtFullName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtBody.Value = string.Empty;
        }

    }
 
}