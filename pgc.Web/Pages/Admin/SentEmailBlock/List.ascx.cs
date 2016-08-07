using System;
using kFrameWork.UI;
using System.Web.UI;
using kFrameWork.Util;
using pgc.Model.Enums;

public partial class Pages_Admin_SentEmailBlock_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }
    protected  void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        int cellRecipient= 4;
        int cellDate = 5;
        int cellStatus = 6;

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            e.Row.Cells[cellRecipient].Style.Add("direction", "ltr");
            e.Row.Cells[cellDate].Text = DateUtil.GetPersianDateWithTime((DateTime)DataBinder.Eval(e.Row.DataItem, "Date"));
            e.Row.Cells[cellStatus].Text= ((bool)DataBinder.Eval(e.Row.DataItem,"IsSent"))?"ارسال شده":"ارسال نشده";
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.emailattemptid))
        {
            if ((this.Page as BasePage).HasValidQueryString<EventType>(QueryStringKeys.eventtype) &&
                (this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.occuredid))
            {
                EventType type = (EventType)Enum.Parse(typeof(EventType), (this.Page as BasePage).GetQueryStringValue<string>(QueryStringKeys.eventtype));
                long OccuredEventID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.occuredid);

                Response.Redirect(GetRouteUrl("admin-emailarchive", new { occuredid = OccuredEventID, eventtype = type }));
            }
            else
                Response.Redirect(GetRouteUrl("admin-emailarchive", null));
        }
    }
}