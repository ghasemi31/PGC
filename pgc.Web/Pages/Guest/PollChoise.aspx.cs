using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model.Enums;
using pgc.Model;
using kFrameWork.Util;
using pgc.Business.General;
using kFrameWork.Model;



public partial class Pages_Guest_PollChoise : BasePage
{
    public PollBusiness business = new PollBusiness();

    public Poll poll;
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasValidQueryString_Routed<long>(QueryStringKeys.id))
            Server.Transfer("~/Pages/Guest/404.aspx");

        poll = business.RetrivePoll(GetQueryStringValue_Routed<long>(QueryStringKeys.id));

        if (poll == null)
            Server.Transfer("~/Pages/Guest/404.aspx");

        if (poll.IsActive == false)
            Server.Transfer("~/Pages/Guest/PollList.aspx");

        if (!this.IsPostBack)
        {
            rblChoises.DataTextField = "Description";
            rblChoises.DataValueField = "ID";
            rblChoises.DataSource = poll.PollChoises;
            rblChoises.DataBind();
        }

        //foreach (PollChoise choise in poll.PollChoises)
        //{
        //    rblChoises.Items.Add(new ListItem()
        //    {
        //        Text = choise.Description,
        //        Value = choise.ID.ToString(),

        //    });
        //}
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PollResult pollResult = new PollResult();

        OperationResult result = new OperationResult();
        if (string.IsNullOrEmpty(rblChoises.SelectedValue))
        {
            UserSession.AddMessage(UserMessageKey.Failed);
            return;                
        }
        pollResult.PollChoise_ID = Convert.ToInt64(rblChoises.SelectedValue);
        pollResult.Poll_ID = GetQueryStringValue_Routed<long>(QueryStringKeys.id);
        string IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

        //HttpRequest currentRequest = HttpContext.Current.Request;
        //string IPAddress=currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //string IPAddress=HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
       
        

        result = business.RegisterPoll(pollResult, IPAddress);
        UserSession.AddMessage(result.Messages);

        if (result.Result == ActionResult.Done)
        {
            Response.Redirect(GetRouteUrl("guest-polllist", null));
        }
        
    }
}