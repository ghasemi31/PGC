using System;
using kFrameWork.UI;
using pgc.Model;
using pgc.Model.Patterns;
using pgc.Business;
using kFrameWork.Util;

public partial class Pages_Admin_SystemEvent_List : BaseListControl
{
    
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.Grid = this.grdList;
        base.DataSource = this.obdSource;
    }

    protected override void Grid_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Action")
        //{
        //    BaseManagementPage<SystemEventBusiness, SystemEvent, SystemEventPattern, Entities> page = (this.Page as BaseManagementPage<SystemEventBusiness, SystemEvent, SystemEventPattern, Entities>);
        //    page.Mode = kFrameWork.Enums.ManagementPageMode.Edit;
        //    page.SelectedID = ConvertorUtil.ToInt64(Grid.DataKeys[ConvertorUtil.ToInt32(e.CommandArgument)].Value);
        //}
        base.Grid_RowCommand(sender, e);
    }
}