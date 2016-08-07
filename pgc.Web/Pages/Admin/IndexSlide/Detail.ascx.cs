using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_IndexSlide_Detail : BaseDetailControl<IndexSlide>
{
    public override IndexSlide GetEntity(IndexSlide Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new IndexSlide();

        Data.Title = txtTitle.Text;
        Data.NavigateUrl = txtNavigateUrl.Text;
        Data.DispOrder = txtDispOrder.GetNumber<int>();
        Data.ImagePath = fupPic.FilePath;
        return Data;
    }

    public override void SetEntity(IndexSlide Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtNavigateUrl.Text = Data.NavigateUrl;
        txtDispOrder.SetNumber(Data.DispOrder);
        fupPic.FilePath = Data.ImagePath;
    }
}