using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using kFrameWork.Util;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model.Patterns;

public partial class Pages_Admin_EmailSendAttempt_Detail : BaseDetailControl<EmailSendAttempt>
{
    public override EmailSendAttempt GetEntity(EmailSendAttempt Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new EmailSendAttempt();

        return Data;
    }

    public override void SetEntity(EmailSendAttempt Data, ManagementPageMode Mode)
    {
        fup1.FilePath = Data.FilePath1;        
        fup2.FilePath = Data.FilePath2;
        fup3.FilePath = Data.FilePath3;
        fup4.FilePath = Data.FilePath4;
        fup5.FilePath = Data.FilePath5;

        Tr1.Visible = !string.IsNullOrEmpty(Data.FilePath1);
        Tr2.Visible = !string.IsNullOrEmpty(Data.FilePath2);
        Tr3.Visible = !string.IsNullOrEmpty(Data.FilePath3);
        Tr4.Visible = !string.IsNullOrEmpty(Data.FilePath4);
        Tr5.Visible = !string.IsNullOrEmpty(Data.FilePath5);


        lblSubject.Text = Data.Subject;
        lblBody.InnerHtml = Data.Body; 
        lblBody.EnableViewState = false;
        lblRecipient.Text = Data.Recipients.Replace(",", "<br />");
        lblDate.Text = DateUtil.GetPersianDateWithTime(Data.Date);
        lblEventType.Text = EnumUtil.GetEnumElementPersianTitle((EventType)Data.EventType);


        lblSentEmail.Text = Data.SentEmail_Count.ToString();
        lblfailedEmail.Text = Data.FailedEmail_Count.ToString();
        lblTotalMail.Text = Data.TotalEmail_Count.ToString();
        lblSentBlock.Text = Data.SentBlock_Count.ToString();
        lblFailedBlock.Text = Data.FailedBlock_Count.ToString();
        lblTotalBlock.Text = Data.TotalBlock_Count.ToString();
        lblBlockSize.Text = Data.BlockSize.ToString();
        lblInvalidEmail.Text = Data.InvalidEmailAddress_Count.ToString();

        if (Data.TotalEmail_Count == Data.SentEmail_Count)
            lblStatus.Text = EnumUtil.GetEnumElementPersianTitle(EmailSendAttemptStatus.AllSent);
        else if (Data.SentEmail_Count == 0)
            lblStatus.Text = EnumUtil.GetEnumElementPersianTitle(EmailSendAttemptStatus.NoSent);
        else
            lblStatus.Text = EnumUtil.GetEnumElementPersianTitle(EmailSendAttemptStatus.SomeSent);



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


    protected void btnEmailBlock_Click(object sender, EventArgs e)
    {
        if ((this.Page as BasePage).HasValidQueryString<EventType>(QueryStringKeys.eventtype) &&
               (this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.occuredid))
        {
            EventType type = (EventType)Enum.Parse(typeof(EventType), (this.Page as BasePage).GetQueryStringValue<string>(QueryStringKeys.eventtype));
            long OccuredEventID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.occuredid);

            Response.Redirect(GetRouteUrl("admin-sentemailblock", new { emailattemptid = (this.Page as BaseManagementPage<EmailSendAttemptBusiness, EmailSendAttempt, EmailSendAttemptPattern, pgcEntities>).SelectedID, occuredid = OccuredEventID, eventtype = type }));
        }
        else
            Response.Redirect(GetRouteUrl("admin-sentemailblock", new { emailattemptid = (this.Page as BaseManagementPage<EmailSendAttemptBusiness, EmailSendAttempt, EmailSendAttemptPattern, pgcEntities>).SelectedID }));
    }

}