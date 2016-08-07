using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_Sample_Detail : BaseDetailControl<Sample>
{
    public override Sample GetEntity(Sample Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Sample();

        //Data.Title = txtTitle.Text;
        //Data.Description = txtDescription.Text;
        Data.Title = ImageUrl1.FilePath;
        Data.Description = ImageUrl2.FilePath;

        Data.StartPersianDate = dpStartDate.PersianDate;
        Data.EndPersianDate = dpEndDate.PersianDate;

        return Data;
    }

    public override void SetEntity(Sample Data, ManagementPageMode Mode)
    {
        //txtTitle.Text = Data.Title;
        //txtDescription.Text = Data.Description;
        ImageUrl1.FilePath = Data.Title;
        ImageUrl2.FilePath = Data.Description;

        dpStartDate.PersianDate = Data.StartPersianDate;
        dpEndDate.PersianDate = Data.EndPersianDate;
    }
}