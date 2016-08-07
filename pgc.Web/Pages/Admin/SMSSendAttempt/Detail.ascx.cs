using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using kFrameWork.Util;
using pgc.Model.Enums;
using pgc.Business;
using System.Linq;
using kFrameWork.Business;
using pgc.Model.Patterns;

public partial class Pages_Admin_SMSSendAttempt_Detail : BaseDetailControl<SMSSendAttempt>
{
    public override SMSSendAttempt GetEntity(SMSSendAttempt Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new SMSSendAttempt();

        return Data;
    }

    public override void SetEntity(SMSSendAttempt Data, ManagementPageMode Mode)
    {


        lblBody.InnerText = Data.Body;
        lblRecipient.Text = Data.Recipients.Replace(",", "<br />");
        lblRecipient.Text = (lblRecipient.Text.StartsWith("<br />")) ? lblRecipient.Text.Substring(6) : lblRecipient.Text;  
        lblDate.Text = DateUtil.GetPersianDateWithTime(Data.Date);
        lblEventType.Text = EnumUtil.GetEnumElementPersianTitle((EventType)Data.EventType);
        try
        { lblPrivateNo.Text = Data.SentSMS.First().PrivateNo.Number; }
        catch (Exception) { }
        

        lblSentEmail.Text = Data.Total_SucceedCount.ToString();
        lblfailedEmail.Text = Data.Total_FailedCount.ToString();
        lblTotalMail.Text = Data.Total_SumCount.ToString();
        lblUnknown.Text = Data.Total_UnknownCount.ToString();

        lblBlockSize.Text = OptionBusiness.GetTinyInt(OptionKey.PacketSize).ToString();

        if (Data.Total_SumCount == Data.Total_SucceedCount)
            lblStatus.Text = EnumUtil.GetEnumElementPersianTitle(SMSSendAttemptStatus.AllSent);
        else if (Data.Total_SucceedCount == 0)
            lblStatus.Text = EnumUtil.GetEnumElementPersianTitle(SMSSendAttemptStatus.NoSent);
        else
            lblStatus.Text = EnumUtil.GetEnumElementPersianTitle(SMSSendAttemptStatus.SomeSent);


        if (Data.OccuredEvent_ID != null)
        {
            EventTitleCell.Visible = true;
            lblEventTitle.Visible = true;

            //if (Data.EventType == (int)EventType.Schedule)
            //{
            //    linkEvent.NavigateUrl = GetRouteUrl("admin-occuredscheduleevent", new { occuredid = Data.OccuredEvent_ID.Value });
            //    lblEventTitle.Text = new OccuredScheduleEventBusiness().Retrieve(Data.OccuredEvent_ID.Value).ScheduleEvent.Title;
            //}
            //else if (Data.EventType == (int)EventType.System)
            if (Data.EventType == (int)EventType.System)
            {
                linkEvent.NavigateUrl = GetRouteUrl("admin-occuredsystemevent", new { occuredid = Data.OccuredEvent_ID.Value });
                lblEventTitle.Text = new OccuredSystemEventBusiness().Retrieve(Data.OccuredEvent_ID.Value).SystemEvent.Title;
            }
        }
        else
        {
            EventTitleCell.Visible = false;
            lblEventTitle.Visible = false;
        }
    }

    protected void btnSMSPacket_Click(object sender, EventArgs e)
    {
        if ((this.Page as BasePage).HasValidQueryString<EventType>(QueryStringKeys.eventtype) &&
                (this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.occuredid))
        {
            EventType type = (EventType)Enum.Parse(typeof(EventType), (this.Page as BasePage).GetQueryStringValue<string>(QueryStringKeys.eventtype));
            long OccuredEventID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.occuredid);

            Response.Redirect(GetRouteUrl("admin-sentsmspacket", new { smsattemptid = (this.Page as BaseManagementPage<SMSSendAttemptBusiness, SMSSendAttempt, SMSSendAttemptPattern, pgcEntities>).SelectedID, occuredid = OccuredEventID, eventtype = type }));
        }
        else
            Response.Redirect(GetRouteUrl("admin-sentsmspacket", new { smsattemptid = (this.Page as BaseManagementPage<SMSSendAttemptBusiness, SMSSendAttempt, SMSSendAttemptPattern, pgcEntities>).SelectedID }));
    }

}