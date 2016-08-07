using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using System.Web;

public partial class Pages_Admin_BranchOrder_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }
    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        int deliverDateCell = 6;
        int lackCell = 10;
        int logCell = 9;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime orderedDateCell = DateUtil.GetEnglishDateTime(DataBinder.Eval(e.Row.DataItem, "OrderedPersianDate").ToString());
            e.Row.Cells[deliverDateCell].Text = DateUtil.GetPersianDateWithTime(orderedDateCell);

            if ((bool)DataBinder.Eval(e.Row.DataItem, "HasLack"))
            {
                HyperLink h1 = new HyperLink();
                h1.CssClass = "hbtn";
                h1.Target = "_blank";
                h1.NavigateUrl = ResolveClientUrl(GetRouteUrl("admin-branchlackorder", null) + "?fid=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString());
                h1.Text = "کسری";

                e.Row.Cells[lackCell].Controls.Add(h1);
            }


            HyperLink h2 = new HyperLink();
            h2.Target = "_blank";
            h2.CssClass = "hbtn";
            h2.NavigateUrl = ResolveClientUrl(GetRouteUrl("admin-branchfinancelog", null) + "?fid=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&logtype=BranchOrder");
            h2.Text = "تاریخچه عملیات";
            e.Row.Cells[logCell].Controls.Add(h2);
        }
    }
}