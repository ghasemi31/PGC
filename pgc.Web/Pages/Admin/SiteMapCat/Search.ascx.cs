using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_SiteMapCat_Search : BaseSearchControl<SiteMapCatPattern>
{
    public override SiteMapCatPattern Pattern
    {
        get
        {
            return new SiteMapCatPattern()
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