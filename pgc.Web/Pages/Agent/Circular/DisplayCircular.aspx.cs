using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Agent_Circular_DisplayCircular : BasePage
{
    public Circular circular;
    public CircularBusiness business = new CircularBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {
        long id = this.GetQueryStringValue<long>(QueryStringKeys.id);
        circular = business.RetriveCircular(id);
    }
}