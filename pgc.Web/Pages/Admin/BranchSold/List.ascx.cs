using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using pgc.Model;
using pgc.Model.Patterns;
using pgc.Business;
using kFrameWork.Util;
using System.Web.UI;

public partial class Pages_Admin_BranchSold_List : BaseListControl
{
    string emptyString = "-------";
    BaseManagementPage<BranchSoldBusiness, BranchTransaction, BranchSoldPattern, pgcEntities> _Page = new BaseManagementPage<BranchSoldBusiness, BranchTransaction, BranchSoldPattern, pgcEntities>();

    public void Page_Load()
    {
        _Page = this.Page as BaseManagementPage<BranchSoldBusiness, BranchTransaction, BranchSoldPattern, pgcEntities>;
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }
    
    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {      
        int cellMinimumCredit = 5;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            long MinimumCredit = ((BranchSold)e.Row.DataItem).MinimumCredit;
            if (MinimumCredit < 0)
                e.Row.Cells[cellMinimumCredit].Text = UIUtil.GetCommaSeparatedOf(Math.Abs(MinimumCredit)) + "- ریال ";
            else
                e.Row.Cells[cellMinimumCredit].Text = UIUtil.GetCommaSeparatedOf(MinimumCredit) + " ریال ";
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "جمع کل اعتبار شعب";
            long total = _Page.Business.Search_SelectTotalAmount(_Page.SearchControl.Pattern);
            e.Row.Cells[4].Text = UIUtil.GetCommaSeparatedOf(Math.Abs(total)) + " ریال";
            e.Row.Cells[4].Style.Add("padding-top", "10px");
            e.Row.Font.Bold = true;
            e.Row.Font.Size = 8;
        }
    }
    
    protected void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ChangeRow")
        {
            _Page.SelectedID = long.Parse(Grid.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
            _Page.Mode = kFrameWork.Enums.ManagementPageMode.Edit;
            _Page.DetailControl.BeginMode(kFrameWork.Enums.ManagementPageMode.Edit);
        }
        else
            base.Grid_RowCommand(sender, e);
    }

}