using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Model;
using pgc.Business.General;
using kFrameWork.Model;
using pgc.Business;
using kFrameWork.Util;
using pgc.Model.Enums;

public partial class Pages_Agent_AllowOnlineOrder_Default : BasePage
{
    pgc.Business.General.BranchBusiness Business = new pgc.Business.General.BranchBusiness();
    Branch Branch = new Branch();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            UpdateElement();
    }

    private void UpdateElement()
    {
         Branch= Business.RetirveBranchID(UserSession.User.Branch_ID.Value);
         chbAllow.Checked = Branch.AllowOnlineOrder;
         timeFrom.SelectedTime = Branch.AllowOnlineOrderTimeFrom;
         timeTo.SelectedTime = Branch.AllowOnlineOrderTimeTo;         
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Branch = Business.RetirveBranchID(UserSession.User.Branch_ID.Value);

        Branch.AllowOnlineOrder = chbAllow.Checked;

        if (Branch.AllowOnlineOrder)
        {
            Branch.AllowOnlineOrderTimeFrom = timeFrom.SelectedTime;
            Branch.AllowOnlineOrderTimeTo = timeTo.SelectedTime;
        }


        OperationResult res = new OperationResult();
        res = Business.UpdateAllowOnlineOrderChanges(Branch);

        UserSession.AddMessage(res.Messages);
        UpdateElement();
        //if (res.Result == ActionResult.Done)
        //    Response.Redirect(GetRouteUrl("agent-default", null));
     
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("agent-default", null);
    }
}