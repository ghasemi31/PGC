using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.Util;
using pgc.Model;
using kFrameWork.UI;

public partial class UserControls_Project_AdvCollection : System.Web.UI.UserControl
{
    long PageID;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageID = (this.Page as BasePage).Entity.ID;
    }

    protected void lsvAdv_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            long id = ConvertorUtil.ToInt64(DataBinder.Eval(e.Item.DataItem, "id"));

            //  (e.Item.FindControl("viewer") as UserControl_AdvViewer).adv = advBusiness.GetAdv(id);
            (e.Item.FindControl("viewer") as UserControls_Project_AdvViewer).adv = e.Item.DataItem as Advertisement;
        }
    }
    protected void odsAdv_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["PageID"] = PageID;
    }
}