using kFrameWork.Enums;
using kFrameWork.UI;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_MainMenu_Detail : BaseDetailControl<MainMenu>
{
    public override MainMenu GetEntity(MainMenu Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new MainMenu();
        Data.Title = txtTitle.Text;
        Data.NavigationUrl = txtUrl.Text;
        Data.DisplayOrder = Convert.ToInt32(txtDispOrder.Text);
        //Data.HtmlCode = txtHtml.Text;
        Data.IsHtml = chkHtml.Checked;
        Data.DispInHome = chkHomPage.Checked;
        Data.DispInOtherPage = chkOtherPage.Checked;
        Data.IsBlank = chkIsBlank.Checked;
        return Data;
    }

    public override void SetEntity(MainMenu Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtUrl.Text = Data.NavigationUrl;
        txtDispOrder.Text = Data.DisplayOrder.ToString();
        //txtHtml.Text = Data.HtmlCode;
        chkHtml.Checked = Data.IsHtml;
        chkHomPage.Checked = Data.DispInHome;
        chkOtherPage.Checked = Data.DispInOtherPage;
        chkIsBlank.Checked = Data.IsBlank;
    }
}