using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_User_UserProfile : BasePage
{
    UserBusiness userbusiness = new UserBusiness();
    public CircularBusiness business = new CircularBusiness();
    public User user; 
    protected void Page_Load(object sender, EventArgs e)
    {
        user = userbusiness.RetriveUser(UserSession.UserID);

    }
}