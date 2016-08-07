using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_Advertisement_Search : BaseSearchControl<AdvPattern>
{
    public override AdvPattern Pattern
    {
        get
        {
            return new AdvPattern()
            {
                Title = txtTitle.Text,
            };
        }
        set
        {
            txtTitle.Text = value.Title;
        }
    }
}