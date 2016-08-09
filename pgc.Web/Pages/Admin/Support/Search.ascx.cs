using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_Support_Search : BaseSearchControl<SupportPattern>
{
    public override SupportPattern Pattern
    {
        get
        {
            return new SupportPattern()
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