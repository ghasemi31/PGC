using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model.Patterns;

public partial class Pages_Admin_BranchPayment_Detail : BaseDetailControl<BranchPayment>
{
    public BranchPayment Pay = new BranchPayment();

    public override BranchPayment GetEntity(BranchPayment Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchPayment();

        Data.Amount = nmrPrice.GetNumber<long>();
        Data.OfflineReceiptPersianDate = pdpPayDate.PersianDate;
        Data.OfflineReceiptType = (int)lkpRcieptType.GetSelectedValue<BranchOfflineReceiptType>();
        Data.OfflineReceiptLiquidator = txtRecieptName.Text;
        Data.OfflineReceiptNumber = txtRecieptNumber.Text;
        Data.OfflineBankAccount_ID = lkpBankAccount.GetSelectedValue<long>();
        BankAccount bank=new BankAccountBusiness().Retrieve(Data.OfflineBankAccount_ID.Value);
        Data.OfflineBankAccountDescription = bank.Description;
        Data.OfflineBankAccountTitle = bank.Title;
        Data.OfflineDescription = txtOfflineDesc.Text;

        return Data;
    }

    public override void SetEntity(BranchPayment Data, ManagementPageMode Mode)
    {
        Pay = Data;

        nmrPrice.SetNumber(Data.Amount);
        pdpPayDate.PersianDate = Data.OfflineReceiptPersianDate;
        lkpRcieptType.SetSelectedValue(Data.OfflineReceiptType);
        txtRecieptName.Text = Data.OfflineReceiptLiquidator;
        txtRecieptNumber.Text = Data.OfflineReceiptNumber;
        lkpBankAccount.SetSelectedValue(Data.OfflineBankAccount_ID);
        txtOfflineDesc.Text = Data.OfflineDescription;
    }

    

    protected void Con_Click(object sender, EventArgs e)
    {
        OnSave(sender, e);

        var _Page = this.Page as BaseManagementPage<BranchPaymentBusiness, BranchPayment, BranchPaymentPattern, pgcEntities>;
        var msgs=_Page.Business.ConfirmPayment(_Page.SelectedID).Messages;

        UserSession.CurrentMessages.Clear();
        foreach (var item in msgs)
        {
            UserSession.AddMessage(item);
        }
        //base.EndMode(ManagementPageMode.Search);
        //_Page.Mode = ManagementPageMode.Search;
        _Page.ListControl.Grid.DataBind();

    }
    protected void UnCon_Click(object sender, EventArgs e)
    {
        var _Page = this.Page as BaseManagementPage<BranchPaymentBusiness, BranchPayment, BranchPaymentPattern, pgcEntities>;
        var msgs = _Page.Business.UnConfirmPayment(_Page.SelectedID).Messages;
        foreach (var item in msgs)
        {
            UserSession.AddMessage(item);
        }
        base.EndMode(ManagementPageMode.Search);
        _Page.Mode = ManagementPageMode.Search;
        _Page.ListControl.Grid.DataBind();
    }
   
    public override void EndMode(ManagementPageMode Mode)
    {
        base.EndMode(Mode);
        var _Page = this.Page as BaseManagementPage<BranchPaymentBusiness, BranchPayment, BranchPaymentPattern, pgcEntities>;
        _Page.ListControl.Grid.DataBind();
    }
}