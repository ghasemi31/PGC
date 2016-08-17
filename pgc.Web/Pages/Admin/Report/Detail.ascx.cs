using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_Report_Detail : BaseDetailControl<Report>
{
    public override Report GetEntity(Report Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Report();

        Data.Title = txtTitle.Text;
        Data.Body = ckBody.GetValue();
        Data.Summary = txtSummary.Text;
        Data.MetaKeyWords = txtMetaKeyWords.Text;
        Data.MetaDescription = txtMetaDesc.Text;
        Data.Note = txtNote.Text;
        Data.ThumbImageUrl = fupReportPic.FilePath;

        return Data;
    }

    public override void SetEntity(Report Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        ckBody.SetValue(Data.Body);
        txtSummary.Text = Data.Summary;
        txtNote.Text = Data.Note;
        txtMetaDesc.Text = Data.MetaDescription;
        txtMetaKeyWords.Text = Data.MetaKeyWords;
        fupReportPic.FilePath = Data.ThumbImageUrl;
    }
}