using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_Poll_Detail : BaseDetailControl<Poll>
{
    public override Poll GetEntity(Poll Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Poll();

        Data.Title = txtTitle.Text;
        Data.Description = txtDesc.Text;
        Data.PollPersianDate = pdpPollPersianDate.PersianDate;
        Data.IsActive = Convert.ToBoolean(chkIsActive.Checked);
        return Data;
    }

    public override void SetEntity(Poll Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtDesc.Text = Data.Description;
        pdpPollPersianDate.PersianDate = Data.PollPersianDate;
        chkIsActive.Checked = Data.IsActive;
        
    }
}