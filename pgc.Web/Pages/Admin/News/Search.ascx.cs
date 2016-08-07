using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_News_Search : BaseSearchControl<NewsPattern>
{
    public override NewsPattern Pattern
    {
        get
        {
            return new NewsPattern()
            {
                Title = txtTitle.Text,
                Status=lkpStatus.GetSelectedValue<NewsStatus>()
            };
        }
        set
        {
            txtTitle.Text = value.Title;
            lkpStatus.SetSelectedValue(value.Status);
        }
    }
}