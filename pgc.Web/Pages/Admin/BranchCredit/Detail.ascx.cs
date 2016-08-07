using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Patterns;
using pgc.Business;

public partial class Pages_Admin_BranchCredit_Detail : BaseDetailControl<BranchTransaction>
{
    public BaseManagementPage<BranchCreditBusiness, BranchTransaction, BranchCreditPattern, pgcEntities> _Page = new BaseManagementPage<BranchCreditBusiness, BranchTransaction, BranchCreditPattern, pgcEntities>();

    public void Page_Load()
    {
        _Page = this.Page as BaseManagementPage<BranchCreditBusiness, BranchTransaction, BranchCreditPattern, pgcEntities>;
    }

    public override BranchTransaction GetEntity(BranchTransaction Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchTransaction();

        //Data.Title = txtTitle.Text;
        //Data.Province_ID = lkpProvince.GetSelectedValue<long>();

        return Data;
    }

    public override void SetEntity(BranchTransaction Data, ManagementPageMode Mode)
    {
    }

    public override void BeginMode(ManagementPageMode Mode)
    {
        base.BeginMode(Mode);
        //txtPrice.SetNumber(_Page.Business.GetCurrentMinimumCredit(_Page.SelectedID));
        txtPrice.Text = _Page.Business.GetMinimumCredit(_Page.SelectedID).ToString();
    }

    public override void EndMode(ManagementPageMode Mode)
    {
        base.EndMode(Mode);
        _Page.ListControl.Grid.DataBind();
    }
    protected void OnSave(object sender, EventArgs e)
    {
        //var msgs = _Page.Business.UpdateBranchMinimumCredit(_Page.SelectedID, txtPrice.GetNumber<long>()).Messages;
        var msgs = _Page.Business.UpdateBranchMinimumCredit(_Page.SelectedID, long.Parse(txtPrice.Text.Replace(",",""))).Messages;
        foreach (var item in msgs)
        {
            UserSession.AddMessage(item);
        }
        _Page.Mode = ManagementPageMode.Search;
        _Page.ListControl.Grid.DataBind();
    }
}