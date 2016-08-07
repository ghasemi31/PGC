using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_BranchPic_Detail : BaseDetailControl<BranchPic>
{
    public override BranchPic GetEntity(BranchPic Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchPic();
        Data.Branch_ID = lkpBranch.GetSelectedValue<long>();
        
        Data.ImagePath = fupBranchPic.FilePath;
        return Data;
    }

    public override void SetEntity(BranchPic Data, ManagementPageMode Mode)
    {
        lkpBranch.SetSelectedValue(Data.Branch_ID);
        
        fupBranchPic.FilePath = Data.ImagePath;

    }
}