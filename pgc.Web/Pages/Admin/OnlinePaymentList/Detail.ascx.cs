using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_OnlinePaymentList_Detail : BaseDetailControl<OnlinePaymentList>
{
    public override OnlinePaymentList GetEntity(OnlinePaymentList Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new OnlinePaymentList();


        return Data;
    }

    public override void SetEntity(OnlinePaymentList Data, ManagementPageMode Mode)
    {

    }
}