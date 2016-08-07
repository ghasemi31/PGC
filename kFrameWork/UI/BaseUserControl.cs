using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;

namespace kFrameWork.UI
{
    public class BaseUserControl:UserControl
    {
        #region Attachments handling

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            queryAttachments();
        }

        private void queryAttachments()
        {
            //string currentPath = this.AppRelativeVirtualPath;
            //string currentPath_Css_Min = currentPath.Replace("~", "~/styles").Replace(".ascx", ".min.css");
            //string currentPath_Js_Min = currentPath.Replace("~", "~/scripts").Replace(".ascx", ".min.js");
            //string currentPath_Css = currentPath_Css_Min.Replace(".min", "");
            //string currentPath_Js = currentPath_Js_Min.Replace(".min", "");

            //if ((this.Page as BasePage).IsAttachmentValid(currentPath_Css_Min))
            //    (this.Page as BasePage).AddCssReservation(currentPath_Css_Min);
            //else if ((this.Page as BasePage).IsAttachmentValid(currentPath_Css))
            //    (this.Page as BasePage).AddCssReservation(currentPath_Css);

            //if ((this.Page as BasePage).IsAttachmentValid(currentPath_Js_Min))
            //    (this.Page as BasePage).AddJsReservation(currentPath_Js_Min);
            //else if ((this.Page as BasePage).IsAttachmentValid(currentPath_Js))
            //    (this.Page as BasePage).AddJsReservation(currentPath_Js);
        }

        #endregion
    }
}
