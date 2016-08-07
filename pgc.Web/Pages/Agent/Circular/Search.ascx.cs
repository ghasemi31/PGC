using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Agent_Circular_Search : BaseSearchControl<CircularPattern>
{
    public override CircularPattern Pattern
    {
        get
        {
            return new CircularPattern()
            {
                Title = txtFileName.Text,
            };
        }
        set
        {
            txtFileName.Text = value.Title;

        }
    }
}