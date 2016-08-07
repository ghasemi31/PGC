using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_HeadingNews_Search : BaseSearchControl<HeadingNewsPattern>
{
    public override HeadingNewsPattern Pattern
    {
        get
        {
            return new HeadingNewsPattern()
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