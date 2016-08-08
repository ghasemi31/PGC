using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_Game_Search : BaseSearchControl<GamePattern>
{
    public override GamePattern Pattern
    {
        get
        {
            return new GamePattern()
            {
                Title = txtTitle.Text,
                UrlKey=txtUrlKey.Text,
               // AllowOnlineOrder=lkpAllowOnlineOrder.GetSelectedValue<BooleanStatus>()

            };
        }
        set
        {
            txtTitle.Text = value.Title;
            txtUrlKey.Text = value.UrlKey;
            //lkpAllowOnlineOrder.SetSelectedValue(value.AllowOnlineOrder);
        }
    }
}