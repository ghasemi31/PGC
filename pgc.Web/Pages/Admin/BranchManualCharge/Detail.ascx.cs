using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using kFrameWork.Util;
using pgc.Model.Enums;

public partial class Pages_Admin_BranchManualCharge_Detail : BaseDetailControl<BranchTransaction>
{
    public override BranchTransaction GetEntity(BranchTransaction Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchTransaction();

        Data.Description = txtDesc.Text;
        Data.Branch_ID = lkpBranch.GetSelectedValue<long>();
        //Data.RegDate = DateTime.Now;
        //Data.RegPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);
        //Data.TransactionType = (int)BranchTransactionType.ManualCharge;
        if (rdbIncredit.Checked)
        {
            Data.BranchCredit = txtPrice.GetNumber<long>();
            Data.BranchDebt = 0;
        }
        else if (rdbIndebt.Checked)
        {
            Data.BranchDebt = txtPrice.GetNumber<long>();
            Data.BranchCredit = 0;
        }
            
        

        return Data;
    }

    public override void SetEntity(BranchTransaction Data, ManagementPageMode Mode)
    {
        
    }

    public override bool Validate(ManagementPageMode Mode)
    {

        if (!rdbIndebt.Checked && !rdbIncredit.Checked)
        {
            UserSession.AddMessage(UserMessageKey.InvalidTransaction);
            return false;
        }

        return base.Validate(Mode);
    }
}