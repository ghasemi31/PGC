using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using kFrameWork.UI;
using pgc.Model.Enums;
using pgc.Model;
using kFrameWork.Business;



public partial class UserControls_Common__UserMessageViewer : BaseUserControl
{
    
    LiteralControl pnlMessage = new LiteralControl();

    protected override void OnInit(EventArgs e)
    {
        
        base.OnInit(e);
        if (ScriptManager.GetCurrent(this.Page) == null)
        {
            this.Controls.Add(new LiteralControl("<div  onclick=\"$.unblockUI();\" id=\"usermessage\" style=\"display:none;\"><div onclick=\"$.unblockUI();\" class=\"cross\" title=\"بستن\"></div><div class=\"spacer\">&nbsp;</div>"));
            this.Controls.Add(pnlMessage);
            this.Controls.Add(new LiteralControl("</div>"));
        }
        else
        {
            UpdatePanel CustomUpdatePanel = new UpdatePanel() { ID = "updPanel" };
            this.Controls.Add(CustomUpdatePanel);
            CustomUpdatePanel.ContentTemplateContainer.Controls.Add(new LiteralControl("<div onclick=\"$.unblockUI();\" id=\"usermessage\" style=\"display:none;\"><div onclick=\"$.unblockUI();\" class=\"cross\" title=\"بستن\"></div><div class=\"spacer\">&nbsp;</div>"));
            CustomUpdatePanel.ContentTemplateContainer.Controls.Add(pnlMessage);
            CustomUpdatePanel.ContentTemplateContainer.Controls.Add(new LiteralControl("</div>"));
        }
        
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        List<UserMessageKey> TempMessages = UserSession.CurrentMessages;
        
        foreach (UserMessageKey key in TempMessages)
        {
            UserMessage LoadedMessage = LoadMessage(key.ToString());

            string MessageIcon = "<div class=\"" + ((UserMessageType)LoadedMessage.Type).ToString().ToLower() + "\"></div>";
            string MessageText = "<div class=\"text\">" + LoadedMessage.Description + "</div>";

            pnlMessage.Text += "<tr>";
            pnlMessage.Text += "<td class=\"iconcell\">";
            pnlMessage.Text += MessageIcon;
            pnlMessage.Text += "</td>";
            pnlMessage.Text += "<td class=\"textcell\">";
            pnlMessage.Text += MessageText;
            pnlMessage.Text += "</td>";
            pnlMessage.Text += "</tr>";
        }

        if (!string.IsNullOrEmpty(pnlMessage.Text))
        {
            pnlMessage.Text = "<table dir='rtl'>" + pnlMessage.Text + "</table>";
        }

        pnlMessage.Text = "<div class=\"text\" dir='rtl'>" + pnlMessage.Text + "</div>";

        if (UserSession.CurrentData.Count > 0)
        {
            string strData = "";

            foreach (object Key in UserSession.CurrentData.Keys)
            {
                strData += "<tr>";
                strData += "<td>";
                strData += "<div class=\"bullet-data\"></div>";
                strData += "</td>";
                strData += "<td>";
                if (Key != null)
                    strData += "<div class=\"datakey\">" + Key.ToString() + "</div>";
                strData += "</td>";
                strData += "<td>";
                if (UserSession.CurrentData[Key] != null)
                    strData += "<div class=\"datavalue\">" + UserSession.CurrentData[Key].ToString() + "</div>";
                strData += "</td>";
                strData += "</tr>";
            }

            strData = "<table dir='rtl'>" + strData + "</table>";
            strData = "<div class=\"text\" dir='rtl'>" + strData + "</div>";

            pnlMessage.Text = pnlMessage.Text + strData;
        }

        UserSession.CurrentData = null;

        UserSession.ClearMessages();
    }

    private UserMessage LoadMessage(string key)
    {
        pgcEntities db= new pgcEntities();
        UserMessage Result = db.UserMessages.SingleOrDefault(u => u.Key == key);
        if (key == UserMessageKey.OnlinePaymentSucceedText.ToString())
        {
            Result.Description = OptionBusiness.GetHtml(OptionKey.OnlinePaymentSucceedText);
        }
        else if (key == UserMessageKey.OnlineOrderIsSuspended.ToString())
        {
            Result.Description = OptionBusiness.GetHtml(OptionKey.MessageForOnlineOrderIsSuspended);
        }
        return Result;
    }

}
