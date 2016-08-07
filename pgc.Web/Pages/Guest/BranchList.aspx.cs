using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Guest_BranchList : BasePage
{
    public BranchBusiness business = new BranchBusiness();
    public List<Branch> tehranBranch, iranBranch, worldBranch;
    protected void Page_Load(object sender, EventArgs e)
    {
        tehranBranch = business.GetTehranBranch();
        iranBranch = business.GetIranBranch();
        worldBranch = business.GetWorldBranch();
    }
}