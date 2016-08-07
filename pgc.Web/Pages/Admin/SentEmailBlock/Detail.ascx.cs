using System;
using kFrameWork.UI;
using pgc.Model;
using pgc.Business;
using kFrameWork.Enums;
using pgc.Model.Enums;
using kFrameWork.Util;
using pgc.Model.Patterns;

public partial class Pages_Admin_SentEmailBlock_Detail : BaseDetailControl<SentEmailBlock>
{
    public override SentEmailBlock GetEntity(SentEmailBlock Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new SentEmailBlock();

        return Data;
    }

    public override void SetEntity(SentEmailBlock Data, ManagementPageMode Mode)
    {
        lblDate.Text = DateUtil.GetPersianDateWithTime(Data.Date);
        lblRecipients.Text = Data.RecipientMailAddress.Replace(",", "<br />");
        lblStatus.Text = (Data.IsSent) ? "با موفقیت ارسال شده" : "در زمان ارسال با خطا مواجه شده";
        lblSize.Text = Data.RecipientMailAddress.Split(',').Length.ToString();
        lblBody.InnerHtml = Data.EmailSendAttempt.Body;
        lblBody.EnableViewState = false;
        lblSubject.Text = Data.EmailSendAttempt.Subject;

        lblEventType.Text = EnumUtil.GetEnumElementPersianTitle((EventType)Data.EmailSendAttempt.EventType);

        if (Data.EmailSendAttempt.OccuredEvent_ID != null)
        {
            EventTitleCell.Visible = true;
            lblEventTitle.Visible = true;

            //if (Data.EmailSendAttempt.EventType == (int)EventType.Schedule)
            //{
            //    linkEvent.NavigateUrl = GetRouteUrl("admin-occuredscheduleevent", new { occuredid = Data.EmailSendAttempt.OccuredEvent_ID.Value });
            //    lblEventTitle.Text = new OccuredScheduleEventBusiness().Retrieve(Data.EmailSendAttempt.OccuredEvent_ID.Value).ScheduleEvent.Title;
            //}
            //else if (Data.EmailSendAttempt.EventType == (int)EventType.System)
            if (Data.EmailSendAttempt.EventType == (int)EventType.System)
            {
                linkEvent.NavigateUrl = GetRouteUrl("admin-occuredsystemevent", new { occuredid = Data.EmailSendAttempt.OccuredEvent_ID.Value });
                lblEventTitle.Text = new OccuredSystemEventBusiness().Retrieve(Data.EmailSendAttempt.OccuredEvent_ID.Value).SystemEvent.Title;
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
        (this.Page as BaseManagementPage<SentEmailBlockBusiness, SentEmailBlock, SentEmailBlockPattern, pgcEntities>).ListControl.Grid.DataBind();
    }
}