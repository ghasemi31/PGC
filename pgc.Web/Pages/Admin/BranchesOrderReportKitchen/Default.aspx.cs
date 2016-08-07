using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_BranchesOrderReportKitchen_Default : BasePage
{
    public BranchesOrderReportBusiness business = new BranchesOrderReportBusiness();
    public IQueryable<BranchOrderDetail> order;
    public List<string> header;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BranchesOrderReportPattern pattern = new BranchesOrderReportPattern();
            pattern = null;
            order = business.RetriveOrder(pattern);
            Session["BranchesOrderReportPattern"] = pattern;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BranchesOrderReportPattern pattern = new BranchesOrderReportPattern()
        {
            Title = txtTitle.Text,
            Branch_ID = lkpBranch.GetSelectedValue<long>(),
            PersianDate = pdrDeliverDate.DateRange,
            Status = lkpOrderStatus.GetSelectedValue<BranchOrderStatus>()
        };
        Session["BranchesOrderReportPattern"] = pattern;
        order = business.RetriveOrder(pattern);

    }
    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        BranchesOrderReportPattern pattern = new BranchesOrderReportPattern();
        pattern = null;
        order = business.RetriveOrder(pattern);
        Session["BranchesOrderReportPattern"] = pattern;
    }


}