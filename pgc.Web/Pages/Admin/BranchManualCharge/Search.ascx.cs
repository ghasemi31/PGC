using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_BranchManualCharge_Search : BaseSearchControl<BranchManualChargePattern>
{
    public override BranchManualChargePattern Pattern
    {
        get
        {
            return new BranchManualChargePattern()
            {
                Branch_ID=lkpBranch.GetSelectedValue<long>(),
                PersianDate=pdrRegDate.DateRange
            };
        }
        set
        {
            lkpBranch.SetSelectedValue(value.Branch_ID);
            pdrRegDate.DateRange = value.PersianDate;
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        Session["BranchManualChargePattern"] = Pattern;
    }
}