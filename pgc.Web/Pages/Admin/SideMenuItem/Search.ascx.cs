using kFrameWork.UI;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_SideMenuItem_Search : BaseSearchControl<SideMenuItemPattern>
{
    public override SideMenuItemPattern Pattern
    {
        get
        {
            return new SideMenuItemPattern()
            {
                Title = txtTitle.Text,
                SideMenuCat_ID = lkpSideMenuCat.GetSelectedValue<long>(),
            };
        }
        set
        {
            txtTitle.Text = value.Title;
            lkpSideMenuCat.SetSelectedValue(value.SideMenuCat_ID);
        }
    }
}