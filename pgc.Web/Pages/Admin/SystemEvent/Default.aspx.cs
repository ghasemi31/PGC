using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Business;
using pgc.Model;
using pgc.Model.Patterns;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public partial class Pages_Admin_SystemEvent_Default : BaseManagementPage<SystemEventBusiness, SystemEvent, SystemEventPattern, pgcEntities>
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        
        base.DetailControl = pnlDetail;
        base.ListControl = pnlList;
        base.SearchControl = pnlSearch;

        base.Business = new SystemEventBusiness();
    }
}