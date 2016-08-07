using kFrameWork.Enums;
using kFrameWork.UI;
using pgc.Business;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_SocialIcon_Detail : BaseDetailControl<SocialIcon>
{
    public override SocialIcon GetEntity(SocialIcon Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new SocialIcon();

        Data.Title = txtTitle.Text;
        Data.Url = txtUrl.Text;
        Data.DispOrder = Convert.ToInt32(txtDispOrder.Text);
        Data.IsVisible = Convert.ToBoolean(chkIsVisible.Checked);
        Data.Icon = icpSocial.GetValue();

        return Data;
    }

    public override void SetEntity(SocialIcon Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtUrl.Text = Data.Url;
        txtDispOrder.Text = Data.DispOrder.ToString();
        icpSocial.SetValue(Data.Icon);
        chkIsVisible.Checked = Data.IsVisible;


    }
}