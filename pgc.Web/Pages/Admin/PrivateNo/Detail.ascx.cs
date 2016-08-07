using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;

public partial class Pages_Admin_PrivateNo_Detail : BaseDetailControl<PrivateNo>
{
    public override PrivateNo GetEntity(PrivateNo Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new PrivateNo();

        Data.Number = txtNumber.Text;
        Data.Status = (int)lkpStatus.GetSelectedValue<PrivateNoStatus>();
        
        return Data;
    }

    public override void SetEntity(PrivateNo Data, ManagementPageMode Mode)
    {
        txtNumber.Text = Data.Number;
        lkpStatus.SetSelectedValue(Data.Status);
    }
}