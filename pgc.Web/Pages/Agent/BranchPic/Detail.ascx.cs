using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Agent_BranchPic_Detail : BaseDetailControl<BranchPic>
{
    public override BranchPic GetEntity(BranchPic Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchPic();

        Data.Branch_ID = (long)UserSession.User.Branch_ID;
        Data.ImagePath = fupBranchPic.FilePath;
        return Data;
    }

    public override void SetEntity(BranchPic Data, ManagementPageMode Mode)
    {
        
        lblBranch.Text = Data.Branch.Title;
      
        fupBranchPic.FilePath = Data.ImagePath;

    }
}