using kFrameWork.Model;
using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Business.General;
using pgc.Model;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoogleReCaptcha;
using System.Runtime.Serialization;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web.Services;
public partial class Pages_Guest_Branch : BasePage
{
    public BranchBusiness business = new BranchBusiness();

    public Branch branch;
    public List<Comment> comment;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasValidQueryString_Routed<string>(QueryStringKeys.urlkey))
        {
            Server.Transfer("~/Pages/Guest/404.aspx");
        }

        branch = business.RetriveBranch(GetQueryStringValue_Routed<string>(QueryStringKeys.urlkey));

        if (branch == null)
            Server.Transfer("~/Pages/Guest/404.aspx");
        comment = business.GetProductComment(branch);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Captcha4.UserValidated)
        {
            Comment branchComment = new Comment();
            branchComment.SenderName = txtFullName.Text;
            branchComment.SenderEmail = txtEmail.Text;
            branchComment.Body = txtBody.Value;
            branchComment.Date = DateTime.Now;
            branchComment.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
            branchComment.Branch_ID = branch.ID;
            branchComment.IsDisplay = kFrameWork.Business.OptionBusiness.GetBoolean(OptionKey.IsRead);
            OperationResult result = new OperationResult();
            CommentBusiness business = new CommentBusiness();
            result = business.AddComment(branchComment);
            UserSession.AddMessage(result.Messages);
            if (result.Result == ActionResult.Done)
            {
                txtFullName.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtBody.Value = string.Empty;
                txtCaptcha.Text = string.Empty;
            }
        }
    }
    protected void btnContactSave_Click(object sender, EventArgs e)
    {
       if (Captcha5.UserValidated)
        {
            BranchContact bContact = new BranchContact();
            bContact.FullName = txtContactFullName.Text;
            bContact.Email = txtContactEmail.Text;
            bContact.Body = txtContactBody.Value;
            bContact.Branch_ID = branch.ID;
            bContact.BranchTitle = branch.Title;
            OperationResult result = new OperationResult();
            BranchContactBusiness business = new BranchContactBusiness();
            result = business.AddBranchContact(bContact);
            UserSession.AddMessage(result.Messages);
            if (result.Result == ActionResult.Done)
            {
                txtContactFullName.Text = string.Empty;
                txtContactEmail.Text = string.Empty;
                txtBody.Value = string.Empty;
                txtCaptchaContact.Text = string.Empty;
            }
        }
    }


    protected void ValidateCaptcha4(object sender, ServerValidateEventArgs e)
    {
        Captcha4.ValidateCaptcha(txtCaptcha.Text.Trim());
        e.IsValid = Captcha4.UserValidated;
    }
    protected void ValidateCaptcha5(object sender, ServerValidateEventArgs e)
    {
        Captcha5.ValidateCaptcha(txtCaptchaContact.Text.Trim());
        e.IsValid = Captcha5.UserValidated;
    }

    [WebMethod]
    public static int Like(string mode, string commentID)
    {
        ProductBusiness likeBusiness = new ProductBusiness();
        HttpCookie userInfo = new HttpCookie("userInfo");
        HttpCookie reqCookies = HttpContext.Current.Request.Cookies["userInfo"];
        if (reqCookies["info"].Contains(mode + "-" + commentID))
        {
            return 1;
        }
        else
        {
            string str = "";
            if (reqCookies != null)
            {
                str = reqCookies.Value + mode + "-" + commentID;
            }
            else
            {
                str = mode + "-" + commentID;
            }

            userInfo["info"] = str;
            userInfo.Expires = DateTime.Now.AddDays(365);
            HttpContext.Current.Response.Cookies.Add(userInfo);
            if (mode == "1")
            {
                likeBusiness.Like(1, Convert.ToInt64(commentID));
            }
            if (mode == "2")
            {
                likeBusiness.Like(-1, Convert.ToInt64(commentID));
            }
            return 2;
        }

    }
       
}
