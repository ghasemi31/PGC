using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.Util;
using System.Drawing;
using kFrameWork.UI;

public partial class UserControl_Common_NumericTextBox :BaseUserControl
{

    #region Properies

    private bool _SupportLetter = true;
    public bool SupportLetter 
    {
        get{return _SupportLetter;}
        set{_SupportLetter = value;}
    }

    private bool _SupportComma = true;
    public bool SupportComma
    {
        get { return _SupportComma; }
        set { _SupportComma = value; }
    }

    private string _UnitText = "";
    public string UnitText
    {
        get { return _UnitText; }
        set { _UnitText = value; }
    }

    private string _Direction = "ltr";
    public string Direction
    {
        get { return _Direction; }
        set { _Direction = value; }
    }

    private bool _Required = false;
    public bool Required
    {
        get { return _Required; }
        set { _Required = value; }
    }

    private string _ValidationGroup = "";
    public string ValidationGroup
    {
        get { return _ValidationGroup; }
        set { _ValidationGroup = value; }
    }

    private string _ValidationText = "";
    public string ValidationText
    {
        get { return _ValidationText; }
        set { _ValidationText = value; }
    }

    private int _TextBoxWidth = 0;
    public int TextBoxWidth
    {
        get { return _TextBoxWidth; }
        set { _TextBoxWidth = value; }
    }

    private int _TextBoxMaxLen = 0;
    public int TextBoxMaxLen
    {
        get { return _TextBoxMaxLen; }
        set { _TextBoxMaxLen = value; }
    }

    private string _TextBoxDisplay = "";
    public string TextBoxDisplay
    {
        get { return _TextBoxDisplay; }
        set { _TextBoxDisplay = value; }
    }

    public bool HasValue
    {
        get { return (txt.Text.Trim() != string.Empty); }
    }

    public TextBox TextBoxControl
    {
        get
        {
            return this.txt;
        }
    }

    #endregion

    public T GetNumber<T>() 
    {
        if (typeof(T).Name == typeof(Int16).Name)
            return (T)Convert.ChangeType(ConvertorUtil.ToInt16(txt.Text.Replace(",","")),typeof(T));

        if (typeof(T).Name == typeof(Int32).Name)
            return (T)Convert.ChangeType(ConvertorUtil.ToInt32(txt.Text.Replace(",", "")), typeof(T));

        if (typeof(T).Name == typeof(Int64).Name)
            return (T)Convert.ChangeType(ConvertorUtil.ToInt64(txt.Text.Replace(",", "")), typeof(T));

        if (typeof(T).Name == typeof(decimal).Name)
            return (T)Convert.ChangeType(ConvertorUtil.ToDecimal(txt.Text.Replace(",", "")), typeof(T));

        return (T)Convert.ChangeType(txt.Text.Replace(",", ""), typeof(T));
    }

    public void SetNumber(Object Value)
    {
        txt.Text = UIUtil.GetCommaSeparatedOf(Value.ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (SupportComma)
            UIUtil.SetAttribute(txt, "onkeyup", "FormatCurrency(this)");

        UIUtil.SetAttribute(txt, "dir", Direction);

        if (SupportLetter)
        {
            UIUtil.SetAttribute(txt, "onblur", "hideLetter(" + txt.ClientID + ")");
            this.Controls.Add(new LiteralControl("<span id=" + txt.ClientID + "_Letter" + " class=\"letter\">" + ((txt.Text != "") ? UIUtil.GetLetterOf(txt.Text.Replace(",", "")) : "") + "</span>" + ((UnitText != "") ? "&nbsp;<span class='unittext'>" + UnitText + "</span>" : "")));
            UIUtil.SetAttribute(txt, "onfocus", "showLetter( " + txt.ClientID + " , true )");
            UIUtil.SetAttribute(txt, "onclick", "showLetter( " + txt.ClientID + " , true )");
            UIUtil.SetAttribute(txt, "ondblclick", "showLetter( " + txt.ClientID + " , true )");
        }

        if (TextBoxWidth > 0)
            txt.Width = Unit.Pixel(TextBoxWidth);

        if (TextBoxMaxLen > 0)
            txt.MaxLength = TextBoxMaxLen;

        if (TextBoxDisplay != "")
            txt.Style["display"] = TextBoxDisplay;

        if (Required)
        {
            RequiredFieldValidator validator = new RequiredFieldValidator();
            validator.ControlToValidate = "txt";
            validator.ToolTip = (ValidationText != string.Empty) ? ValidationText : "وارد کردن مقدار این مشخصه ، الزامی است";
            validator.Text = "*";
            validator.CssClass = "validator";
            validator.ValidationGroup = ValidationGroup;
            this.Controls.Add(validator);
        }
    }

}