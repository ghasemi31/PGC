using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;

public partial class Pages_Admin_News_Confirm : BasePage
{
    public News news;
    NewsBusiness business = new NewsBusiness();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.HasValidQueryString<long>(QueryStringKeys.id))
        {
            long id = this.GetQueryStringValue<long>(QueryStringKeys.id);
            try
            {
                news = business.Retrieve(id);
                //if (!IsPostBack)
                //{
                //    lkpStatus.SetSelectedValue(news.Status);
                //}
            }
            catch (Exception ex)
            {
                Response.Redirect(GetRouteUrl("admin-news", null));
            }
        }
        else
            Response.Redirect(GetRouteUrl("admin-news", null));
    }

    protected void OnCancel(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("admin-news", null));
    }

    protected void OnSave(object sender, EventArgs e)
    {
        //news.Status = (int)lkpStatus.GetSelectedValue<NewsStatus>();
        UserSession.AddMessage(business.Update(news).Messages);
        Response.Redirect(GetRouteUrl("admin-news", null));
    }
}