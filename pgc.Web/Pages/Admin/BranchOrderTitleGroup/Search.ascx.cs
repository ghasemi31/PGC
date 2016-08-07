using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_BranchOrderTitleGroup_Search : BaseSearchControl<BranchOrderTitleGroupPattern>
{
    public override BranchOrderTitleGroupPattern Pattern
    {
        get
        {
            return new BranchOrderTitleGroupPattern()
            {
                Title = txtTitle.Text                
            };
        }
        set
        {
            txtTitle.Text = value.Title;
        }
    }
}