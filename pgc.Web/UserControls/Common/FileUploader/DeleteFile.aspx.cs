using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.Util;
using System.IO;
using System.Text.RegularExpressions;

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

        //delete all dependencies file
        Regex reg = new Regex(@"\b" + name + @"_(?:\d*\.)?\d+_(?:\d*\.)?\d+_(keepratio|stretch|cropandscale|crop)?" + ext);
        var files = Directory.GetFiles(Server.MapPath(folder)).Where(path => reg.IsMatch(path));
        foreach (var file in files)
        {
            IOUtil.DeleteFile(file, false);
        }

        return true;
    }
}