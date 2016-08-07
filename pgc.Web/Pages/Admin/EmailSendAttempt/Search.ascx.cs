using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using System;
using pgc.Business;
using pgc.Model;
using kFrameWork.Util;

public partial class Pages_Admin_EmailSendAttempt_Search : BaseSearchControl<EmailSendAttemptPattern>
{
    public override EmailSendAttemptPattern Pattern
    {
        get
        {
            EmailSendAttemptPattern ep = new EmailSendAttemptPattern()
            {
                Title = txtTitle.Text,
                //SentEmail_Count = nrSentEmail.Pattern,
                PersianDate = pdrDate.DateRange,
                EventType = lkpEventType.GetSelectedValue<EventType>(),
                EventTitle = txtEventTitle.Text,
                Recipient = txtRecipient.Text,
                //Time = tpTime.SelectedTime,
                Status = lkpStatus.GetSelectedValue<EmailSendAttemptStatus>()
            };

            return ep;
        }
        set
        {
            txtTitle.Text = value.Title;
            //nrSentEmail.Pattern = value.SentEmail_Count;
            pdrDate.DateRange = value.PersianDate;
            lkpEventType.SetSelectedValue(value.EventType);
            txtEventTitle.Text = value.EventTitle;
            txtRecipient.Text = value.Recipient;
            //tpTime.SelectedTime = value.Time;
            lkpStatus.SetSelectedValue(value.Status);
        }
    }

    public override EmailSendAttemptPattern DefaultPattern
    {
        get
        {
            EmailSendAttemptPattern ep = new EmailSendAttemptPattern();

            if ((this.Page as BasePage).HasValidQueryString<EventType>(QueryStringKeys.eventtype) &&
                (this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.occuredid))
            {
                ep.EventType = (EventType)Enum.Parse(typeof(EventType), (this.Page as BasePage).GetQueryStringValue<string>(QueryStringKeys.eventtype));
                ep.OccuredEventID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.occuredid);

                var page = (this.Page as BaseManagementPage<EmailSendAttemptBusiness, EmailSendAttempt, EmailSendAttemptPattern, pgcEntities>);

                page.ListControl.FindControl("parentInfo").Visible = true;
                page.ListControl.FindControl("btnEvent").Visible = true;
                string parentInfo = "اقدامات ایمیلی رخداد \"{0}\" در تاریخ {1}";

                //if (ep.EventType == EventType.Schedule)
                //{
                //    txtTitle.Text = new OccuredScheduleEventBusiness().Retrieve(ep.OccuredEventID).ScheduleEvent.Title;
                //    //tpTime.SelectedTime = new OccuredScheduleEventBusiness().Retrieve(ep.OccuredEventID).Date.TimeOfDay.ToString().Substring(0, 5);

                //    ((System.Web.UI.WebControls.Label)page.ListControl.FindControl("parentInfo")).Text = string.Format(parentInfo, txtTitle.Text, DateUtil.GetPersianDateWithTime(new OccuredScheduleEventBusiness().Retrieve(ep.OccuredEventID).Date));
                //}
                //else if (ep.EventType == EventType.System)
                if (ep.EventType == EventType.System)
                {
                    txtTitle.Text = new OccuredSystemEventBusiness().Retrieve(ep.OccuredEventID).SystemEvent.Title;
                    //tpTime.SelectedTime = new OccuredSystemEventBusiness().Retrieve(ep.OccuredEventID).Date.TimeOfDay.ToString().Substring(0, 5);

                    ((System.Web.UI.WebControls.Label)page.ListControl.FindControl("parentInfo")).Text = string.Format(parentInfo, txtTitle.Text, DateUtil.GetPersianDateWithTime(new OccuredSystemEventBusiness().Retrieve(ep.OccuredEventID).Date));
                }
            }

            return ep;
        }
    }

    public override EmailSendAttemptPattern SearchAllPattern
    {
        get
        {
            return base.SearchAllPattern;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        if ((this.Page as BasePage).HasValidQueryString<EventType>(QueryStringKeys.eventtype) &&
                       (this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.occuredid))
        {
            var page = (this.Page as BaseManagementPage<EmailSendAttemptBusiness, EmailSendAttempt, EmailSendAttemptPattern, pgcEntities>);
            page.SearchControl.Visible = false;            
        }
        base.OnPreRender(e);
    }
}