using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model;
using System;
using kFrameWork.Util;

public partial class Pages_Admin_SentEmailBlock_Search : BaseSearchControl<SentEmailBlockPattern>
{
    public override SentEmailBlockPattern Pattern
    {
        get
        {
            return new SentEmailBlockPattern()
            {
                Title = txtTitle.Text,
                EventType=lkpEventType.GetSelectedValue<EventType>(),
                EventTitle=txtEventTitle.Text,
                RecipientMailAddress=txtRecipients.Text,
                Size=nrSentEmail.Pattern,
                IsSent=lkpStatus.GetSelectedValue<SentEmailBlockStatus>()
            };
        }
        set
        {
            txtTitle.Text = value.Title;
            lkpEventType.SetSelectedValue(value.EventType);
            txtEventTitle.Text = value.EventTitle;
            txtRecipients.Text = value.RecipientMailAddress;
            nrSentEmail.Pattern = value.Size;
            lkpStatus.SetSelectedValue(value.IsSent);
        }
    }

    public override SentEmailBlockPattern DefaultPattern
    {
        get
        {
            SentEmailBlockPattern sep = new SentEmailBlockPattern();
            var page = (this.Page as BaseManagementPage<SentEmailBlockBusiness, SentEmailBlock, SentEmailBlockPattern, pgcEntities>);

            if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.emailattemptid))
            {
                long attemptid = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.emailattemptid);
                sep.EmailSentAttempt_ID = attemptid;

                EmailSendAttempt data = new EmailSendAttemptBusiness().Retrieve(attemptid);
                string eventTitle = "";
                string occuredDate = "";

                if (data.OccuredEvent_ID != null)
                {
                    //if (data.EventType == (int)EventType.Schedule)
                    //{
                    //    OccuredScheduleEvent oc = new OccuredScheduleEventBusiness().Retrieve(data.OccuredEvent_ID.Value);
                    //    eventTitle = oc.ScheduleEvent.Title;
                    //    occuredDate = DateUtil.GetPersianDateWithTime(oc.Date);
                    //}
                    //else if (data.EventType == (int)EventType.System)
                    if (data.EventType == (int)EventType.System)
                    {
                        OccuredSystemEvent oc = new OccuredSystemEventBusiness().Retrieve(data.OccuredEvent_ID.Value);
                        eventTitle = oc.SystemEvent.Title;
                        occuredDate = DateUtil.GetPersianDateWithTime(oc.Date);
                    }

                    string parentInfo = "ایمیل های ارسالی رخداد \"{0}\" در تاریخ {1}";
                    var label = ((System.Web.UI.WebControls.Label)page.ListControl.FindControl("parentInfo"));
                    label.Visible = true;
                    label.Text = string.Format(parentInfo, eventTitle, occuredDate);

                    page.ListControl.FindControl("btnBack").Visible = true;
                }
                else
                {

                    string parentInfo = "ایمیل های ارسال شده دستی در تاریخ {1}";
                    var label = ((System.Web.UI.WebControls.Label)page.ListControl.FindControl("parentInfo"));
                    label.Visible = true;
                    label.Text = string.Format(parentInfo, eventTitle, DateUtil.GetPersianDateWithTime(data.Date));

                    page.ListControl.FindControl("btnBack").Visible = true;
                }
            }
            
            return sep;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.emailattemptid))
        {
            var page = (this.Page as BaseManagementPage<SentEmailBlockBusiness, SentEmailBlock, SentEmailBlockPattern, pgcEntities>);
            page.SearchControl.Visible = false;
        }
        base.OnPreRender(e);
    }
}