using System;
using kFrameWork.UI;
using System.Web.UI;
using pgc.Business;
using pgc.Model;
using kFrameWork.Util;
using pgc.Model.Enums;

public partial class Pages_Admin_SentMessage_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }
    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        int cellDate = 8;
        int cellSendStatus = 5;
        int cellFaultType = 6;

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.Header)
            e.Row.Cells[cellFaultType].Visible = false;

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            e.Row.Cells[cellDate].Text = DateUtil.GetPersianDateWithTime((DateTime)DataBinder.Eval(e.Row.DataItem, "Date"));

            long ID = (long)DataBinder.Eval(e.Row.DataItem, "ID");
            SentSMS sms = new SentSMSBusiness().Retrieve(ID);

            if (sms.GatewayMessageID.HasValue)
            {
                e.Row.Cells[cellSendStatus].Visible = true;
                e.Row.Cells[cellFaultType].Visible = false;
            }
            else if (sms.FaultType.HasValue)
            {
                e.Row.Cells[cellSendStatus].Visible = false;
                e.Row.Cells[cellFaultType].Visible = true;
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.AlternateText= string.Format("کد خطا:{0}", sms.FaultType.Value);
                img.ToolTip = img.AlternateText;
                img.ImageUrl = ResolveUrl("~/Styles/Images/sendresult_fail.png");
                img.Height = 20;
                img.Width = 20;


                e.Row.Cells[cellFaultType].Controls.Add(img);
            }
            else
            {
                e.Row.Cells[cellSendStatus].Visible = false;
                e.Row.Cells[cellFaultType].Visible = true;
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.AlternateText = "بروز خطا";
                img.ToolTip = img.AlternateText;
                img.ImageUrl = ResolveUrl("~/Styles/Images/sendresult_fail.png");
                img.Height = 20;
                img.Width = 20;


                e.Row.Cells[cellFaultType].Controls.Add(img);
            }
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.smsattemptid))
        {
            if ((this.Page as BasePage).HasValidQueryString<EventType>(QueryStringKeys.eventtype) &&
                (this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.occuredid))
            {
                EventType type = (EventType)Enum.Parse(typeof(EventType), (this.Page as BasePage).GetQueryStringValue<string>(QueryStringKeys.eventtype));
                long OccuredEventID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.occuredid);

                Response.Redirect(GetRouteUrl("admin-smsarchive", new { occuredid = OccuredEventID, eventtype = type }));
            }
            else
                Response.Redirect(GetRouteUrl("admin-smsarchive", null));
        }
    }
}