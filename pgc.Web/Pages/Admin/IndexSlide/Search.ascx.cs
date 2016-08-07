using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_IndexSlide_Search : BaseSearchControl<IndexSlidePattern>
{
    public override IndexSlidePattern Pattern
    {
        get
        {
            return new IndexSlidePattern()
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