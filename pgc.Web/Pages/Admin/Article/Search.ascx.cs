using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_Article_Search : BaseSearchControl<ArticlePattern>
{
    public override ArticlePattern Pattern
    {
        get
        {
            return new ArticlePattern()
            {
                Title = txtTitle.Text,
            };
        }
        set
        {
            txtTitle.Text = value.Title;
        }
    }
}