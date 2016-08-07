using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Project_Gallery : BaseUserControl
{
    public GalleryBusiness business = new GalleryBusiness();
    public Gallery gallery;

    private long _galleryID;

    public long GalleryID
    {
        get
        {
            return _galleryID;
        }
        set
        {
            _galleryID = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        gallery = business.GetGallery(GalleryID);
    }
}