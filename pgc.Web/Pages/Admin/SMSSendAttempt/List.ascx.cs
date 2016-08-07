using System;
using kFrameWork.UI;
using System.Web.UI;
using pgc.Model.Enums;
using pgc.Business;
using kFrameWork.Util;
using System.Web.UI.WebControls;

public partial class Pages_Admin_SMSSendAttempt_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        
        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }

    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        int cellEventTitle = 4;
        int cellDate = 8;

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            EventType type = (EventType)DataBinder.Eval(e.Row.DataItem, "EventType");

            if (type == EventType.Manual)
            { }
            //else if (type == EventType.Schedule)
            //{
            //    if (DataBinder.Eval(e.Row.DataItem, "OccuredEvent_ID") != null)
            //    {
            //        long id = (long)DataBinder.Eval(e.Row.DataItem, "OccuredEvent_ID");
            //        e.Row.Cells[cellEventTitle].Text = new OccuredScheduleEventBusiness().Retrieve(id).ScheduleEvent.Title;
            //    }
            //    else
            //        e.Row.Cells[cellEventTitle].Text = "ارسال توسط مدیر/دستی";
            //}
            //else if (type == EventType.System)
            if (type == EventType.System)
            {
                if (DataBinder.Eval(e.Row.DataItem, "OccuredEvent_ID") != null)
                {
                    long id = (long)DataBinder.Eval(e.Row.DataItem, "OccuredEvent_ID");
                    e.Row.Cells[cellEventTitle].Text = new OccuredSystemEventBusiness().Retrieve(id).SystemEvent.Title;
                }
                else
                    e.Row.Cells[cellEventTitle].Text = "ارسال توسط مدیر/دستی";
            }

            e.Row.Cells[cellDate].Text = DateUtil.GetPersianDateWithTime((DateTime)DataBinder.Eval(e.Row.DataItem, "Date"));
        }
    }

    protected void btnEvent_Click(object sender, EventArgs e)
    {
        if ((this.Page as BasePage).HasValidQueryString<EventType>(QueryStringKeys.eventtype) &&
                       (this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.occuredid))
        {
            EventType type = (EventType)Enum.Parse(typeof(EventType), (this.Page as BasePage).GetQueryStringValue<string>(QueryStringKeys.eventtype));

            //if (type == EventType.Schedule)
            //{
            //    Response.Redirect(GetRouteUrl("admin-occuredscheduleevent", null));
            //}
            //else if (type == EventType.System)
            if (type == EventType.System)
            {
                Response.Redirect(GetRouteUrl("admin-occuredsystemevent", null));
            }
        }
    }
}