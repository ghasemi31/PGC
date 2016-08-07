using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using System.ComponentModel;
using kFrameWork.WebControls;
using kFrameWork.Util;
using pgc.Model;

public partial class UserControls_Common_ckeditor_HtmlEditor : BaseUserControl
{
    #region Properies

    /// <summary>
    /// Provides Control Width --- Default Value is 0 and will set by Css
    /// </summary>
    private int _TextBoxWidth = 0;
    [Category("Appearance")]
    public int TextBoxWidth
    {
        get { return _TextBoxWidth; }
        set { _TextBoxWidth = value; }
    }

    /// <summary>
    /// Provides Control Height --- Default Value is 0 and will set by Css
    /// </summary>
    private int _TextBoxHeight = 0;
    [Category("Appearance")]
    public int TextBoxHeight
    {
        get { return _TextBoxHeight; }
        set { _TextBoxHeight = value; }
    }

    /// <summary>
    /// Provides Control MaxLength --- Default Value is 0 
    /// </summary>
    private int _MaxLength = 0;
    [Category("Appearance")]
    public int MaxLength
    {
        get { return _MaxLength; }
        set { _MaxLength = value; }
    }

    /// <summary>
    /// Provides Control Visibility by style 
    /// Default value is string.Empty (Means block) in orther to change visibility, set this property to none or block 
    /// </summary>
    private string _TextBoxDisplay = "";
    [Category("Appearance")]
    public string TextBoxDisplay
    {
        get { return _TextBoxDisplay; }
        set { _TextBoxDisplay = value; }
    }

    /// <summary>
    /// Provides Direction --- Default Value is ltr
    /// </summary>
    private string _Direction = "ltr";
    [Category("Appearance")]
    public string Direction
    {
        get { return _Direction; }
        set { _Direction = value; }
    }

    /// <summary>
    /// Provides Css-Class for control --- Default Value is "htmleditor" 
    /// </summary>    
    private string _CssClass = "htmleditor";
    [Category("Appearance")]
    public string CssClass
    {
        set { _CssClass = value; }
        get { return _CssClass; }
    }

    /// <summary>
    /// Provides Required Field Validator --- Default Value is false
    /// </summary>
    private bool _Required = false;
    [Category("Validation")]
    public bool Required
    {
        get { return _Required; }
        set { _Required = value; }
    }

    /// <summary>
    /// Provides Validation Group whether Required property is true --- Default Value is string.Empty
    /// </summary>
    private string _ValidationGroup = string.Empty;
    [Category("Validation")]
    public string ValidationGroup
    {
        get { return _ValidationGroup; }
        set { _ValidationGroup = value; }
    }

    /// <summary>
    /// Provides Validation Text whether Required property is true --- Default Value is "مقدار این مشخصه الزامی می باشد"
    /// </summary>
    private string _ValidationText = "مقدار این مشخصه الزامی می باشد";
    [Category("Validation")]
    public string ValidationText
    {
        get { return _ValidationText; }
        set { _ValidationText = value; }
    }

    /// <summary>
    /// Provides whether control's value has set or NOT.
    /// </summary>
    [Category("Status")]
    public bool HasValue
    {
        get { return (txt.Text.Trim() != string.Empty); }
    }

    /// <summary>
    /// Provides control's value.
    /// </summary>
    [Category("Status")]
    public string Text
    {
        set { txt.Text = value; }
        get { return txt.Text; }
    }

    /// <summary>
    /// Returns TextBox Control Instanse
    /// </summary>
    [Category("Status")]
    public TextBox TextBoxControl
    {
        get { return txt; }
    }

    /// <summary>
    /// Provides control's mode (Single Line, Multi Line, Password).
    /// Default Value is Multiline
    /// </summary>
    [Category("Behaviour")]
    public TextBoxMode TextMode
    {
        get { return txt.TextMode; }
        set { txt.TextMode = value; }
    }

    /// <summary>
    /// Authomatically Change direction of textbox while toggleing language by client --- Default Value is True
    /// This property takes affects only during TextMode
    /// </summary>    
    private bool _ToggleLang = true;
    [Category("Behaviour")]
    public bool ToggleLang
    {
        set { _ToggleLang = value; }
        get { return _ToggleLang; }
    }

    /// <summary>
    /// Columns Count using for TextArea
    /// </summary>    
    [Category("Behaviour")]
    public int Columns
    {
        set { txt.Columns = value; }
        get { return txt.Columns; }
    }

    /// <summary>
    /// Rows Count using for TextArea
    /// </summary>    
    [Category("Behaviour")]
    public int Rows
    {
        set { txt.Rows = value; }
        get { return txt.Rows; }
    }

    #endregion

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        txt.TextMode = TextBoxMode.MultiLine;
        (this.Page as BasePage).AddCssReservation("~/UserControls/Common/ckeditor/custom/htmleditor.css");

        (this.Page as BasePage).AddJsReservation("~/Scripts/Shared/jquery-ui-1.8.16.custom.min.js");
        (this.Page as BasePage).AddJsReservation("~/UserControls/Common/ckeditor/custom/htmleditor.js");
        
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        txt.CssClass = this.CssClass;
        if (TextBoxWidth > 0)
            txt.Width = Unit.Pixel(TextBoxWidth);
        if (TextBoxHeight > 0)
            txt.Height = Unit.Pixel(TextBoxHeight);
        if (MaxLength > 0)
            txt.MaxLength = MaxLength;
        if (TextBoxDisplay != "")
            txt.Style["display"] = TextBoxDisplay;

        //if (Direction != string.Empty)
        //    UIUtil.SetAttribute(txt, "dir", Direction);
        txt.Direction = Direction;

        txt.ToggleLang = this.ToggleLang;

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