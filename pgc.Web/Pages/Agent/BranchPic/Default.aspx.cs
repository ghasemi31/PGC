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

public partial class Pages_Agent_BranchPic_Default : BaseManagementPage<AgentBranchPicBusiness, BranchPic, AgentBranchPicPattern, pgcEntities>
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.DetailControl = pnlDetail;
        base.ListControl = pnlList;
        base.SearchControl = pnlSearch;

        base.Business = new AgentBranchPicBusiness();
        //BranchBusiness business = new BranchBusiness();

    }
}