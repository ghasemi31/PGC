using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Agent_OnlinePayment_Detail : BaseDetailControl<OnlinePayment>
{
    public override OnlinePayment GetEntity(OnlinePayment Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new OnlinePayment();


        return Data;
    }

    public override void SetEntity(OnlinePayment Data, ManagementPageMode Mode)
    {
        
    }
}