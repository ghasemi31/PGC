using System;
using kFrameWork.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
using kFrameWork.Util;
using pgc.Business;
using pgc.Model.Enums;
using System.Linq;

public partial class Pages_Admin_OccuredSystemEvent_List : BaseListControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }

    protected void grdList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        int cellEventTitle = 3;
        //int cellActor = 4;
        int cellDate = 5;
        int cellDevice = 7;
        int cellMail= 8;
        int cellSMS= 9;
        

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            e.Row.Cells[cellEventTitle].Text = new SystemEventBusiness().Retrieve((long)DataBinder.Eval(e.Row.DataItem, "SystemEvent_ID")).Title;
            //e.Row.Cells[cellActor].Text = DataBinder.Eval(e.Row.DataItem, "Actor").ToString();
            e.Row.Cells[cellDate].Text = DateUtil.GetPersianDateWithTime((DateTime)DataBinder.Eval(e.Row.DataItem, "Date"));
            Device status = (Device)Enum.Parse(typeof(Device), DataBinder.Eval(e.Row.DataItem, "DeviceType_Enum").ToString());
            if (status==Device.WebApp)
            {
                e.Row.Cells[cellDevice].Text = "<i class='fa fa-internet-explorer' aria-hidden='true' style='font-size:1.5em;color:#2196F3'></i>";
                
            }
            if (status == Device.AndroidApp)
            {
                e.Row.Cells[cellDevice].Text = "<i class='fa fa-android' aria-hidden='true'style='font-size:1.5em;color:green'></i>";
            }
            if (status == Device.IOSApp)
            {
                e.Row.Cells[cellDevice].Text = "<i class='fa fa-apple' aria-hidden='true'style='font-size:1.5em;color:#A29C9C'></i>";
            }
            e.Row.Cells[cellDevice].Style.Add("text-align", "center");

            if (new EmailSendAttemptBusiness().RetrieveByOccuredEvent(EventType.System, (long)DataBinder.Eval(e.Row.DataItem, "ID")).Count() > 0)
            {
                HyperLink HyperLinkMail = new HyperLink()
                    {
                       
                        NavigateUrl = GetRouteUrl("admin-emailarchive", new { occuredid = DataBinder.Eval(e.Row.DataItem, "ID").ToString(), eventtype = EventType.System.ToString() }),
                        //Target = "_blank",
                        Text = "<i class='fa fa-envelope' aria-hidden='true' style='font-size:1.5em;color:#02820D'></i>"
                    };

                e.Row.Cells[cellMail].Controls.Add(HyperLinkMail);
                e.Row.Cells[cellMail].Style.Add("text-align", "center");                
            }
            else
            {
                e.Row.Cells[cellMail].Text = "<i class='fa fa-times' aria-hidden='true' style='font-size:1.5em;color:#B31918'></i>";
                e.Row.Cells[cellMail].Style.Add("text-align", "center");
            }

            if (new SMSSendAttemptBusiness().RetrieveByOccuredEvent(EventType.System, (long)DataBinder.Eval(e.Row.DataItem, "ID")).Count() > 0)
            {
                HyperLink HyperLinkSMS = new HyperLink()
                    {
                       
                        NavigateUrl = GetRouteUrl("admin-smsarchive", new { occuredid = DataBinder.Eval(e.Row.DataItem, "ID").ToString(), eventtype = EventType.System.ToString() }),
                        //Target = "_blank",
                        Text = "<i class='fa fa-paper-plane' aria-hidden='true' style='font-size:1.5em;color:#02820D'></i>"

                    };
                e.Row.Cells[cellSMS].Controls.Add(HyperLinkSMS);
                e.Row.Cells[cellSMS].Style.Add("text-align", "center");
            }
            else
            {
                e.Row.Cells[cellSMS].Text = "<i class='fa fa-times' aria-hidden='true' style='font-size:1.5em;color:#B31918'></i>";
                e.Row.Cells[cellSMS].Style.Add("text-align", "center");
            }
           
        }
    }
}