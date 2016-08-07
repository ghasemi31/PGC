using System;
using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model;
using pgc.Business;
using System.Web.UI;
using System.Web.UI.WebControls;
using pgc.Model.Enums;

public partial class Pages_Admin_BranchOrderShipmentState_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;       
    }    
}