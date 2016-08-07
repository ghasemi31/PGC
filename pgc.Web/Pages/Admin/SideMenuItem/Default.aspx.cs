using kFrameWork.UI;
using pgc.Business;
using pgc.Business.EntityManagementBusiness;
using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_SideMenuItem_Default : BaseManagementPage<SideMenuItemBusiness, SideMenuItem, pgc.Model.Patterns.SideMenuItemPattern, pgcEntities>
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.DetailControl = pnlDetail;
        base.ListControl = pnlList;
        base.SearchControl = pnlSearch;

        base.Business = new SideMenuItemBusiness();
    }
}