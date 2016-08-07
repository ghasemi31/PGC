using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_Product_Search : BaseSearchControl<ProductPattern>
{
    public override ProductPattern Pattern
    {
        get
        {
            return new ProductPattern()
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