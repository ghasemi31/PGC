using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kFrameWork.WebControls
{
    public class HKSearchPanel : Panel
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //PlaceHolder PreContent = this.FindControl("plhPreContent") as PlaceHolder;
            //PlaceHolder PostContent = this.FindControl("plhPostContent") as PlaceHolder;

            PlaceHolder PreContent = this.Controls[1] as PlaceHolder;
            PlaceHolder PostContent = this.Controls[this.Controls.Count - 2] as PlaceHolder;

            if (PreContent != null && PostContent != null)
            {
//                bool CurrentToggleState = false;
//                if (HttpContext.Current.Request.Form[this.NamingContainer.UniqueID + "$" + "hdfSearchToggle"] != null)
//                    Boolean.TryParse(HttpContext.Current.Request.Form[this.NamingContainer.UniqueID + "$" + "hdfSearchToggle"], out CurrentToggleState);

//                PreContent.Controls.Add(new LiteralControl("<div class='searchpanel'><div class='content' style='display:" + ((CurrentToggleState) ? "block" : "none") + "'" + ">"));
//                //PreContent.Controls.Add(new LiteralControl("<div class='searchpanel'><div class='content' style='display:" + ((CurrentToggleState) ? "block" : "block") + "'" + ">"));
//                PostContent.Controls.Add(new LiteralControl("</div><div id='togglebutton' class='toggle" + ((CurrentToggleState) ? "On" : "") + "'>"));
//                PostContent.Controls.Add(new HiddenField() { ID = "hdfSearchToggle", Value = CurrentToggleState.ToString().ToLower() });
//                PostContent.Controls.Add(new LiteralControl("</div></div>"));

//                string scr = @"function OnSearchContentPageLoad() {
//                    $('#togglebutton').click(function () {
//                        if ($('.searchpanel .content').css('display') == 'none') { $('#" + this.NamingContainer.ClientID + "_" + "hdfSearchToggle" + @"').val('true'); }
//                        else { $('#" + this.NamingContainer.ClientID + "_" + "hdfSearchToggle" + @"').val('false'); }
//                        $('#togglebutton').toggleClass('toggle toggleOn');
//                        $('.searchpanel .content').slideToggle(600);
//                        try{onTogglePanel();}catch(e){}
//                    });
//                }";

//                this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "searchtoggle", scr, true);

                PreContent.Controls.Add(new LiteralControl("<div class='searchpanel'>"));
                PostContent.Controls.Add(new LiteralControl("</div>"));
            }
        }
    }
}