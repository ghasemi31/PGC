using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using kFrameWork.Util;
using pgc.Model.Patterns;
using pgc.Business;
using pgc.Model.Enums;

public partial class Pages_Admin_News_Detail : BaseDetailControl<News>
{
    public override News GetEntity(News Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new News();

        Data.Title = txtTitle.Text;
        Data.Body = txtBody.Text;
        Data.Summary = txtSummary.Text;
        Data.NewsPicPath = fupNewsPic.FilePath;
        Data.PageTitle = txtPageTitle.Text;
        Data.PageDescription = txtPageDescription.Text;
        Data.PageKeywords = txtPageKeywords.Text.Replace("\n", ", ");
        Data.Gallery_ID = lkpGallery.GetSelectedValue<long>();
        Data.IsDisplayGallery = Convert.ToBoolean(chkIsDisplayGallery.Checked);
        //Data.Status = chkStatus.Checked ? (int)NewsStatus.Show : (int)NewsStatus.Hide;
        Data.Status = (int)lkpStatus.GetSelectedValue<NewsStatus>();
        Data.NewsPersianDate = pdpDate.PersianDate;
        Data.NewsDate = kFrameWork.Util.DateUtil.GetEnglishDateTime(pdpDate.PersianDate);
        return Data;
    }

    public override void SetEntity(News Data, ManagementPageMode Mode)
    {

        txtTitle.Text = Data.Title;
        txtBody.Text = Data.Body;
        txtSummary.Text = Data.Summary;
        fupNewsPic.FilePath = Data.NewsPicPath;
        txtPageTitle.Text = Data.PageTitle;
        txtPageDescription.Text = Data.PageDescription;
        txtPageKeywords.Text = Data.PageKeywords.Replace(", ", "\n");
        lkpGallery.SetSelectedValue(Data.Gallery_ID);
        chkIsDisplayGallery.Checked = Data.IsDisplayGallery;
        //chkStatus.Checked = (Data.Status == (int)NewsStatus.Show) ? true : false;
        lkpStatus.SetSelectedValue(Data.Status);
        pdpDate.PersianDate = Data.NewsPersianDate;

    }


}