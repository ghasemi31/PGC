using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_City_Detail : BaseDetailControl<City>
{
    public override City GetEntity(City Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new City();

        Data.Title = txtTitle.Text;
        Data.Province_ID = lkpProvince.GetSelectedValue<long>();

        return Data;
    }

    public override void SetEntity(City Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        lkpProvince.SetSelectedValue(Data.Province_ID);
    }
}