using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Agent_UserComment_Search : BaseSearchControl<AgentUserCommentPattern>
{
    public override AgentUserCommentPattern Pattern
    {
        get
        {
            return new AgentUserCommentPattern()
            {
                Name = txtName.Text,
                Type=lkcType.GetSelectedValue<UserCommentType>(),
                Status=lkcStatus.GetSelectedValue<UserCommentStatus>(),
                UCPersianDate=pdrUCPersianDate.DateRange,
 
            };
        }
        set
        {
            txtName.Text = value.Name;
            lkcStatus.SetSelectedValue(value.Status);
            lkcType.SetSelectedValue(value.Type);
            pdrUCPersianDate.DateRange = value.UCPersianDate;

        }
    }
}