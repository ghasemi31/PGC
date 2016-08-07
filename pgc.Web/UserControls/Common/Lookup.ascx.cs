using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using kFrameWork.Util;
using kFrameWork.UI;

public partial class UserControl_Lookup : BaseUserControl
{

    #region Properties

    /// <summary>
    /// Default value is False
    /// </summary>
    private bool _Required = false;
    public bool Required
    {
        get { return _Required; }
        set { _Required = value; }
    }

    /// <summary>
    /// Default Value is String.Empty 
    /// </summary>
    private string _ValidationGroup = string.Empty;
    public string ValidationGroup
    {
        get { return _ValidationGroup; }
        set { _ValidationGroup = value; }
    }

    /// <summary>
    /// Default Value is "وارد کردین این مشخصه الزامی می باشد"
    /// </summary>
    private string _ValidationText = "وارد کردین این مشخصه الزامی می باشد";
    public string ValidationText
    {
        get { return _ValidationText; }
        set { _ValidationText = value; }
    }

    /// <summary>
    /// Default Value is String.Empty 
    /// </summary>
    private string _URL = string.Empty;
    public string URL
    {
        get { return _URL; }
        set { _URL = value; }
    }

    /// <summary>
    /// Default Value is 0
    /// </summary>
    private int _ColumnIndex = 0;
    public int ColumnIndex
    {
        get { return _ColumnIndex; }
        set { _ColumnIndex = value; }
    }


    /// <summary>
    /// Default Value is 870
    /// </summary>
    private int _LookupWidth = 870;
    public int LookupWidth
    {
        get { return _LookupWidth; }
        set { _LookupWidth = value; }
    }

    /// <summary>
    /// Default Value is 550
    /// </summary>
    private int _LookupHeight = 550;
    public int LookupHeight
    {
        get { return _LookupHeight; }
        set { _LookupHeight = value; }
    }

    
    public string Text
    {
        set 
        { 
            txtSearch.Text = value;
            hfResultText.Value = value;
        }
        get { return hfResultText.Value; }
    }

    public long Value
    {
        set 
        {
            hfResultValue.Value = value.ToString();           
        }
        get 
        {
            return ConvertorUtil.ToInt64(hfResultValue.Value);
        }
    }

    #endregion

    #region Events

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        
        if (Required)
        {
            RequiredFieldValidator validator = new RequiredFieldValidator();
            validator.ControlToValidate = "txtSearch";
            validator.ToolTip = (ValidationText != string.Empty) ? ValidationText : "وارد کردن مقدار این مشخصه ، الزامی است";
            validator.Text = "*";
            validator.CssClass = "validator";
            validator.ValidationGroup = ValidationGroup;
            plhvalidator.Controls.Add(validator);            
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtSearch.Text = hfResultText.Value;

        UIUtil.AddCSSReference("~/Styles/lookup.css", this, "DynamicHeader");

        UIUtil.AddScriptReference("~/Scripts/Lookup.js", this, ScriptManager.GetCurrent(this.Page));
        UIUtil.AddScriptReference("~/Scripts/jquery-ui-1.8.16.custom.min.js", this, ScriptManager.GetCurrent(this.Page));
    }

    #endregion
}