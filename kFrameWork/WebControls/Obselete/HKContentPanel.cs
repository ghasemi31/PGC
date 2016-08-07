using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kFrameWork.WebControls
{
    public class HKContentPanel : Panel
    {
        public HKContentPanel()
        {
            ToggleDuration = 400;
            Title = "لیست";
            IconClass = "list";
        }

        public int ToggleDuration { get; set; }
        public string Title { get; set; }
        public string IconClass { get; set; }
        public string LeftTitle { get; set; }

        public bool _DefaultDisplay = true;
        public bool DefaultDisplay
        {
            get { return _DefaultDisplay; }
            set { _DefaultDisplay = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            PlaceHolder PreContent = this.Controls[1] as PlaceHolder;
            PlaceHolder PostContent = this.Controls[this.Controls.Count - 2] as PlaceHolder;

            if (PreContent != null && PostContent != null)
            {
                PreContent.Controls.Add(new LiteralControl("<div class='dtpnlhead" + ((DefaultDisplay) ? "" : "_hid") + "'><div class='dtpanelheadicon_" + IconClass + "'></div><h5><div class='dtpanelheadtitle_right'>" + Title + "</div><div class='dtpanelheadtitle_left'>" + LeftTitle + "</div></h5></div><div class='dtpnlcontent' " + ((DefaultDisplay) ? "" : "style=\"display:none\"") + ">" + ((IconClass == "list") ? "<div class='grid'>" : "")));
                PostContent.Controls.Add(new LiteralControl(((IconClass == "list") ? "</div>" : "") + "</div>"));

                string scr = @"function togglePanel() {
                    $('.dtpnlhead,.dtpnlhead_hid').click(function () {
                        $(this).toggleClass('dtpnlhead dtpnlhead_hid');
                        $(this).next().slideToggle(" + ToggleDuration + @");
                        try{onTogglePanel();}catch(e){}
                    });
                }";

                this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "togglePanel", scr, true);
            }
        }
    }
}