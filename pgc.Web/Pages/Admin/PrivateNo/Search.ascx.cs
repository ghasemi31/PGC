using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_PrivateNo_Search : BaseSearchControl<PrivateNoPattern>
{
    public override PrivateNoPattern Pattern
    {
        get
        {
            return new PrivateNoPattern()
            {
                Number = txtNumber.Text,
            };
        }
        set
        {
            txtNumber.Text = value.Number;
        }
    }
}