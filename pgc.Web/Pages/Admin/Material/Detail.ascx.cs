using kFrameWork.Enums;
using kFrameWork.UI;
using pgc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Material_Detail : BaseDetailControl<Material>
{
    public override Material GetEntity(Material Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Material();
        Data.Title = txtTitle.Text;
        Data.MaterialPicPath = fupProductPic.FilePath;
        return Data;
    }

    public override void SetEntity(Material Data, ManagementPageMode Mode)
    {
        txtTitle.Text = Data.Title;
        fupProductPic.FilePath = Data.MaterialPicPath;
    }
}