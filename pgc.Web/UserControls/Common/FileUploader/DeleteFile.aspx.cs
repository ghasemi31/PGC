using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.Util;
using System.IO;

public partial class UserControls_Common_FileUploader_DeleteFile : System.Web.UI.Page
{
    public bool IsDelete = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        IsDelete = Delete();
    }

    private bool Delete()
    {
        string FilePath = Request.QueryString["FilePath"];
        if (string.IsNullOrEmpty(FilePath))
            return false;

        //delete mani file
        bool res = IOUtil.DeleteFile(FilePath, true);

        //delete thumb file
        string fullname = Path.GetFileName(FilePath);
        string name = Path.GetFileNameWithoutExtension(FilePath);
        string ext = Path.GetExtension(FilePath);
        string folder = FilePath.TrimEnd(fullname.ToCharArray());
        IOUtil.DeleteFile(folder + name + "_DefaultThumb" + ext, true);

        return res;
    }
}