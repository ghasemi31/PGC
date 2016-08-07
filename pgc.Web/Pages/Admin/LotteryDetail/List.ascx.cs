using System;
using kFrameWork.UI;

public partial class Pages_Admin_LotteryDetail_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }

    protected void btnLottery_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("admin-lottery");
    }
}