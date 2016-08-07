using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using System;
using pgc.Business;
using pgc.Model;
using kFrameWork.Util;

public partial class Pages_Admin_SMSSendAttempt_Search : BaseSearchControl<SMSSendAttemptPattern>
{
    public override SMSSendAttemptPattern Pattern
    {
        get
        {
            SMSSendAttemptPattern ep = new SMSSendAttemptPattern()
            {
                Message = txtTitle.Text,
                Total_SucceedCount = nrSentEmail.Pattern,
                PersianDate = pdrDate.DateRange,
                EventType = lkpEventType.GetSelectedValue<EventType>(),
                EventTitle = txtEventTitle.Text,
                RecipientNumber = txtRecipient.Text,
                //Time = tpTime.SelectedTime,
                Status = lkpStatus.GetSelectedValue<SMSSendAttemptStatus>(),
                MessageType = lkpMsgType.GetSelectedValue<MessageType>()
            };

            return ep;
        }
        set
        {
            txtTitle.Text = value.Message;
            nrSentEmail.Pattern = value.Total_SucceedCount;
            pdrDate.DateRange = value.PersianDate;
            lkpEventType.SetSelectedValue(value.EventType);
            txtEventTitle.Text = value.EventTitle;
            txtRecipient.Text = value.RecipientNumber;
            //tpTime.SelectedTime = value.Time;
            lkpStatus.SetSelectedValue(value.Status);
            lkpMsgType.SetSelectedValue(value.MessageType);
        }
    }

    public override SMSSendAttemptPattern DefaultPattern
    {
        get
        {
            SMSSendAttemptPattern ep = new SMSSendAttemptPattern();

            if ((this.Page as BasePage).HasValidQueryString<EventType>(QueryStringKeys.eventtype) &&
                (this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.occuredid))
            {
                ep.EventType = (EventType)Enum.Parse(typeof(EventType), (this.Page as BasePage).GetQueryStringValue<string>(QueryStringKeys.eventtype));
                ep.OccuredEventID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.occuredid);

                var page = (this.Page as BaseManagementPage<SMSSendAttemptBusiness, SMSSendAttempt, SMSSendAttemptPattern, pgcEntities>);

                page.ListControl.FindControl("parentInfo").Visible = true;
                page.ListControl.FindControl("btnEvent").Visible = true;
                string parentInfo = "اقدامات پیامکی رخداد \"{0}\" در تاریخ {1}";

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

    public override SMSSendAttemptPattern SearchAllPattern
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
            var page = (this.Page as BaseManagementPage<SMSSendAttemptBusiness, SMSSendAttempt, SMSSendAttemptPattern, pgcEntities>);
            page.SearchControl.Visible = false;            
        }
        base.OnPreRender(e);
    }
}