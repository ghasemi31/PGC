using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Pages_Admin_Game_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }


    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        int cellActive = 7;
        int cellDeActive = 8;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if ((bool)DataBinder.Eval(e.Row.DataItem, "IsActive"))
            //{
            //    e.Row.Cells[cellActive].Visible = false;
            //    e.Row.Cells[cellDeActive].Visible = true;
            //    e.Row.Cells[cellDeActive].Style.Add("text-align", "center");
            //}
            //else
            //{
            //    e.Row.Cells[cellActive].Visible = true;
            //    e.Row.Cells[cellDeActive].Visible = false;
            //    e.Row.Cells[cellActive].Style.Add("text-align", "center");
            //}
        }
    }
}