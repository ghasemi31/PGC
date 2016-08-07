using System;
using kFrameWork.UI;

public partial class Pages_Admin_SiteMapCat_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }
}