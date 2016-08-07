using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.Util;
using kFrameWork.UI;

public partial class UserControl_PersianDatePicker : BaseUserControl
{
    public bool Required { get; set; }
    public string PersianDate
    {               
        get
        {
            return txtPersianDate.Text;
        }
        set
        {
            txtPersianDate.Text = value;            
        }
    }
    public string ValidationText { get; set; }
    public string ValidationGroup { get; set; }
    public bool HasDate
    {
        get
        {
            return (!string.IsNullOrEmpty(txtPersianDate.Text.Trim()));
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //UIUtil.AddCSSReference("~/Styles/farsicalender.css", this, "DynamicHeader");
        //UIUtil.AddScriptReference("~/Scripts/farsicalendar.js", this, ScriptManager.GetCurrent(this.Page));
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (pnlDP.Attributes["onclick"] == null)
            pnlDP.Attributes.Add("onclick", "displayDatePicker('" + txtPersianDate.ClientID + "')");
        else
            pnlDP.Attributes["onclick"] = "displayDatePicker('" + txtPersianDate.ClientID + "')";

        if (txtPersianDate.Attributes["onkeypress"] == null)
            txtPersianDate.Attributes.Add("onkeypress", "lockkeypress(event)");
        else
            txtPersianDate.Attributes["onkeypress"] = "lockkeypress(event)";


        if (Required)
        {
            RequiredFieldValidator Validator = new RequiredFieldValidator();

            Validator.ControlToValidate = txtPersianDate.ID;
            Validator.ErrorMessage = "*";
            Validator.ToolTip = ValidationText;
            if (!string.IsNullOrEmpty(ValidationGroup))
                Validator.ValidationGroup = ValidationGroup;

            Validator.CssClass = "dpValidator";
            plhvalidator.Controls.Add(Validator);
        }
    }
}