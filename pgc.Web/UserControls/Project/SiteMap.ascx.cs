using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Business.General;

public partial class UserControls_Project_SiteMap : BaseUserControl
{
    public SiteMapBusiness SiteMap = new SiteMapBusiness();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}