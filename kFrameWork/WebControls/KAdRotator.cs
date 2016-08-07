using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Text;


namespace KAdRotator
{

    [DefaultBindingProperty("Text"), ToolboxData("<{0}:KAdRotator runat='server'></{0}:KAdRotator>")]
    public class KAdRotator : System.Web.UI.WebControls.AdRotator
    {
        private bool blnSWF;
        private string strImgUrl;
        private string strNavUrl;

        
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {

            if (this.blnSWF)
            {

                #region Old     

                //string Res = "";

                //string RelativePathRoot = GetRelativePathRoot(CurrentRelativePath);
                //ImgURL = RelativePathRoot + ImgURL.TrimStart('~', '\\');
                //ImgURL = ImgURL.Replace("\\", "/");

                //if (!ImgWidth.EndsWith("px"))
                //    ImgWidth += "px";

                //if (!ImgHeight.EndsWith("px"))
                //    ImgHeight += "px";

                //Res += "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" " + ((this.CssClass.Length > 0) ? "class=\"" + this.CssClass + "\" " : " ");
                //Res += "codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" ";
                //Res += "width=\"" + ImgWidth + "\" height=\"" + ImgHeight + "\">";
                //Res += "<param name=\"movie\" value=\"" + ImgURL + "\" />";
                //Res += "<param name=\"quality\" value=\"high\" />";
                //Res += "<!--[if !IE]> <-->";
                //Res += "<object data=\"" + ImgURL + "\" ";
                //Res += "width=\"" + ImgWidth + "\" height=\"" + ImgHeight + "\" type=\"application/x-shockwave-flash\">";
                //Res += "<param name=\"quality\" value=\"high\" />";
                //Res += "<param name=\"pluginurl\" value=\"http://www.macromedia.com/go/getflashplayer\" />";
                //Res += "برای نمایش این فایل باید پلاگین فلش را روی سیستم خود نصب کنید.";
                //Res += "</object>";
                //Res += "<!--> <![endif]-->";
                //Res += "</object>";

                //writer.Write(Res);

                #endregion

                #region New

                StringBuilder s = new StringBuilder();

                string RelativePathRoot = GetRelativePathRoot(CurrentRelativePath);
                ImgURL = RelativePathRoot + ImgURL.TrimStart('~', '\\');
                ImgURL = ImgURL.Replace("\\", "/");

                if (ImgWidth!= String.Empty && !ImgWidth.EndsWith("px"))
                    ImgWidth += "px";

                if (ImgHeight != String.Empty && !ImgHeight.EndsWith("px"))
                    ImgHeight += "px";

                s.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" align=\"center\"><tr><td>\n");
                s.AppendFormat("<div style=\"width:{0};height:{1};\" {2} >\n", (ImgWidth != string.Empty) ? ImgWidth : "200px", (ImgHeight != string.Empty) ? ImgHeight : "200px", ((this.CssClass.Length > 0) ? "class=\"" + this.CssClass + "\" " : " "));
                s.AppendFormat("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" {0} \n", ((this.CssClass.Length > 0) ? "class=\"" + this.CssClass + "\" " : " "));
                s.AppendFormat("codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" \n");
                s.AppendFormat("width=\"{0}\" height=\"{1}\">\n", (ImgWidth != string.Empty) ? ImgWidth : "200px", (ImgHeight != string.Empty) ? ImgHeight : "200px");
                s.AppendFormat("<param name=\"movie\" value=\"{0}\" />\n", ImgURL);
                s.AppendFormat("<param name=\"quality\" value=\"high\" />\n");
                //s.AppendFormat("<param name=\"menu\" value=\"false\" />\n");
                s.AppendFormat("<param name=\"wmode\" value=\"transparent\" />\n");
                s.AppendFormat("<!--[if !IE]> <-->\n");
                s.AppendFormat("<object data=\"{0}\" \n", ImgURL);
                s.AppendFormat("width=\"{0}\" height=\"{1}\" type=\"application/x-shockwave-flash\">\n", (ImgWidth != string.Empty) ? ImgWidth : "200px", (ImgHeight != string.Empty) ? ImgHeight : "200px");
                s.AppendFormat("<param name=\"quality\" value=\"high\" />\n");
                //s.AppendFormat("<param name=\"menu\" value=\"false\" />\n");
                s.AppendFormat("<param name=\"wmode\" value=\"transparent\" />\n");
                s.AppendFormat("<param name=\"pluginurl\" value=\"http://adobe.com/go/getflashplayer\" />\n");
                s.AppendFormat("<a href=\"{0}\" target=\"_blank\">{1}</a>\n",this.NvgURL,this.AltText);
                s.AppendFormat("</object>\n");
                s.AppendFormat("<!--> <![endif]-->\n");
                s.AppendFormat("</object>\n");
                s.AppendFormat("</div>\n</td></tr></table>\n");

                writer.Write(s.ToString());

                #endregion
            }
            else
            {
                base.Render(writer);
            }

        }

        protected override void OnAdCreated(AdCreatedEventArgs e)
        {
            base.OnAdCreated(e);

            if (e.ImageUrl.ToLower().IndexOf(".swf") != -1)
            {
                this.blnSWF = true;

                this.strImgUrl = e.ImageUrl; 
                this.strNavUrl = e.NavigateUrl;
                this.ImgWidth = e.AdProperties["Width"].ToString();
                this.ImgHeight = e.AdProperties["Height"].ToString();
                this.AltText = e.AlternateText;
            }
            else
            {
                this.blnSWF = false;
            }
        }

        private string strAltText;
        public string AltText
        {
            get { return strAltText; }
            set { strAltText = value; }
        }

        public string ImgURL
        {
            get { return strImgUrl; }
            set { strImgUrl = value; }
        }

        public string NvgURL
        {
            get { return strNavUrl; }
            set { strNavUrl = value; }
        }

        private string m_Width;

        public string ImgWidth
        {
            get { return m_Width; }
            set { m_Width = value; }
        }

        private string m_Height;

        public string ImgHeight
        {
            get { return m_Height; }
            set { m_Height = value; }
        }

        private string m_CurrentRelativePath;

        public string CurrentRelativePath
        {
            get { return m_CurrentRelativePath; }
            set { m_CurrentRelativePath = value; }
        }

        public string GetRelativePathRoot(string CurrentRelativePath)
        {
            int FolderLevel = CurrentRelativePath.Split('/').Length - 2;

            string Result = string.Empty;

            for (byte LevelCounter = 0; LevelCounter < FolderLevel; LevelCounter++)

                Result += "..\\";

            return Result;
        }
    }
}

