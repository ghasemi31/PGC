using kFrameWork.Enums;
using kFrameWork.UI;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_SideMenuCat_Detail : BaseDetailControl<SideMenuCat>
{
    public override SideMenuCat GetEntity(SideMenuCat Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new SideMenuCat();
        Data.Title = txtTitle.Text;
        Data.NavigationUrl = txtNavigationUrl.Text;
        Data.DisplayOrder = txtDispOrder.GetNumber<int>();
        Data.IsVisible = chkIsVisible.Checked;
        Data.IsBlank = chkIsBlank.Checked;
        return Data;
    }

    public override void SetEntity(SideMenuCat Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtNavigationUrl.Text = Data.NavigationUrl;
        txtDispOrder.SetNumber(Data.DisplayOrder);
        chkIsVisible.Checked = Data.IsVisible;
        chkIsBlank.Checked = Data.IsBlank;
    }
}