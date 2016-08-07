using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using kFrameWork.Util;
using pgc.Model.Enums;


public partial class Pages_Guest_ReportDetail :BasePage
{
    public ReportBusiness business = new ReportBusiness();

    public Report report;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasValidQueryString_Routed<long>(QueryStringKeys.id))
            Server.Transfer("~/Pages/Guest/404.aspx");

        report = business.RetriveReport(GetQueryStringValue_Routed<long>(QueryStringKeys.id));

        if (report == null)
            Server.Transfer("~/Pages/Guest/404.aspx");
    }
}