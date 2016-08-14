using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_GameCenter_Search : BaseSearchControl<GameCenterPattern>
{
    public override GameCenterPattern Pattern
    {
        get
        {
            return new GameCenterPattern()
            {
                
                City_ID =  lkcCity.GetSelectedValue<long>(),
                Description = txtDesc.Text,
               
                Province_ID = lkcProvince.GetSelectedValue<long>()
           
            };
        }
        set
        {
            
            lkcCity.SetSelectedValue(value.City_ID);
            txtDesc.Text = value.Description;
           lkcProvince.SetSelectedValue(value.Province_ID);
      
            
        }
    }
}