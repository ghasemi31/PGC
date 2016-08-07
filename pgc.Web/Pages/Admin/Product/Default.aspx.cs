using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using kFrameWork.UI;
using pgc.Business;
using pgc.Model;
using pgc.Model.Patterns;
using kFrameWork.Util;
using pgc.Model.Enums;

public partial class Pages_Admin_Product_Default : BaseManagementPage<ProductBusiness, Product, ProductPattern, pgcEntities>
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        base.DetailControl = pnlDetail;
        base.ListControl = pnlList;
        base.SearchControl = pnlSearch;

        base.Business = new ProductBusiness();
    }

    public override void Command(object Sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Pics")
        {
            long Product_ID = ConvertorUtil.ToInt64(this.ListControl.Grid.DataKeys[ConvertorUtil.ToInt32(e.CommandArgument)].Value);
            Response.Redirect(GetRouteUrl("admin-productmaterial", null) + "?" + QueryStringKeys.id.ToString() + "=" + Product_ID);
        }

        base.Command(Sender, e);
    }
}