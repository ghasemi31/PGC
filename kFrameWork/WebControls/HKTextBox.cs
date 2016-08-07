using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using kFrameWork.Util;
using kFrameWork.UI;

namespace kFrameWork.WebControls
{
    public class HKTextBox:TextBox
    {
        //required restrictNum() js function (currently in global.js)

        public string TiedButton { get; set; }
        public bool? ToggleLang { get; set; }

        private InputMode _Mode = InputMode.Text;
        public InputMode Mode
        {
            get{return _Mode;}
            set{_Mode = value;}
        }

        private string _Direction="";
        public string Direction
        {
            get { return _Direction; }
            set { _Direction = value; }
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (Mode == InputMode.Text)
            {
                if (this.TiedButton != null)
                {
                    string strJS = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {document.getElementById('" + (this.NamingContainer.FindControl(this.TiedButton) as Button).ClientID + "').click();return false;} else return true; ";
                    UIUtil.SetAttribute(this, "onkeydown", strJS);
                }

                if (ToggleLang == null || ToggleLang == true)
                {
                    UIUtil.SetAttribute(this, "onkeyup", string.Format("toggleLang({0})",this.ClientID));
                    UIUtil.SetAttribute(this, "onkeypress", string.Format("toggleLang({0})",this.ClientID));
                }
            }

            if (Mode == InputMode.Numeric)
                UIUtil.SetAttribute(this, "onkeypress", "restrictNum(event,false,false,false,false)");

            if (Mode == InputMode.Numeric_Decimal)
                UIUtil.SetAttribute(this, "onkeypress", "restrictNum(event,true,false,false,false)");
                

            if (Mode == InputMode.Phone)
                UIUtil.SetAttribute(this, "onkeypress", "restrictNum(event,false,true,true,true)");


            if (Direction == "")
            {
                // Default Behavoiur
                if (Mode == InputMode.Numeric || Mode == InputMode.Phone || Mode == InputMode.Email || Mode == InputMode.Numeric_Decimal)
                    UIUtil.SetAttribute(this, "dir", "ltr");
                else
                    UIUtil.SetAttribute(this, "dir", "rtl");
            }
            else
            {
                // Overrided Behavoiur
                UIUtil.SetAttribute(this, "dir", Direction);
            }

            (this.Page as BasePage).AddJsReservation(UIUtil.GetAttachmentUrl(this, UIUtil.AttachmentType.js));
        }

        public enum InputMode
        {
            Text = 0,
            Numeric = 1,
            Phone = 2,
            Email = 3,
            Numeric_Decimal = 4
        }

    }
}
