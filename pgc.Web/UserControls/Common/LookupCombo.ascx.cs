using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI.WebControls;
using kFrameWork.Util;
using kFrameWork.UI;

public partial class UserControl_Common_LookupCombo : BaseUserControl
{
    #region Properties

    /// <summary>
    /// Default Value is string.Empty
    /// </summary>
    [Category("Appearance")]
    public string CssClass
    {
        get { return cboLookup.CssClass; }
        set { cboLookup.CssClass = value; }
    }

    /// <summary>
    /// Default value is default width of DropDwonList or its css
    /// </summary>
    [Category("Appearance")]
    public Unit DisplayWidth
    {
        get { return cboLookup.Width; }
        set { cboLookup.Width = value; }
    }

    /// <summary>
    /// Default Value is False
    /// </summary>
    private bool _Required = false;

    [Category("Validation")]
    public bool Required
    {
        get { return _Required; }
        set { _Required = value; }
    }

    /// <summary>
    /// Default Value is "مقدار این مشخصه الزامی می باشد"
    /// </summary>
    private string _ValidationText = "مقدار این مشخصه الزامی می باشد";

    [Category("Validation")]
    public string ValidationText
    {
        get { return _ValidationText; }
        set { _ValidationText = value; }
    }

    /// <summary>
    /// Default Value is string.Empty
    /// </summary>
    private string _ValidationGroup = string.Empty;

    [Category("Validation")]
    public string ValidationGroup
    {
        get { return _ValidationGroup; }
        set { _ValidationGroup = value; }
    }

    /// <summary>
    /// Default Value is False
    /// </summary>
    private bool _AutoPostBack = false;

    [Category("Behaviour")]
    public bool AutoPostBack
    {
        get { return _AutoPostBack; }
        set { _AutoPostBack = value; }
    }

    /// <summary>
    /// Default Value is string.Empty
    /// </summary>
    private string _BusinessTypeName = string.Empty;

    [Category("Binding")]
    public string BusinessTypeName
    {
        get { return _BusinessTypeName; }
        set { _BusinessTypeName = value; }
    }

    /// <summary>
    /// Default Value is string.Empty
    /// </summary>
    private string _SelectMethod = "";

    [Category("Binding")]
    public string SelectMethod
    {
        get { return _SelectMethod; }
        set { _SelectMethod = value; }
    }

    /// <summary>
    /// Default Value is "ID"
    /// </summary>
    private string _ValueColumnName = "ID";

    [Category("Binding")]
    public string ValueColumnName
    {
        get { return _ValueColumnName; }
        set { _ValueColumnName = value; }
    }

    /// <summary>
    /// Default Value is "Title"
    /// </summary>
    private string _TextColumnName = "Title";

    [Category("Binding")]
    public string TextColumnName
    {
        get { return _TextColumnName; }
        set { _TextColumnName = value; }
    }

    /// <summary>
    /// Default Value is string.Empty
    /// </summary>
    private string _EnumParameterType = string.Empty;

    [Category("Binding")]
    public string EnumParameterType
    {
        get { return _EnumParameterType; }
        set { _EnumParameterType = value; }
    }

    /// <summary>
    /// Default Value is string.Empty
    /// </summary>
    private string _EnumParameterTypeAssembly = "";

    [Category("Binding")]
    public string EnumParameterTypeAssembly
    {
        get { return _EnumParameterTypeAssembly; }
        set { _EnumParameterTypeAssembly = value; }
    }

    /// <summary>
    /// Default Value is false
    /// </summary>
    private bool _AddUserIDParameter = false;

    [Category("Binding")]
    public bool AddUserIDParameter
    {
        get { return _AddUserIDParameter; }
        set { _AddUserIDParameter = value; }
    }

    /// <summary>
    /// Default Value is string.Empty
    /// </summary>
    private string _DependantControl = string.Empty;

    [Category("Binding")]
    public string DependantControl
    {
        get { return _DependantControl; }
        set { _DependantControl = value; }
    }

    /// <summary>
    /// Default Value is string.Empty
    /// </summary>
    private string _DependOnParameterName = string.Empty;

    [Category("Binding")]
    public string DependOnParameterName
    {
        get { return _DependOnParameterName; }
        set { _DependOnParameterName = value; }
    }

    /// <summary>
    /// Default Value is "Int32"
    /// </summary>
    private string _DependOnParameterType = "Int";

    [Category("Binding")]
    public string DependOnParameterType
    {
        get { return _DependOnParameterType; }
        set { _DependOnParameterType = value; }
    }

    /// <summary>
    /// Default Value is Null
    /// </summary>
    private object _DependOnParameterValue = null;

    [Category("Binding")]
    [Browsable(false)]
    public object DependOnParameterValue
    {
        get { return _DependOnParameterValue; }
        set { _DependOnParameterValue = value; }
    }

    /// <summary>
    /// Default Value is false
    /// </summary>
    private bool _AddDefaultItem = false;

    [Category("DefaultItem")]
    public bool AddDefaultItem
    {
        get { return _AddDefaultItem; }
        set { _AddDefaultItem = value; }
    }

    /// <summary>
    /// Default Value is "فرقی نمی کند"
    /// </summary>
    private string _DefaultItemText = "فرقی نمی کند";

    [Category("DefaultItem")]
    public string DefaultItemText
    {
        get { return _DefaultItemText; }
        set { _DefaultItemText = value; }
    }

    /// <summary>
    /// Default Value is -1
    /// </summary>
    private int _DefaultItemValue = -1;

    [Category("DefaultItem")]
    public int DefaultItemValue
    {
        get { return _DefaultItemValue; }
        set { _DefaultItemValue = value; }
    }

    /// <summary>
    /// Default Value is DefaultItemPosition.First
    /// </summary>
    private DefaultItemPosition _DefaultItemPlacement = DefaultItemPosition.First;

    [Category("DefaultItem")]
    public DefaultItemPosition DefaultItemPlacement
    {
        get { return _DefaultItemPlacement; }
        set { _DefaultItemPlacement = value; }
    }

    public enum DefaultItemPosition
    {
        First,
        Last
    }

    /// <summary>
    /// Gets the DropDown control of this user control
    /// </summary>
    [Browsable(false)]
    public DropDownList DropDownListControl
    {
        get
        {
            return this.cboLookup;
        }
    }

    #endregion Properties

    public delegate void SelectingDelegate(object sender, List<Type> ParamTypes , List<object> ParamValues);
    public event SelectingDelegate Selecting;

    public delegate void SelectedIndexChangedDelegate(object sender, EventArgs e);
    public event SelectedIndexChangedDelegate SelectedIndexChanged;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (AutoPostBack)
        {
            cboLookup.SelectedIndexChanged += new EventHandler(On_SelectedIndexChanged);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
            DataBind();
    }

    public new void DataBind()
    {
        List<object> ParamValues = new List<object>();
        List<Type> ParamTypes = new List<Type>();

        if (EnumParameterType != string.Empty)
        {
            //Enum Selecting
            if (EnumParameterTypeAssembly == "")
                EnumParameterTypeAssembly = EnumParameterType.Split('.')[0] + "." + EnumParameterType.Split('.')[1];
            ParamValues.Add(Type.GetType(EnumParameterType + "," + EnumParameterTypeAssembly));
            ParamTypes.Add(typeof(Type));
        }
        else
        {
            //Other Selecting
            if (AddUserIDParameter)
            {
                ParamValues.Add(UserSession.UserID);
                ParamTypes.Add(typeof(long));
            }

            if (DependOnParameterName != string.Empty)
            {
                ParamValues.Add(ConvertorUtil.Convert(DependOnParameterValue, DependOnParameterType, true));
                ParamTypes.Add(Type.GetType("System." + DependOnParameterType));
            }
        }

        if (Selecting != null)
            Selecting(this, ParamTypes,ParamValues);

        //Binding
        string Assembly = "";
        if (EnumParameterType != string.Empty && BusinessTypeName == string.Empty && SelectMethod == string.Empty)
        {
            //Shortcut Binding > Enum
            BusinessTypeName = "kFrameWork.Util.EnumUtil";
            Assembly = "kFrameWork";
            SelectMethod = "GetEnumLookup";
        }
        else
        {
            //Default Binding
            Assembly = BusinessTypeName.Split('.')[0] + "." + BusinessTypeName.Split('.')[1];
            if (SelectMethod == "")
                SelectMethod = "GetLookupList";
        }

        cboLookup.DataTextField = TextColumnName;
        cboLookup.DataValueField = ValueColumnName;

        Type BusinessType = Type.GetType(BusinessTypeName + "," + Assembly );
        MethodInfo method = BusinessType.GetMethod(SelectMethod, ParamTypes.ToArray());
        var Res = method.Invoke(Activator.CreateInstance(BusinessType), ParamValues.ToArray());//MethodInfo generic = method.MakeGenericMethod(myType);

        cboLookup.ClearSelection();

        cboLookup.DataSource = Res;
        cboLookup.DataBind();
    }

    public void Reset()
    {
        cboLookup.Items.Clear();
        if (AddDefaultItem && !cboLookup.Items.Contains(new ListItem(DefaultItemText, DefaultItemValue.ToString())))
        {
            cboLookup.Items.Insert(
                (DefaultItemPlacement == DefaultItemPosition.First) ? 0 : cboLookup.Items.Count,
                new ListItem(DefaultItemText, DefaultItemValue.ToString())
                );
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (Required)
        {
            RequiredFieldValidator validator = new RequiredFieldValidator();
            validator.ControlToValidate = "cboLookup";
            validator.ToolTip = ValidationText;
            validator.Text = "*";
            validator.CssClass = "validator";
            validator.ValidationGroup = ValidationGroup;
            this.Controls.Add(validator);
        }

        cboLookup.AutoPostBack = AutoPostBack;
    }

    protected void On_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SelectedIndexChanged != null)
            SelectedIndexChanged(sender, e);

        if (DependantControl != string.Empty)
        {
            //ContentPlaceHolder MainContent = Page.Master.FindControl("Content") as ContentPlaceHolder;
            UserControl_Common_LookupCombo DependentUserControl = this.Parent.FindControl(DependantControl) as UserControl_Common_LookupCombo;
            DependentUserControl.DependOnParameterValue = cboLookup.SelectedValue;
            DependentUserControl.DataBind();
        }
    }

    protected void cboLookup_DataBound(object sender, EventArgs e)
    {
        if (!this.IsPostBack && AutoPostBack)
            On_SelectedIndexChanged(cboLookup, null);

        if (AddDefaultItem && !cboLookup.Items.Contains(new ListItem(DefaultItemText, DefaultItemValue.ToString())))
        {
            cboLookup.Items.Insert(
                (DefaultItemPlacement == DefaultItemPosition.First) ? 0 : cboLookup.Items.Count,
                new ListItem(DefaultItemText, DefaultItemValue.ToString())
                );

            //cboLookup.SelectedValue = DefaultItemValue.ToString();
        }
    }

    public T GetSelectedValue<T>()
    {
        if (typeof(T) == typeof(Int16))
            return (T)Convert.ChangeType(ConvertorUtil.ToInt16(cboLookup.SelectedValue), typeof(T));

        if (typeof(T) == typeof(Int32))
            return (T)Convert.ChangeType(ConvertorUtil.ToInt32(cboLookup.SelectedValue), typeof(T));

        if (typeof(T) == typeof(Int64))
            return (T)Convert.ChangeType(ConvertorUtil.ToInt64(cboLookup.SelectedValue), typeof(T));

        if (typeof(T) == typeof(decimal))
            return (T)Convert.ChangeType(ConvertorUtil.ToDecimal(cboLookup.SelectedValue), typeof(T));

        if (typeof (T) == (typeof(bool)))
            return (T)Convert.ChangeType(ConvertorUtil.ToBoolean(cboLookup.SelectedValue), typeof(T));
        if(typeof(T)==(typeof(byte)))
            return (T)Convert.ChangeType(ConvertorUtil.ToByte(cboLookup.SelectedValue), typeof(T));
        return (T)(object)ConvertorUtil.ToInt32(cboLookup.SelectedValue);
    }

    public void SetSelectedValue(object val)
    {
        try
        {
            if (val == null)
            {
                cboLookup.SelectedIndex = -1;
                On_SelectedIndexChanged(cboLookup, null);
                return;
            }

            string NewVal = ConvertorUtil.ToInt32(val).ToString();
            if (cboLookup.SelectedValue != NewVal)
            {
                cboLookup.SelectedValue = NewVal;
                On_SelectedIndexChanged(cboLookup, null);
            }
        }
        catch 
        {
            // do nothing
        }
    }
}