using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Support_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }
}