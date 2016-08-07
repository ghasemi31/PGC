using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using pgc.Model;
using pgc.Model.Patterns;
using pgc.Business;
using System.Linq;
using kFrameWork.Util;

public partial class Pages_Agent_BranchTransaction_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }
    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        int cellCredit = 4;
        int cellDebt = 5;

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "مجموع کل :";

            e.Row.Style.Add("font-weight", "bold");

            var _page = this.Page as BaseManagementPage<BranchTransactionBusiness, BranchTransaction, BranchTransactionPattern, pgcEntities>;

            var result = _page.Business.Search_SelectPrint(_page.SearchControl.Pattern);

            long total = result.Sum(f => f.BranchCredit - f.BranchDebt);

            if (total > 0)
            {
                e.Row.Cells[cellCredit].Text = UIUtil.GetCommaSeparatedOf(total) + " ریال";
                e.Row.Cells[cellDebt].Text = "---- ریال"; 
            }
            else
            {
                e.Row.Cells[cellDebt].Text = UIUtil.GetCommaSeparatedOf(Math.Abs(total)) + " ریال";
                e.Row.Cells[cellCredit].Text = "---- ریال";
            }

        }
    }
}