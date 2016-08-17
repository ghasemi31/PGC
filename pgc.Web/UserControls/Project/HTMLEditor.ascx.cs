using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Project_HTMLEditoe : BaseUserControl 
{


    private bool _required;

    public bool Required
    {
        get { return _required; }
        set { _required = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        RequiredFieldValidator1.Enabled = Required;

    }

    public void SetValue(string value)
    {
        CKEditor.Text = value;
    }
    public string GetValue()
    {
        return CKEditor.Text;
    }



}