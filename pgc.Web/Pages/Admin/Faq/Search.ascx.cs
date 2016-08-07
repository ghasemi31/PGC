using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_Faq_Search : BaseSearchControl<FaqPattern>
{
    public override FaqPattern Pattern
    {
        get
        {
            return new FaqPattern()
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