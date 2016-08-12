using System;
using System.Configuration;
using kFrameWork.Enums;
using kFrameWork.Model;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using pgc.Model;
using pgc.Model.Enums;
using kFrameWork.Util;

namespace kFrameWork.UI
{
    public class BasePage:System.Web.UI.Page
    {
        #region Security
        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);
            securityCheck();
            

        }
        private void securityCheck()
        {
            pgcEntities db = new pgcEntities();
            Entity = db.PanelPages.FirstOrDefault(p => p.URL == this.AppRelativeVirtualPath&&p.Active);

            if (this.IsGuestPage)
                return;

            if (!UserSession.IsUserLogined)
            {
                if (!this.IsLookup)
                    CustomRedirect(GetRouteUrl("guest-login", null) + "?redirecturl=" + this.AppRelativeVirtualPath, UserMessageKey.SessionExpired);
                else
                    CustomRedirect(GetRouteUrl("guest-login", null), UserMessageKey.SessionExpired);
            }
            else
            {
                if (!UserSession.User.AccessLevel.Features.Any(f => f.ID == Entity.Feature_ID))
                    CustomRedirect(GetRouteUrl(DefaultRouteName,null), UserMessageKey.AccessDenied);
            }
        }
        public const string DefaultRouteName = "guest-default";
        public PanelPage Entity { get; set; }
        public bool IsLookup
        {
            get
            {
                //return this.AppRelativeTemplateSourceDirectory.ToLower().Contains("/lookups/");
                return Entity.IsLookup;
            }
        }
        public bool IsGuestPage
        {
            get
            {
                return (Entity.Feature_ID == null);
            }
        }
        public void CustomRedirect(string RedirectUrl, params UserMessageKey[] MessageKeys)
        {
            foreach (UserMessageKey MessageKey in MessageKeys)
                UserSession.AddMessage(MessageKey);

            if (this.IsLookup)
            {
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(),
                    "CloseScript",

                        @"  if (top === self){
                                window.location = '" + this.ResolveClientUrl(RedirectUrl) + @"';
                            }
                            else{
                                window.parent.location.reload(true);
                                //alert(window.parent.location);
                            }",
                    true);

            }
            else
            {
                

                if (!string.IsNullOrEmpty(RedirectUrl))
                    Response.Redirect(RedirectUrl);
            }
        }

        #endregion

        #region Exception Handling

        protected void Page_Error(object sender, EventArgs e)
        {
            //page level exception handling 
            //ExceptionHandler.HandleBasePageException(Server.GetLastError(), this);
        }

        #endregion

        #region QueryStrings

        public bool HasValidQueryString<T>(QueryStringKeys Key)
        {
            string Value = this.Request.QueryString.Get(Key.ToString());

            if (string.IsNullOrEmpty(Value))
                return false;

            return true;
        }
        public T GetQueryStringValue<T>(QueryStringKeys Key)
        {
            return ConvertorUtil.Convert<T>(this.Request.QueryString.Get(Key.ToString()));
        }

        public bool HasValidQueryString_Routed<T>(QueryStringKeys Key)
        {
            object Value = RouteData.Values[Key.ToString()];

            if (Value == null || Value.ToString() == "")
                return false;

            return true;
        }
        public T GetQueryStringValue_Routed<T>(QueryStringKeys Key)
        {
            return ConvertorUtil.Convert<T>(RouteData.Values[Key.ToString()]);
        }

        #endregion

        #region Attachments handling

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.Page.Header.DataBind();
        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);
            queryAttachments();
            handleAttachments();
            
        }

        private void queryAttachments()
        {
            #region currrent handling

            //string currentPath = Request.AppRelativeCurrentExecutionFilePath;
            string currentPath = this.AppRelativeVirtualPath;
            string currentPath_Css_Min = currentPath.Replace("~", "~/styles").Replace(".aspx", ".min.css");
            string currentPath_Js_Min = currentPath.Replace("~", "~/scripts").Replace(".aspx", ".min.js");
            string currentPath_Css = currentPath_Css_Min.Replace(".min", "");
            string currentPath_Js = currentPath_Js_Min.Replace(".min", "");

            if (IsAttachmentValid(currentPath_Css_Min))
                AddCssReservation(currentPath_Css_Min);
            else if (IsAttachmentValid(currentPath_Css))
                AddCssReservation(currentPath_Css);

            if (IsAttachmentValid(currentPath_Js_Min))
                AddJsReservation(currentPath_Js_Min);
            else if (IsAttachmentValid(currentPath_Js))
                AddJsReservation(currentPath_Js);

            #endregion
            #region masters handling

            MasterPage master = this.Page.Master;
            while (master != null)
            {
                queryMaster(master);
                master = master.Master;
            }

            #endregion
            #region BaseUserControls
            queryControls(this.Controls);
            #endregion
        }
        public void queryControls(ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is BaseUserControl)
                {
                    string currentPath = (control as UserControl).AppRelativeVirtualPath;
                    string currentPath_Css_Min = currentPath.Replace("~", "~/styles").Replace(".ascx", ".min.css");
                    string currentPath_Js_Min = currentPath.Replace("~", "~/scripts").Replace(".ascx", ".min.js");
                    string currentPath_Css = currentPath_Css_Min.Replace(".min", "");
                    string currentPath_Js = currentPath_Js_Min.Replace(".min", "");

                    if (IsAttachmentValid(currentPath_Css_Min))
                        AddCssReservation(currentPath_Css_Min);
                    else if (IsAttachmentValid(currentPath_Css))
                        AddCssReservation(currentPath_Css);

                    if (IsAttachmentValid(currentPath_Js_Min))
                        AddJsReservation(currentPath_Js_Min);
                    else if (IsAttachmentValid(currentPath_Js))
                        AddJsReservation(currentPath_Js);
                }
                queryControls(control.Controls);
            }
        }
        public void queryMaster(MasterPage master)
        {
            string master_Css_Min = "";
            string master_Css = "";
            string master_Js_Min = "";
            string master_Js = "";

            master_Css_Min = master.AppRelativeVirtualPath.Replace("~", "~/styles").Replace(".master", ".min.css");
            master_Css = master_Css_Min.Replace(".min", "");
            master_Js_Min = master.AppRelativeVirtualPath.Replace("~", "~/scripts").Replace(".master", ".min.js");
            master_Js = master_Js_Min.Replace(".min", "");

            if (IsAttachmentValid(master_Css_Min))
                AddCssReservation(master_Css_Min);
            else if (IsAttachmentValid(master_Css))
                AddCssReservation(master_Css);

            if (IsAttachmentValid(master_Js_Min))
                AddJsReservation(master_Js_Min);
            else if (IsAttachmentValid(master_Js))
                AddJsReservation(master_Js);
        }
        public bool IsAttachmentValid(string relative_path)
        {
            return File.Exists(Server.MapPath(relative_path));
        }
        public void AddCssReservation(string path)
        {
            if (!css_files.Contains(path))
                css_files.Add(path + "?v=2.2");
        }
        public void AddJsReservation(string path)
        {
            if (!js_files.Contains(path))
                js_files.Insert(0, path + "?v=2.2");
        }

        public List<string> css_files = new List<string>();
        public List<string> js_files = new List<string>();
        private void handleAttachments()
        {
            foreach (string css in css_files)
            {
                AddCssAttachment(css);
            }
            foreach (string js in js_files)
            {
                AddJsAttachment(js);
            }
        }
        private void AddCssAttachment(string path)
        {
            HtmlLink css_file = new HtmlLink()
            {
                Href = path,
            };
            css_file.Attributes.Add("rel", "stylesheet");
            css_file.Attributes.Add("type", "text/css");
            Page.Header.Controls.Add(css_file);
        }
        private void AddJsAttachment(string path)
        {
            LiteralControl javascriptRef = new LiteralControl("<script type='text/javascript' src='" + ResolveClientUrl(path)  + "'></script>");
            Page.Header.Controls.Add(javascriptRef);
        }

        #endregion

        public static void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            //routes.MapPageRoute(
            //    "adsearch",
            //    "search/{ProductType}/{*Params}",
            //    "~/AdvancedSearch.aspx"
            //    );

            //routes.MapPageRoute(
            //    "services",
            //    "services",
            //    "~/pages/guest/services.aspx"
            //    );

            pgcEntities db = new pgcEntities();
            List<PanelPage> pages = db.PanelPages.Where(p => p.RouteName != "").ToList();
            foreach(PanelPage page in pages)
            {
                routes.MapPageRoute(
                    page.RouteName,
                    page.RouteUrl,
                    page.URL);
            }
        }
    }
}