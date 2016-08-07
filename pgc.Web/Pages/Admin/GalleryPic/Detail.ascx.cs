using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_GalleryPic_Detail : BaseDetailControl<GalleryPic>
{
    public override GalleryPic GetEntity(GalleryPic Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new GalleryPic();
        Data.Gallery_ID = lkpGallery.GetSelectedValue<long>();
        Data.Description = txtDesc.Text;
        Data.DispOrder = txtDispOrder.GetNumber<int>();
        Data.ImagePath = fupGalleryPic.FilePath;
        return Data;
    }

    public override void SetEntity(GalleryPic Data, ManagementPageMode Mode)
    {
        lkpGallery.SetSelectedValue(Data.Gallery_ID);
        txtDesc.Text = Data.Description;
        txtDispOrder.SetNumber(Data.DispOrder);
        fupGalleryPic.FilePath = Data.ImagePath;

    }
}