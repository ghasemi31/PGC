using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Guest_Default : BasePage
{
    public DefaultBusiness DBusiness = new DefaultBusiness();
    public IQueryable<Advertisment> adv;
    protected void Page_Load(object sender, EventArgs e)
    {
        adv = DBusiness.GetAdvertisment();
    }
}