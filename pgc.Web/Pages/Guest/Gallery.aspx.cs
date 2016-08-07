using kFrameWork.UI;
using pgc.Business;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Guest_Gallery : BasePage
{
    public pgc.Business.General.GalleryBusiness gallery_business = new pgc.Business.General.GalleryBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lsvGallery_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            int gallery_id = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"ID"));
            List<GalleryPic> pics = gallery_business.GetGalleryPic(gallery_id);

            (e.Item.FindControl("lsvPics") as System.Web.UI.WebControls.ListView).DataSource = pics;
            (e.Item.FindControl("lsvPics") as System.Web.UI.WebControls.ListView).DataBind();
        }
    }
}