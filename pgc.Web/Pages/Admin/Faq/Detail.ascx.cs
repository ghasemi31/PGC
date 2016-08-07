using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_Faq_Detail : BaseDetailControl<Faq>
{
    public override Faq GetEntity(Faq Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Faq();

        Data.Title = txtTitle.Text;
        Data.Body = txtBody.Text;
        Data.Summery = txtSummery.Text;

        return Data;
    }

    public override void SetEntity(Faq Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtBody.Text = Data.Body;
        txtSummery.Text = Data.Summery;

    }
}