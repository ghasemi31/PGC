using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using kFrameWork.Util;
using pgc.Model.Patterns;
using pgc.Business;
using pgc.Model.Enums;

public partial class Pages_Admin_Sponsor_Detail : BaseDetailControl<Sponsor>
{
    public override Sponsor GetEntity(Sponsor Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Sponsor();

        Data.Title = txtTitle.Text;
        Data.Link = txtLink.Text;
        Data.ImagePath = fupPic.FilePath;

        return Data;
    }

    public override void SetEntity(Sponsor Data, ManagementPageMode Mode)
    {

        txtTitle.Text = Data.Title;
        txtLink.Text = Data.Link;
        fupPic.FilePath = Data.ImagePath;
    }


}