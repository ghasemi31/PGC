using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using kFrameWork.Util;
using pgc.Model;
using pgc.Model.Patterns;
using pgc.Business;

public partial class Pages_Admin_BranchOrderedTitleNoMoney_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }
    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.CssClass = "footerRow";

            
        //    e.Row.Cells[6].Text = "مجموع کل";

        //    var _Page =this.Page as BaseManagementPage<BranchOrderedTitleBusiness, BranchOrderTitle, BranchOrderedTitlePattern, pgcEntities>;

        //    e.Row.Cells[7].Text = UIUtil.GetCommaSeparatedOf(_Page.Business.TotalPrice(_Page.SearchControl.Pattern)) + " ریال";
                
        //}
    }
}