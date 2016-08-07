using kFrameWork.UI;
using pgc.Model.Patterns;

public partial class Pages_Admin_Report_Search : BaseSearchControl<ReportPattern>
{
    public override ReportPattern Pattern
    {
        get
        {
            return new ReportPattern()
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