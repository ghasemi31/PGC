using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_BranchCredit_Search : BaseSearchControl<BranchCreditPattern>
{
    public override BranchCreditPattern Pattern
    {
        get
        {
            return new BranchCreditPattern()
            {
                Branch_ID=lkpBranch.GetSelectedValue<long>(),
                Status=lkpStatus.GetSelectedValue<BranchCreditStatus>()
            };
        }
        set
        {
            lkpBranch.SetSelectedValue(value.Branch_ID);
            lkpStatus.SetSelectedValue(value.Status);
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        Session["BranchCreditPrintPattern"] = Pattern;
    }
}