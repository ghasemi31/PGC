using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;
using System;
using kFrameWork.Util;

public partial class Pages_Admin_SentMessage_Search : BaseSearchControl<SentSMSPattern>
{
    public override SentSMSPattern Pattern
    {
        get
        {
            return new SentSMSPattern()
            {
                Message=txtBody.Text,
                MessageType=lkcMessageType.GetSelectedValue<MessageType>(),
                RecipientNumber=txtRecipient.Text,
                SendDate=pdrSendPersianDate.DateRange,
                SendStatus=lkcSendStatus.GetSelectedValue<SendSMSStatus>()
            };
        }
        set
        {
            txtBody.Text = value.Message;
            txtRecipient.Text = value.RecipientNumber;
            lkcMessageType.SetSelectedValue(value.MessageType);
            lkcSendStatus.SetSelectedValue(value.SendStatus);
            pdrSendPersianDate.DateRange = value.SendDate;
        }
    }

    public override SentSMSPattern DefaultPattern
    {
        get
        {
            SentSMSPattern sep = new SentSMSPattern();
            var page = (this.Page as BaseManagementPage<SentSMSBusiness, SentSMS, SentSMSPattern, pgcEntities>);

            if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.smsattemptid))
            {
                long attemptid = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.smsattemptid);
                sep.SMSSendAttempt_ID = attemptid;

                SMSSendAttempt data = new SMSSendAttemptBusiness().Retrieve(attemptid);
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

                    string parentInfo = "پیامک های ارسالی رخداد \"{0}\" در تاریخ {1}";
                    var label = ((System.Web.UI.WebControls.Label)page.ListControl.FindControl("parentInfo"));
                    label.Visible = true;
                    label.Text = string.Format(parentInfo, eventTitle, occuredDate);

                    page.ListControl.FindControl("btnBack").Visible = true;
                }
                else
                {

                    string parentInfo = "پیامک های ارسال شده دستی در تاریخ {1}";
                    var label = ((System.Web.UI.WebControls.Label)page.ListControl.FindControl("parentInfo"));
                    label.Visible = true;
                    label.Text = string.Format(parentInfo, eventTitle, data.PersianDate);

                    page.ListControl.FindControl("btnBack").Visible = true;
                }
            }

            return sep;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.smsattemptid))
        {
            var page = (this.Page as BaseManagementPage<SentSMSBusiness, SentSMS, SentSMSPattern, pgcEntities>);
            page.SearchControl.Visible = false;
        }
        base.OnPreRender(e);
    }
}