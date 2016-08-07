using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_Gallery_Search : BaseSearchControl<GalleryPattern>
{
    public override GalleryPattern Pattern
    {
        get
        {
            return new GalleryPattern()
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