using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model.Enums;
using kFrameWork.Util;
using kFrameWork.Model;
using pgc.Model.Enums;
using System.Web.Services;

public partial class Pages_Guest_Products : BasePage
{
    public ProductBusiness business = new ProductBusiness();

    public Product products;
    public List<Product> foodSlider;
    public List<Material> material;
    public List<Comment> comment;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasValidQueryString_Routed<long>(QueryStringKeys.id))
            Server.Transfer("~/Pages/Guest/404.aspx");

        products = business.RetriveProduct(GetQueryStringValue_Routed<long>(QueryStringKeys.id));
        foodSlider = business.GetFoodSlider();
        material = business.GetMaterial(products);
        comment = business.GetProductComment(products);
        if (products == null)
            Server.Transfer("~/Pages/Guest/404.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Captcha2.UserValidated)
        {
            Comment productComment = new Comment();
            productComment.SenderName = txtFullName.Text;
            productComment.SenderEmail = txtEmail.Text;
            productComment.Body = txtBody.Value;
            productComment.Date = DateTime.Now;
            productComment.PersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
            productComment.Product_ID = products.ID;
            productComment.IsDisplay = kFrameWork.Business.OptionBusiness.GetBoolean(OptionKey.IsRead);
            OperationResult result = new OperationResult();
            CommentBusiness business = new CommentBusiness();
            result = business.AddComment(productComment);
            UserSession.AddMessage(result.Messages);
            txtFullName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtBody.Value = string.Empty;
        }
    }

    protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
    {
        Captcha2.ValidateCaptcha(txtCaptcha.Text.Trim());
        e.IsValid = Captcha2.UserValidated;
    }

    [WebMethod]
    public static int Like(string mode, string commentID)
    {
        ProductBusiness likeBusiness = new ProductBusiness();
        HttpCookie userInfo = new HttpCookie("userInfo");
        HttpCookie reqCookies = HttpContext.Current.Request.Cookies["userInfo"];
        if (reqCookies != null && reqCookies["info"].Contains(mode + "-" + commentID))
        {
            return 1;
        }
        else
        {
            string str = "";
            if (reqCookies!=null)
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