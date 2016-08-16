using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model.Patterns;

public partial class Pages_Admin_GameCenter_Detail : BaseDetailControl<GameCenter>
{
    public GameCenterBusiness business = new GameCenterBusiness();
    public override GameCenter GetEntity(GameCenter Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new GameCenter();

   
        Data.TItle = txtTitle.Text;
        Data.City_ID = lkcCity.GetSelectedValue<long>();
        Data.Description = txtDesc.Text;
      

        return Data;
    }
    
    public override void SetEntity(GameCenter Data, ManagementPageMode Mode)
    {

        lkcProvince.SetSelectedValue(Data.City.Province_ID);
        lkcCity.SetSelectedValue(Data.City_ID);
        txtDesc.Text = Data.Description;
        txtTitle.Text = Data.TItle;
   
    }

    public override void BeginMode(ManagementPageMode Mode)
    {
        base.BeginMode(Mode);
       
    }

}