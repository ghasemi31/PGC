using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_AccessLevel_Search : BaseSearchControl<AccessLevelPattern>
{
    public override AccessLevelPattern Pattern
    {
        get
        {
            return new AccessLevelPattern()
            {
                Title = txtTitle.Text,
                Role = lkcRole.GetSelectedValue<Role>()
            };
        }
        set
        {
            txtTitle.Text = value.Title;
            lkcRole.SetSelectedValue(value.Role);
        }
    }
}