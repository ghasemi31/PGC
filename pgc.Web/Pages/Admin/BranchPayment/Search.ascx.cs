using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_BranchPayment_Search : BaseSearchControl<BranchPaymentPattern>
{
    public override BranchPaymentPattern Pattern
    {
        get
        {
            return new BranchPaymentPattern()
            {
                Title = txtTitle.Text,
                Branch_ID=lkpBranch.GetSelectedValue<long>(),                
                PayDate=pdrDate.DateRange,
                Price=nmrPrice.Pattern,
                Type=lkpPaymentType.GetSelectedValue<BranchPaymentTypeForSearch>()
            };
        }
        set
        {
            txtTitle.Text = value.Title;
            lkpBranch.SetSelectedValue(value.Branch_ID);
            pdrDate.DateRange = value.PayDate;
            nmrPrice.Pattern = value.Price;
            lkpPaymentType.SetSelectedValue(value.Type);
        }
    }

    public override BranchPaymentPattern DefaultPattern
    {
        get
        {
            return new BranchPaymentPattern() { DefaultSearch = true };
        }
    }
    protected void OnSearchAll(object sender, System.EventArgs e)
    {
        IsFirstTime.Value = "false";
        base.OnSearchAll(sender, e);
    }
    protected void OnSearch(object sender, System.EventArgs e)
    {
        IsFirstTime.Value = "false";
        base.OnSearch(sender, e);
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        Session["BranchPaymentPrintPattern"] = Pattern;
    }
}