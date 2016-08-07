using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_BranchOrderTitle_Search : BaseSearchControl<BranchOrderTitlePattern>
{
    public override BranchOrderTitlePattern Pattern
    {
        get
        {
            return new BranchOrderTitlePattern()
            {
                Title = txtTitle.Text,
                Status=lkpStatus.GetSelectedValue<BranchOrderTitleStatus>(),
                Price=nmrPrice.Pattern,
                Group_ID=lkpGroup.GetSelectedValue<long>()
            };
        }
        set
        {
            txtTitle.Text = value.Title;
            lkpStatus.SetSelectedValue(value.Status);
            nmrPrice.Pattern = value.Price;
            lkpGroup.SetSelectedValue(value.Group_ID);
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        Session["BranchOrderTitlePrintPattern"] = Pattern;
    }
}