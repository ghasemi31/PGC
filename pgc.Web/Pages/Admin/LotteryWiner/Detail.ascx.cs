using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Patterns;
using pgc.Business;

public partial class Pages_Admin_LotteryWiner_Winer : BaseDetailControl<LotteryWiner>
{
    public override LotteryWiner GetEntity(LotteryWiner Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new LotteryWiner();

        Data.Rank = txtRank.GetNumber<int>();
        Data.FName = txtFName.Text;
        Data.LName = txtLName.Text;
        Data.LotteryID = lkpLottery.GetSelectedValue<long>();
        Data.Description = txtDesc.Text;       
        return Data;
    }

    public override void SetEntity(LotteryWiner Data, ManagementPageMode Mode)
    {
        lkpLottery.SetSelectedValue(Data.LotteryID);
        txtRank.SetNumber(Data.Rank);
        txtFName.Text = Data.FName;
        txtLName.Text = Data.LName;
        txtDesc.Text = Data.Description;
    }


}