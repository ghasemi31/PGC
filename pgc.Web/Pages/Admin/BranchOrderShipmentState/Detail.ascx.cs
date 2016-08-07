using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Business;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_BranchOrderShipmentState_Detail : BaseDetailControl<BranchOrderShipmentState>
{
    public override BranchOrderShipmentState GetEntity(BranchOrderShipmentState Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchOrderShipmentState();

        Data.Title = txtTitle.Text;

        return Data;
    }

    public override void SetEntity(BranchOrderShipmentState Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
    }
}