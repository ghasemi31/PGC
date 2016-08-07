using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_PollChoise_Detail : BaseDetailControl<PollChoise>
{
    public override PollChoise GetEntity(PollChoise Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new PollChoise();
        Data.Poll_ID = lkpPoll.GetSelectedValue<long>();
        Data.Title = txtTitle.Text;
        Data.Description = txtDesc.Text;
        return Data;
    }

    public override void SetEntity(PollChoise Data, ManagementPageMode Mode)
    {
        lkpPoll.SetSelectedValue(Data.Poll_ID);
        txtDesc.Text = Data.Description;
        txtTitle.Text = Data.Title;
        
    }
}