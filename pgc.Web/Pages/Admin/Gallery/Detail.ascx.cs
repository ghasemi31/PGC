using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_Gallery_Detail : BaseDetailControl<Gallery>
{
    public override Gallery GetEntity(Gallery Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Gallery();

        Data.Title = txtTitle.Text;
        Data.DispOrder = txtDispOrder.GetNumber<int>();
        Data.GalleryThumbImagePath = fupGallery.FilePath;
        return Data;
    }

    public override void SetEntity(Gallery Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtDispOrder.SetNumber(Data.DispOrder);
        fupGallery.FilePath = Data.GalleryThumbImagePath;
    }
}