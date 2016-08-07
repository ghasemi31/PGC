using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using pgc.Business;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.Util;

public partial class Pages_Admin_OccuredSystemEvent_Detail : BaseDetailControl<OccuredSystemEvent>
{
    public override OccuredSystemEvent GetEntity(OccuredSystemEvent Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new OccuredSystemEvent();


        return Data;
    }

    public override void SetEntity(OccuredSystemEvent Data, ManagementPageMode Mode)
    {
        lblDate.Text = DateUtil.GetPersianDateWithTime(Data.Date);
        lblEventTile.Text = Data.SystemEvent.Title;
        lblDescription.Text = Data.Description;

        if (new EmailSendAttemptBusiness().RetrieveByOccuredEvent(EventType.Schedule, Data.ID).Count() > 0)
        {
            LinkMailCell.Visible = true;
            linkMail.NavigateUrl = GetRouteUrl("admin-emailarchive", new { occuredid = Data.ID, eventtype = EventType.System.ToString() });
            linkMail.Text = "جزئیات اقدامات ایمیلی";
            linkMail.Target = HrefTarget._blank.ToString();
        }
        else
        {
            LinkMailCell.Visible = false;
        }


        if (new SMSSendAttemptBusiness().RetrieveByOccuredEvent(EventType.Schedule, Data.ID).Count() > 0)
        {
            LinkSMSCell.Visible = true;
            linkSMS.NavigateUrl = GetRouteUrl("admin-smsarchive", new { occuredid = Data.ID, eventtype = EventType.System.ToString() });
            linkSMS.Text = "جزئیات اقدامات پیامکی";
            linkSMS.Target = HrefTarget._blank.ToString();
        }
        else
        {
            LinkSMSCell.Visible = false;
        }

    }

    public override void EndMode(ManagementPageMode Mode)
    {
        (this.Page as BaseManagementPage<OccuredSystemEventBusiness, OccuredSystemEvent, OccuredSystemEventPattern, pgcEntities>).ListControl.Grid.DataBind();
        base.EndMode(Mode);
    }
}