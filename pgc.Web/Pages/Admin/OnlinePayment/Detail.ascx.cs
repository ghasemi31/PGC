using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_Payment_Detail : BaseDetailControl<Payment>
{
    public override Payment GetEntity(Payment Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Payment();


        return Data;
    }

    public override void SetEntity(Payment Data, ManagementPageMode Mode)
    {
        
    }
}