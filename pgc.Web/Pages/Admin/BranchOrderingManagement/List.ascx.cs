using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using pgc.Model;
using pgc.Model.Patterns;
using pgc.Business;
using kFrameWork.Util;
using System.Web.UI;

public partial class Pages_Admin_BranchCredit_List : BaseListControl
{
    //string emptyString = "-------";
    //BaseManagementPage<BranchCreditBusiness, BranchTransaction, BranchCreditPattern, pgcEntities> _Page = new BaseManagementPage<BranchCreditBusiness, BranchTransaction, BranchCreditPattern, pgcEntities>();

    //public void Page_Load()
    //{
    //    _Page = this.Page as BaseManagementPage<BranchCreditBusiness, BranchTransaction, BranchCreditPattern, pgcEntities>;
    //}

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }
    //protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    //{
    //    int cellTransactionList = 8;
    //    int cellBranchCredit = 4;
    //    int cellBranchDebt = 5;
    //    int cellMinimumCredit = 6;

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        HyperLink h = new HyperLink();
    //        h.CssClass = "hbtn";
    //        h.NavigateUrl = GetRouteUrl("admin-branchtransaction", null) + "?fid=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString();
    //        h.Text = "لیست تراکنش ها";

    //        e.Row.Cells[cellTransactionList].Controls.Add(h);

    //        e.Row.Cells[cellBranchCredit].Text = (e.Row.Cells[cellBranchCredit].Text == "0") ? emptyString : e.Row.Cells[cellBranchCredit].Text;
    //        e.Row.Cells[cellBranchDebt].Text = (e.Row.Cells[cellBranchDebt].Text == "0") ? emptyString : e.Row.Cells[cellBranchDebt].Text;

    //        long MinimumCredit = ((BranchCredit)e.Row.DataItem).MinimumCredit;
    //        if (MinimumCredit < 0)
    //            e.Row.Cells[cellMinimumCredit].Text = UIUtil.GetCommaSeparatedOf(Math.Abs(MinimumCredit)) + "- ریال ";
    //        else
    //            e.Row.Cells[cellMinimumCredit].Text = UIUtil.GetCommaSeparatedOf(MinimumCredit) + " ریال ";
    //    }
    //    else if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        e.Row.Cells[3].Text = "جمع کل اعتبار شعب";
    //        long total = _Page.Business.Search_Select(_Page.SearchControl.Pattern);
    //        if (total < 0)
    //        {
    //            e.Row.Cells[cellBranchDebt].Text = UIUtil.GetCommaSeparatedOf(Math.Abs(total)) + " ریال";
    //            e.Row.Cells[cellBranchDebt].Style.Add("padding-top", "10px");

    //            e.Row.Cells[cellBranchCredit].Text = emptyString;
    //        }
    //        else
    //        {
    //            e.Row.Cells[cellBranchCredit].Text = UIUtil.GetCommaSeparatedOf(total) + " ریال";
    //            e.Row.Cells[cellBranchCredit].Style.Add("padding-top", "10px");

    //            e.Row.Cells[cellBranchDebt].Text = emptyString;
    //        }
    //        e.Row.Font.Bold = true;
    //        e.Row.Font.Size = 8;
    //    }
    //}
    //protected void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "ChangeRow")
    //    {
    //        _Page.SelectedID = long.Parse(Grid.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
    //        _Page.Mode = kFrameWork.Enums.ManagementPageMode.Edit;
    //        _Page.DetailControl.BeginMode(kFrameWork.Enums.ManagementPageMode.Edit);
    //    }
    //    else
    //        base.Grid_RowCommand(sender, e);
    //}
}