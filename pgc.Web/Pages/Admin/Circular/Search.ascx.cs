using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_Circular_Search : BaseSearchControl<CircularPattern>
{
    public override CircularPattern Pattern
    {
        get
        {
            return new CircularPattern()
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