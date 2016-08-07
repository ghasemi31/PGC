using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_Help_Search : BaseSearchControl<HelpPattern>
{
    public override HelpPattern Pattern
    {
        get
        {
            return new HelpPattern()
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