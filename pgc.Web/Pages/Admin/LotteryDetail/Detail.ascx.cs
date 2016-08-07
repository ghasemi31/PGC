using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_LotteryDetail_Detail : BaseDetailControl<LotteryDetail>
{
    public override LotteryDetail GetEntity(LotteryDetail Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new LotteryDetail();
        Data.Code = txtCode.GetNumber<int>();
        Data.Description = txtDesc.Text;
        Data.Email = txtEmail.Text;
        Data.FName = txtFName.Text;
        Data.LName = txtLName.Text;
        Data.LotteryID = lkpLottery.GetSelectedValue<long>();
        
        return Data;
    }

    public override void SetEntity(LotteryDetail Data, ManagementPageMode Mode)
    {
        lkpLottery.SetSelectedValue(Data.LotteryID);
        txtCode.SetNumber(Data.Code);
        txtDesc.Text = Data.Description;
        txtEmail.Text = Data.Email;
        txtFName.Text = Data.FName;
        txtLName.Text = Data.LName;
        
    }
}