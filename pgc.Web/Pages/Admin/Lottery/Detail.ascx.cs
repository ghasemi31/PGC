using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using kFrameWork.Util;

public partial class Pages_Admin_Lottery_Detail : BaseDetailControl<Lottery>
{
    public override Lottery GetEntity(Lottery Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Lottery();
      
        
        Data.Status = lkcStatus.GetSelectedValue<int>();
        Data.Description = txtDesc.Text;
        Data.Title = txtTitle.Text;
        Data.RegDate = DateTime.Now;
        Data.RegPersianDate = DateUtil.GetPersianDateShortString(DateTime.Now);

        return Data;
    }

    public override void SetEntity(Lottery Data, ManagementPageMode Mode)
    {
        lkcStatus.SetSelectedValue(Data.Status);
        txtTitle.Text = Data.Title;
        txtDesc.Text = Data.Description;
      
    }  
}