using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Patterns;
using pgc.Business;

public partial class Pages_Admin_BranchSold_Detail : BaseDetailControl<BranchTransaction>
{
    public BaseManagementPage<BranchSoldBusiness, BranchTransaction, BranchSoldPattern, pgcEntities> _Page = new BaseManagementPage<BranchSoldBusiness, BranchTransaction, BranchSoldPattern, pgcEntities>();

    public void Page_Load()
    {
        _Page = this.Page as BaseManagementPage<BranchSoldBusiness, BranchTransaction, BranchSoldPattern, pgcEntities>;
    }

    public override BranchTransaction GetEntity(BranchTransaction Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchTransaction();

        return Data;
    }

    public override void SetEntity(BranchTransaction Data, ManagementPageMode Mode)
    {
    }

    public override void BeginMode(ManagementPageMode Mode)
    {
        base.BeginMode(Mode);
        if (_Page.SelectedID > 0)
        {
            try { detailList.Controls.RemoveAt(0); }
            catch (Exception) { }
            detailList.Controls.Add(_Page.Business.CreateTableOfBranchSoldReadOnly(_Page.SelectedID, _Page.SearchControl.Pattern));
        }
    }

    public override void EndMode(ManagementPageMode Mode)
    {
        base.EndMode(Mode);
        _Page.ListControl.Grid.DataBind();
    }
    protected void OnSave(object sender, EventArgs e)
    {
        //var msgs = _Page.Business.UpdateBranchMinimumCredit(_Page.SelectedID, txtPrice.GetNumber<long>()).Messages;
        //var msgs = _Page.Business.UpdateBranchMinimumCredit(_Page.SelectedID, long.Parse(txtPrice.Text.Replace(",",""))).Messages;
        //foreach (var item in msgs)
        //{
        //    UserSession.AddMessage(item);
        //}
        //_Page.Mode = ManagementPageMode.Search;
        //_Page.ListControl.Grid.DataBind();
    }
}