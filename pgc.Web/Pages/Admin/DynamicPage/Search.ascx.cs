using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_DynPage_Search : BaseSearchControl<DynPagePattern>
{
    public override DynPagePattern Pattern
    {
        get
        {
            return new DynPagePattern()
            {
                Title = txtTitle.Text,
                Meta=txtMeta.Text,
                Content=txtContent.Text,
                UrlKey=txtUrlKey.Text,
                
            };
        }
        set
        {
            txtTitle.Text = value.Title;
            txtUrlKey.Text = value.UrlKey;
            txtMeta.Text = value.Meta;
            txtContent.Text = value.Content;
        }
    }
}