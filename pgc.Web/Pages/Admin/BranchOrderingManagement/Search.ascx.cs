using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_BranchOrderingManagement_Search : BaseSearchControl<BranchOrderingManagementPattern>
{
    public override BranchOrderingManagementPattern Pattern
    {
        get
        {
            return new BranchOrderingManagementPattern()
            {
                Branch_ID = lkpBranch.GetSelectedValue<long>(),
                OrderTitle_ID = lkpOrderTitle.GetSelectedValue<long>()
            };
        }
        set
        {
            lkpBranch.SetSelectedValue(value.Branch_ID);
            lkpOrderTitle.SetSelectedValue(value.OrderTitle_ID);
        }
    }
}