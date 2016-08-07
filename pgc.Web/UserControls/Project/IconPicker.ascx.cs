using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Project_IconPicker: BaseUserControl 
{

    public IconPickerMode Mode { get; set; }
    public IconPickerIconset Iconset { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetValue(string value)
    {
        hfValue.Value = value;
    }
    public string GetValue()
    {
        return hfValue.Value;
    }



}