using kFrameWork.Enums;
using kFrameWork.UI;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Advertisment_Detail : BaseDetailControl<Advertisment>
{
    public override Advertisment GetEntity(Advertisment Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Advertisment();

        Data.Title = txtTitle.Text;
        Data.Adv_Url = txtLink.Text;
        Data.DispOrder = txtDispOrder.GetNumber<int>();
        Data.FilePath = fupPic.FilePath;
        Data.ADV_Type = lkcAdvType.GetSelectedValue<int>();
        return Data;
    }

    public override void SetEntity(Advertisment Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtLink.Text = Data.Adv_Url;
        lkcAdvType.SetSelectedValue(Data.ADV_Type);
        txtDispOrder.SetNumber(Data.DispOrder);
        fupPic.FilePath = Data.FilePath;
    }
}