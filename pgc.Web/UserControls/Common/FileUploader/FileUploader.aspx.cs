using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _FileUploader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //FilePath = Request.QueryString["FilePath"];
            hdf_contId.Value = Request.QueryString["contId"];
        }
    }
}