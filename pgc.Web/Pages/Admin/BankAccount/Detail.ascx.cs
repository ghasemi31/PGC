using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Business;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_BankAccount_Detail : BaseDetailControl<BankAccount>
{
    public override BankAccount GetEntity(BankAccount Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BankAccount();

        Data.Description = txtDescription.Text;
        Data.Title= txtTitleInvoiceSingleService.Text;
        Data.Status = (int)lkpStatus.GetSelectedValue<OfflineBankAccountStatus>();
        return Data;
    }

    public override void SetEntity(BankAccount Data, ManagementPageMode Mode)
    {
        txtDescription.Text = Data.Description;
        txtTitleInvoiceSingleService.Text = Data.Title;
        lkpStatus.SetSelectedValue(Data.Status);
    }
}