using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_SiteMapItem_Search : BaseSearchControl<SiteMapItemPattern>
{
    public override SiteMapItemPattern Pattern
    {
        get
        {
            return new SiteMapItemPattern()
            {
                Title = txtTitle.Text,
                SiteMapCat_ID=lkpSiteMapCat.GetSelectedValue<long>(),
            };
        }
        set
        {
            txtTitle.Text = value.Title;
            lkpSiteMapCat.SetSelectedValue(value.SiteMapCat_ID);
        }
    }
}