using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model.Patterns;

public partial class Pages_Admin_BranchFinanceLog_Detail : BaseDetailControl<BranchFinanceLog>
{
    public override BranchFinanceLog GetEntity(BranchFinanceLog Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchFinanceLog();
       
        return Data;
    }

    public override void SetEntity(BranchFinanceLog Data, ManagementPageMode Mode)
    {
       
    }
}