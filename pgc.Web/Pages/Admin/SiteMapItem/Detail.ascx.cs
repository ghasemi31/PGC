using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_SiteMapItem_Detail : BaseDetailControl<SiteMapItem>
{
    public override SiteMapItem GetEntity(SiteMapItem Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new SiteMapItem();
        Data.SiteMapCat_ID = lkpSiteMapCat.GetSelectedValue<long>();
        Data.Title = txtTitle.Text;
        Data.NavigateUrl = txtNavigateUrl.Text;
        Data.DispOrder = txtDispOrder.GetNumber<int>();
        Data.IsVisible = chkIsVisible.Checked;
        Data.IsBlank = Convert.ToBoolean(chkIsBlank.Checked);
        return Data;
    }

    public override void SetEntity(SiteMapItem Data, ManagementPageMode Mode)
    {
        lkpSiteMapCat.SetSelectedValue(Data.SiteMapCat_ID);
        txtTitle.Text = Data.Title;
        txtNavigateUrl.Text = Data.NavigateUrl;
        txtDispOrder.SetNumber(Data.DispOrder);
        chkIsVisible.Checked = Data.IsVisible;
        chkIsBlank.Checked = Data.IsBlank;
    }
}