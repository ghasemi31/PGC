using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_City_Search : BaseSearchControl<CityPattern>
{
    public override CityPattern Pattern
    {
        get
        {
            return new CityPattern()
            {
                Title = txtTitle.Text,
                Province_ID = lkpProvince.GetSelectedValue<long>()
            };
        }
        set
        {
            txtTitle.Text = value.Title;
            lkpProvince.SetSelectedValue(value.Province_ID);
        }
    }
}