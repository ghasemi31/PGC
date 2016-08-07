using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_Province_Search : BaseSearchControl<ProvincePattern>
{
    public override ProvincePattern Pattern
    {
        get
        {
            return new ProvincePattern()
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