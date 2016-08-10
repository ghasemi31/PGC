using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_Sponsor_Search : BaseSearchControl<SponsorPattern>
{
    public override SponsorPattern Pattern
    {
        get
        {
            return new SponsorPattern()
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