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

public partial class Pages_Admin_RandomImage_Detail : BaseDetailControl<MainSlider>
{
    private RandomImageBusiness business = new RandomImageBusiness();
    public override MainSlider GetEntity(MainSlider Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new MainSlider();
        Data.Title = txtTitle.Text;
        Data.ImgPath = fupPic.FilePath;
        Data.DispOrder = Convert.ToInt32(txtDispOrder.Text);
        Data.IsVisible = Convert.ToBoolean(chkIsVisible.Checked);
        return Data;
    }

    public override void SetEntity(MainSlider Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtDispOrder.Text = Data.DispOrder.ToString();
        chkIsVisible.Checked = Data.IsVisible;
        fupPic.FilePath = Data.ImgPath;
    }
}