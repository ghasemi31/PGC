using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_Sample_Search : BaseSearchControl<SamplePattern>
{
    public override SamplePattern Pattern
    {
        get
        {
            return new SamplePattern()
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