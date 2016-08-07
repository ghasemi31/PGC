using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_SiteMapCat_Detail : BaseDetailControl<SiteMapCat>
{
    public override SiteMapCat GetEntity(SiteMapCat Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new SiteMapCat();

        Data.Title = txtTitle.Text;
        Data.NavigateUrl = txtNavigateUrl.Text;
        Data.DispOrder = txtDispOrder.GetNumber<int>();
        Data.IsVisible = chkIsVisible.Checked;
        Data.IsBlank = Convert.ToBoolean(chkIsBlank.Checked);
        return Data;
    }

    public override void SetEntity(SiteMapCat Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtNavigateUrl.Text = Data.NavigateUrl;
        txtDispOrder.SetNumber(Data.DispOrder);
        chkIsVisible.Checked = Data.IsVisible;
        chkIsBlank.Checked = Data.IsBlank;
    }
}