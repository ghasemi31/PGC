using System;
using kFrameWork.UI;
using pgc.Model;
using pgc.Business;
using kFrameWork.Enums;
using pgc.Model.Enums;
using kFrameWork.Util;
using pgc.Model.Patterns;

public partial class Pages_Admin_SentMessageView_Detail : BaseDetailControl<SentSMS>
{
    public override SentSMS GetEntity(SentSMS Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new SentSMS();

        return Data;
    }

    public override void SetEntity(SentSMS Data, ManagementPageMode Mode)
    {
        if (Data.GatewayMessageID.HasValue)
        {
            MessageGetWayIDCell.Visible = true;
            lblMessageGetWayID.Visible = true;
            lblMessageGetWayID.InnerText = Data.GatewayMessageID.Value.ToString();            
        }
        else
        {
            MessageGetWayIDCell.Visible = false;
            lblMessageGetWayID.Visible = false;
        }

        lblPrivateNo.InnerText = new PrivateNoBusiness().Retrieve(Data.PrivateNo_ID).Number;
        

         if (Data.FaultType.HasValue)
        {
            FaultTypeCell.Visible = true;
            lblFault.Visible = true;
            lblFault.InnerText = Data.FaultType.Value.ToString();
        }
        else
        {
            FaultTypeCell.Visible = false;
            lblFault.Visible = false;
        }

         if (!(Data.GatewayMessageID.HasValue || Data.FaultType.HasValue))
             lblStatus.InnerText = "در حال ارسال";
         else if (Data.GatewayMessageID.HasValue)
             lblStatus.InnerText = EnumUtil.GetEnumElementPersianTitle((SendSMSStatus)Data.SendStatus);
         else if (Data.FaultType.HasValue)
             lblStatus.InnerText = string.Format("بروز خطا با کد:{0}", Data.FaultType.Value);



         lblMessageType.InnerText = EnumUtil.GetEnumElementPersianTitle((MessageType)Data.MessageType);

        lblDate.InnerHtml = DateUtil.GetPersianDateWithTime(Data.Date);
        lblRecipients.InnerText = Data.RecipientNumber;
        //lblStatus.Text = (Data.IsSent) ? "با موفقیت ارسال شده" : "در زمان ارسال با خطا مواجه شده";
        lblSize.InnerText= Data.SMSCount.ToString();
        lblBody.InnerText = Data.Body;
        lblBody.EnableViewState = false;

        lblEventType.InnerText = EnumUtil.GetEnumElementPersianTitle((EventType)Data.SMSSendAttempt.EventType);

        if (Data.SMSSendAttempt.OccuredEvent_ID != null)
        {
            EventTitleCell.Visible = true;
            lblEventTitle.Visible = true;

            //if (Data.SMSSendAttempt.EventType == (int)EventType.Schedule)
            //{
            //    linkEvent.NavigateUrl = GetRouteUrl("admin-occuredscheduleevent", new { occuredid = Data.SMSSendAttempt.OccuredEvent_ID.Value });
            //    lblEventTitle.InnerText = new OccuredScheduleEventBusiness().Retrieve(Data.SMSSendAttempt.OccuredEvent_ID.Value).ScheduleEvent.Title;
            //}
            //else if (Data.SMSSendAttempt.EventType == (int)EventType.System)
            if (Data.SMSSendAttempt.EventType == (int)EventType.System)
            {
                linkEvent.NavigateUrl = GetRouteUrl("admin-occuredsystemevent", new { occuredid = Data.SMSSendAttempt.OccuredEvent_ID.Value });
                lblEventTitle.InnerText = new OccuredSystemEventBusiness().Retrieve(Data.SMSSendAttempt.OccuredEvent_ID.Value).SystemEvent.Title;
            }
        }
        else
        {
            EventTitleCell.Visible = false;
            lblEventTitle.Visible = false;
        }
    }

    public override void EndMode(ManagementPageMode Mode)
    {
        base.EndMode(Mode);
        (this.Page as BaseManagementPage<SentSMSBusiness, SentSMS, SentSMSPattern, pgcEntities>).ListControl.Grid.DataBind();
    }
}