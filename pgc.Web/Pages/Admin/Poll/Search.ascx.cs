using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_Poll_Search : BaseSearchControl<PollPattern>
{
    public override PollPattern Pattern
    {
        get
        {
            return new PollPattern()
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