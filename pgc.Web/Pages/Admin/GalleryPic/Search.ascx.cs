using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_GalleryPic_Search : BaseSearchControl<GalleryPicPattern>
{
    public override GalleryPicPattern Pattern
    {
        get
        {
            return new GalleryPicPattern()
            {
                Description = txtDesc.Text,
                Gallery_ID=lkpGallery.GetSelectedValue<long>(),
                
            };
        }
        set
        {
            txtDesc.Text = value.Description;
            lkpGallery.SetSelectedValue(value.Gallery_ID);

        }
    }

    public override GalleryPicPattern DefaultPattern
    {
        get
        {

            if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.id))
            {
                long GalleryID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.id);
                GalleryPicPattern pat = new GalleryPicPattern()
                {
                    Gallery_ID = GalleryID
                };
                return pat;
            }

            return base.DefaultPattern;
        }
    }
}