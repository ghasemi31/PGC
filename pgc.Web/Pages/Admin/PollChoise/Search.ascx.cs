using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_PollChoise_Search : BaseSearchControl<PollChoisePattern>
{
    public override PollChoisePattern Pattern
    {
        get
        {
            return new PollChoisePattern()
            {
                
                Poll_ID=lkpPoll.GetSelectedValue<long>(),
                Title=txtTitle.Text,
                
            };
        }
        set
        {
            
            lkpPoll.SetSelectedValue(value.Poll_ID);
            txtTitle.Text = value.Title;
        }
    }

    public override PollChoisePattern DefaultPattern
    {
        get
        {

            if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.id))
            {
                long PollID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.id);
                PollChoisePattern pat = new PollChoisePattern()
                {
                    Poll_ID = PollID
                };
                return pat;
            }

            return base.DefaultPattern;
        }
    }
}