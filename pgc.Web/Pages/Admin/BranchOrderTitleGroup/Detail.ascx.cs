using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;

public partial class Pages_Admin_BranchOrderTitleGroup_Detail : BaseDetailControl<BranchOrderTitleGroup>
{
    public override BranchOrderTitleGroup GetEntity(BranchOrderTitleGroup Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchOrderTitleGroup();

        Data.Title = txtTitle.Text;
        Data.DisplayOrder = txtDiaplyOrder.GetNumber<int>();
        return Data;
    }

    public override void SetEntity(BranchOrderTitleGroup Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtDiaplyOrder.SetNumber(Data.DisplayOrder);
    }
}