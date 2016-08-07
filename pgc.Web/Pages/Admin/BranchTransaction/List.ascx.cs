using System;
using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Business;
using pgc.Model;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI;

public partial class Pages_Admin_BranchTransaction_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }
    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        int cellCredit = 3;
        int cellDebt = 4;
        int cellBranch = 3;
        int cellTranType = 4;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[cellBranch].Text = DataBinder.Eval(e.Row.DataItem, "Title").ToString().Replace("شعبه", "");
            e.Row.Cells[cellTranType].Text = EnumUtil.GetEnumElementPersianTitle((BranchTransactionType)DataBinder.Eval(e.Row.DataItem, "TransactionType")).Replace("شعبه", "");

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "مجموع کل :";
            e.Row.Cells[1].ColumnSpan = 3;
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