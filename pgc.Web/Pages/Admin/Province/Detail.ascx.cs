using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_Province_Detail : BaseDetailControl<Province>
{
    public override Province GetEntity(Province Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Province();

        Data.Title = txtTitle.Text;
        
        return Data;
    }

    public override void SetEntity(Province Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
    }
}