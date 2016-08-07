using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model.Patterns;

public partial class Pages_Admin_BranchOrderedTitle_Detail : BaseDetailControl<BranchOrderTitle>
{
    BaseManagementPage<BranchOrderedTitleBusiness, BranchOrderTitle, BranchOrderedTitlePattern, pgcEntities> _Page = new BaseManagementPage<BranchOrderedTitleBusiness, BranchOrderTitle, BranchOrderedTitlePattern, pgcEntities>();
    public BranchOrderTitle orderTitle = new BranchOrderTitle();
    public void Page_Load()
    {
        _Page=this.Page as BaseManagementPage<BranchOrderedTitleBusiness, BranchOrderTitle, BranchOrderedTitlePattern, pgcEntities>;
    }

    public override BranchOrderTitle GetEntity(BranchOrderTitle Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new BranchOrderTitle();
                
        return Data;
    }

    public override void SetEntity(BranchOrderTitle Data, ManagementPageMode Mode)
    {
        orderTitle = _Page.Business.RetrieveBranchOrderTitle(_Page.SelectedID);
        detailList.Controls.RemoveAt(0);
        detailList.Controls.Add(_Page.Business.CreateBranchOrderedTitleBranchDetails(_Page.SelectedID, _Page.SearchControl.Pattern.PersianDate));
    }
}