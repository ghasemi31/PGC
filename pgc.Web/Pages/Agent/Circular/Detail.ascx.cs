using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model.Patterns;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using kFrameWork.Util;
using System.Linq;

public partial class Pages_Agent_Circular_Detail : BaseDetailControl<Circular>
{
    public Circular circular;
    public override Circular GetEntity(Circular Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Circular();

        return Data;
    }
    public override void SetEntity(Circular Data, ManagementPageMode Mode)
    {
        circular = Data;
    }
}