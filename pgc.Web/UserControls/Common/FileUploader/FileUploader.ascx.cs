using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using kFrameWork.UI;
using kFrameWork.Util;

public partial class FileUploader : BaseUserControl
{
    /// <summary>
    /// Relative Path of folder that fill will be saved there
    /// </summary>
    public string SaveFolder { get; set; }
    public string FilePath
    {
        get
        {
            return hdf_FilePath.Value;
        }
        set
        {
            hdf_FilePath.Value = value;
        }
    }
    public int FileSize
    {
        get
        {
            return IOUtil.GetFileSize(FilePath);
        }
    }
    public string ThumbPath
    {
        get
        {
            string fullname = Path.GetFileName(FilePath);
            string name = Path.GetFileNameWithoutExtension(FilePath);
            string ext = Path.GetExtension(FilePath);
            string folder = FilePath.TrimEnd(fullname.ToCharArray());
            return folder + name + "_DefaultThumb" + ext;
        }
    }
    public bool ReadOnly { get; set; }
    public bool Required { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}