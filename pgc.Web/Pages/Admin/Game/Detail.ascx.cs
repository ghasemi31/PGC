using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_Game_Detail : BaseDetailControl<Game>
{
    public override Game GetEntity(Game Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Game();

        Data.Title = txtTitle.Text;
        Data.FirstPresent = txtFirstPresent.Text;
        Data.SecondPresent = txtSecondPresent.Text;
        Data.ThirdPresent = txtThirdPresent.Text;
        Data.Cost = ntbCost.GetNumber<long>();
        Data.GamerCount = ntbGamerCnt.GetNumber<int>();

        Data.UrlKey = txtUrlKey.Text;
        Data.Laws = txtLaws.Text;
        
        //if (managerId > 0)
        //    Data.Manager_ID = managerId;
        //else
        //    Data.Manager_ID = null;

        Data.Type_Enum = lkcGameType.GetSelectedValue<int>();

        Data.DispOrder = txtDispOrder.GetNumber<int>();
        Data.ImagePath = fupPic.FilePath;
        Data.LogoPath = fupLogo.FilePath;

        return Data;
    }

    public override void SetEntity(Game Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtFirstPresent.Text = Data.FirstPresent;
        txtSecondPresent.Text = Data.SecondPresent;
        txtThirdPresent.Text = Data.ThirdPresent;
        ntbCost.SetNumber(Data.Cost);
        ntbGamerCnt.SetNumber(Data.GamerCount);
        txtLaws.Text = Data.Laws;
        lkcGameType.SetSelectedValue(Data.Type_Enum);
        //lkpManager.SetSelectedValue(Data.Manager_ID);

        txtUrlKey.Text = Data.UrlKey;

        txtDispOrder.SetNumber(Data.DispOrder);
        fupPic.FilePath = Data.ImagePath;
        fupLogo.FilePath = Data.LogoPath;
    }
}