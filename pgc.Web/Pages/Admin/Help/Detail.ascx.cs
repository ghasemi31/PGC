using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_Help_Detail : BaseDetailControl<Help>
{
    public override Help GetEntity(Help Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Help();

        Data.Title = txtTitle.Text;
        Data.Body = txtBody.Text;
        

        return Data;
    }

    public override void SetEntity(Help Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtBody.Text = Data.Body;
        

    }
}