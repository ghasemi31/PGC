using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_Branch_Search : BaseSearchControl<BranchPattern>
{
    public override BranchPattern Pattern
    {
        get
        {
            return new BranchPattern()
            {
                Title = txtTitle.Text,
                UrlKey=txtUrlKey.Text,
               // AllowOnlineOrder=lkpAllowOnlineOrder.GetSelectedValue<BooleanStatus>()

            };
        }
        set
        {
            txtTitle.Text = value.Title;
            txtUrlKey.Text = value.UrlKey;
            //lkpAllowOnlineOrder.SetSelectedValue(value.AllowOnlineOrder);
        }
    }
}