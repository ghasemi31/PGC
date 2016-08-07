using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace kFrameWork.Util
{
    public class UIUtil
    {
        #region Numeric Functiaonlities

        private static string[] yakan = new string[10] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        private static string[] dahgan = new string[10] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        private static string[] dahyek = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        private static string[] sadgan = new string[10] { "", "یکصد", "دوصد", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        private static string[] basex = new string[5] { "", "هزار", "میلیون", "میلیارد", "تریلیون" };

        private static string getnum3(int num3)
        {
            string s = "";
            int d3, d12;
            d12 = num3 % 100;
            d3 = num3 / 100;
            if (d3 != 0)
                s = sadgan[d3] + " و ";
            if ((d12 >= 10) && (d12 <= 19))
            {
                s = s + dahyek[d12 - 10];
            }
            else
            {
                int d2 = d12 / 10;
                if (d2 != 0)
                    s = s + dahgan[d2] + " و ";
                int d1 = d12 % 10;
                if (d1 != 0)
                    s = s + yakan[d1] + " و ";
                s = s.Substring(0, s.Length - 3);
            };
            return s;
        }

        public static string GetLetterOf(string snum)
        {
            string stotal = "";
            if (snum == "0")
            {
                return yakan[0];
            }
            else
            {
                snum = snum.PadLeft(((snum.Length - 1) / 3 + 1) * 3, '0');
                int L = snum.Length / 3 - 1;
                for (int i = 0; i <= L; i++)
                {
                    int b = int.Parse(snum.Substring(i * 3, 3));
                    if (b != 0)
                        stotal = stotal + getnum3(b) + " " + basex[L - i] + " و ";
                }
                stotal = stotal.Substring(0, stotal.Length - 3);
            }
            return stotal;
        }

        public static string GetCommaSeparatedOf(string PlainFormat)
        {
            if (PlainFormat == null)

                return string.Empty;

            int Len = PlainFormat.Length;

            if (PlainFormat.Split('.').Length == 2)
                return PlainFormat;

            for (int c = 0; c < (Len / 3); c++)
            {
                PlainFormat = PlainFormat.Insert((c + (Len % 3) + c * 3), ",");
            }
            return PlainFormat.Trim(',');
        }

        public static string GetCommaSeparatedOf(long Number)
        {
            return GetCommaSeparatedOf(Number.ToString());
        }

        public static string GetCommaSeparatedOf(decimal Number)
        {
            return GetCommaSeparatedOf(Number.ToString());
        }

        public static string GetCommaSeparatedOf(float Number)
        {
            return GetCommaSeparatedOf(Number.ToString());
        }

        #endregion Numeric Functiaonlities


        public const string Styles_Folder = "Styles";
        public const string Scripts_Folder = "Scripts";
        public const string Default_Namespace = "kFrameWork";
        public enum AttachmentType
        {
            css,
            css_min,
            js,
            js_min

        }
        public static string GetAttachmentUrl(Control control,AttachmentType type)
        {
            string main  = "";
            if (type == AttachmentType.css || type == AttachmentType.css_min)
                main = Default_Namespace + "." + Styles_Folder + "." + control.GetType().Name;
            if (type == AttachmentType.js || type == AttachmentType.js_min)
                main = Default_Namespace + "." + Scripts_Folder + "." + control.GetType().Name;

            switch (type)
            {
                case AttachmentType.css: main = main + ".css"; break;
                case AttachmentType.css_min: main = main + ".min.css"; break;
                case AttachmentType.js: main = main + ".js"; break;
                case AttachmentType.js_min: main = main + ".min.js"; break;
            }
            return control.Page.ClientScript.GetWebResourceUrl(control.GetType(), main);
        }
        
        public static void ClearControls(ControlCollection controls)
        {
            //string str = ;
            foreach (Control control in controls)
            {
                switch (control.GetType().Name)
                {
                    case "TextBox": (control as TextBox).Text = ""; break;
                    case "HKTextBox":(control as TextBox).Text = "";break;
                    case "CheckBox": (control as CheckBox).Checked = false; break;
                    case "DropDownList":(control as DropDownList).SelectedIndex = -1; break;
                    case "HiddenField": (control as HiddenField).Value = ""; break;
                    case "ListBox": (control as ListBox).SelectedIndex =-1; break;
                    case "HtmlInputText": (control as HtmlInputText).Value = ""; break;
                    case "HtmlInputCheckBox": (control as HtmlInputCheckBox).Checked = false; break;
                    case "HtmlInputHidden": (control as HtmlInputHidden).Value = ""; break;
                    case "HtmlTextArea": (control as HtmlTextArea).InnerText = ""; break;
                    case "HtmlSelect": (control as HtmlSelect).SelectedIndex = -1; break;
                }

                ClearControls(control.Controls);
            }
        }

        public static void SetAttribute(WebControl control, string AttName, string AttValue)
        {
            if (control.Attributes[AttName] == null)
                control.Attributes.Add(AttName, AttValue);
            else
                control.Attributes[AttName] = AttValue;
        }

        public static void AddCSSReference(string CssPath,Control RegisteringControl,string DynamicHeaderContentPlaceHolderID)
        {
            //if (RegisteringControl.Page.IsPostBack)
            //    return;

            HtmlLink CssLink = new HtmlLink();
            CssLink.Href = CssPath;
            CssLink.Attributes.Add("rel", "stylesheet");
            CssLink.Attributes.Add("type", "text/css");

            bool IsCssIncluded = false;
            ControlCollection HeaderControls = RegisteringControl.Page.Header.FindControl(DynamicHeaderContentPlaceHolderID).Controls;

            foreach (Control control in HeaderControls)
            {
                if (control is HtmlLink && (control as HtmlLink).Href == CssLink.Href)
                    IsCssIncluded = true;
            }

            if (!IsCssIncluded)
                HeaderControls.Add(CssLink);
        }

        public static void AddScriptReference(string ScriptPath , Control RegisteringControl,ScriptManager Manager)
        {
            if (RegisteringControl.Page.IsPostBack || Manager == null)
                return;

            Manager.Scripts.Add(new ScriptReference()
            {
                Path = ScriptPath,
                ScriptMode = ScriptMode.Release
            });
        }

        public static void AddStartupScript(string Script, Control RegisteringControl)
        {
            if (!RegisteringControl.Page.IsPostBack)
                return;

            ScriptManager.RegisterStartupScript(RegisteringControl, RegisteringControl.GetType(), RegisteringControl.ClientID, Script, true);
        }

        public static string ToHTMLMultiLinedText(string Text)
        {
            return Text.Replace("\n", "<br/>");
        }

        public static string ToSummerizedText(string strText, int MaxLength,bool WithTooltip)
        {
            if (strText == null)
                return strText;

            string strResult = strText;

            if (strText.Length > MaxLength && MaxLength != 0)
            {
                if (WithTooltip)
                {
                    strResult = string.Format("<span title=\"{1}\">{0}</span>",
                        strText.Substring(0, MaxLength) + " ...",
                        strText
                        );
                }
                else
                {
                    strResult = string.Format("<span>{0}</span>",
                        strText.Substring(0, MaxLength) + " ...");
                }
            }
            return strResult;
        }

        public static string ReplaceEnter(string Text,bool IsHTMLText)
        {
            if(IsHTMLText)

                if (Text.Contains(".\n"))
                    return Text.Replace(".\n", ".");
                else
                    return Text.Replace("\n.", ".");

            else
                return Text.Replace("\n", "<br />");
        }
    }
}