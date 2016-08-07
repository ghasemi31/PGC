using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_HeadingNews_Detail : BaseDetailControl<Heading>
{
    public override Heading GetEntity(Heading Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Heading();

        Data.Title = txtTitle.Text;
        Data.NavigateUrl = txtNavigateUrl.Text;
        Data.DispOrder = txtDispOrder.GetNumber<int>();
        return Data;
    }

    public override void SetEntity(Heading Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtNavigateUrl.Text = Data.NavigateUrl;
        txtDispOrder.SetNumber(Data.DispOrder);
    }
}