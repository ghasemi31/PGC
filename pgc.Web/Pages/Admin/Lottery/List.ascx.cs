using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
using pgc.Business;
using pgc.Model;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_Lottery_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;

        
    }
    protected override void OnPreRender(EventArgs e)
    {
        
        base.Grid.DataBind();

        base.OnPreRender(e);
    }
    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            long ID = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "ID"));
            LotteryBusiness business = (this.Page as BaseManagementPage<LotteryBusiness, Lottery, LotteryPattern, pgcEntities>).Business;
            Lottery lottery= business.RetriveLottery(ID);
            if (lottery.Status == (int)LotteryStatus.flow)
            {
                e.Row.Cells[6].Text = "";
               
            }
            
        }
    }
}