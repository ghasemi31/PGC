using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pgc.Business.Core;

public partial class Pages_Relay : System.Web.UI.Page
{
    enum RequestType
    {
        RecieveMessage = 1,
        UpdateStatus = 2
    }

    private RequestType ReqType;

    private string From;
    private string To;
    private string Text;

    private long MessageID;
    private int Status;
    private int Count;
    private DateTime EnteredDate;



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!ValidateRequest())
        {
            RelayBusiness.Log("Invalid Request", Request, null, From, To, Text, MessageID, Status, Count, EnteredDate);
            return;
        }

        if (ReqType == RequestType.RecieveMessage)
        {
            if (RelayBusiness.ReciveMessage(From, To, Text))
            {
                Response.Write("successfully recived message");
                return;
            }
            else
            {
                Response.Write("failed to recive message, refer to log ...");
                return;
            }
        }
        else if (ReqType == RequestType.UpdateStatus)
        {
            if (RelayBusiness.UpdateStatus(MessageID, Status, Count, EnteredDate))
            {
                Response.Write("successfully updated status");
                return;
            }
            else
            {
                Response.Write("failed to update status, refer to log ...");
                return;
            }
        }
    }

    private bool ValidateRequest()
    {
        if (Request.QueryString["type"] == null ||
            (Request.QueryString["type"] != "1" && Request.QueryString["type"] != "2"))
        {
            Response.Write("no type detected");
            return false;
        }

        if (Request.QueryString["type"] == "1")
        {
            ReqType = RequestType.RecieveMessage;
        }
        else if (Request.QueryString["type"] == "2")
        {
            ReqType = RequestType.UpdateStatus;
        }

        if (ReqType == RequestType.RecieveMessage)
        {
            if (Request.QueryString["from"] == null ||
                Request.QueryString["to"] == null ||
                Request.QueryString["text"] == null)
            {
                Response.Write("invalid data");
                return false;
            }
            else
            {
                From = Request.QueryString["from"];
                To = Request.QueryString["to"];
                Text = Request.QueryString["text"];
            }
        }
        else if (ReqType == RequestType.UpdateStatus)
        {
            if (Request.QueryString["id"] == null ||
                Request.QueryString["status"] == null)
            {
                Response.Write("invalid data");
                return false;
            }
            else
            {
                try
                {
                    MessageID = Convert.ToInt64(Request.QueryString["id"]);
                }
                catch (Exception Ex)
                {
                    Response.Write("invalid MessageId");
                    ExceptionHandler.HandleManualException(Ex, "Pages.Relay.aspx.ValidateRequest.invalid-MessageId");
                    return false;
                }

                if (MessageID <= 0)
                {
                    Response.Write("invalid MessageId");
                    return false;
                }

                try
                {
                    Status = Convert.ToInt32(Request.QueryString["status"]);
                }
                catch (Exception Ex)
                {
                    Response.Write("invalid Status");
                    ExceptionHandler.HandleManualException(Ex, "Pages.Relay.aspx.ValidateRequest.invalid-Status");
                    return false;
                }

                try
                {
                    Count = Convert.ToInt32(Request.QueryString["count"]);
                }
                catch
                {
                    Count = 1;
                }

                try
                {
                    EnteredDate = Convert.ToDateTime(Request.QueryString["date"]);
                }
                catch
                {
                    EnteredDate = DateTime.Now;
                }
            }
        }

        return true;
    }
}