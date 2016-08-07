using kFrameWork.Enums;
using kFrameWork.UI;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_SideMenuItem_Detail : BaseDetailControl<SideMenuItem>
{
    public override SideMenuItem GetEntity(SideMenuItem Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new SideMenuItem();
        Data.SideMenuCat_ID = lkpSideMenuCat.GetSelectedValue<long>();
        Data.Title = txtTitle.Text;
        Data.NavigateUrl = txtNavigateUrl.Text;
        Data.DispOrder = txtDispOrder.GetNumber<int>();
        Data.IsVisible = chkIsVisible.Checked;
        Data.IsBlank = Convert.ToBoolean(chkIsBlank.Checked);
        return Data;
    }

    public override void SetEntity(SideMenuItem Data, ManagementPageMode Mode)
    {
        lkpSideMenuCat.SetSelectedValue(Data.SideMenuCat_ID);
        txtTitle.Text = Data.Title;
        txtNavigateUrl.Text = Data.NavigateUrl;
        txtDispOrder.SetNumber(Data.DispOrder);
        chkIsVisible.Checked = Data.IsVisible;
        chkIsBlank.Checked = Data.IsBlank;
    }
}