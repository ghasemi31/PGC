using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;

public partial class Pages_Admin_Article_Detail : BaseDetailControl<Article>
{
    public override Article GetEntity(Article Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Article();

        Data.Title = txtTitle.Text;
        Data.Body = txtBody.Text;
        Data.Summary = txtSummary.Text;
        Data.MetaKeyWords = txtMetaKeyWords.Text;
        Data.MetaDescription = txtMetaDesc.Text;
        Data.Note = txtNote.Text;

        return Data;
    }

    public override void SetEntity(Article Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        txtBody.Text = Data.Body;
        txtSummary.Text = Data.Summary;
        txtNote.Text = Data.Note;
        txtMetaDesc.Text = Data.MetaDescription;
        txtMetaKeyWords.Text = Data.MetaKeyWords;

    }
}