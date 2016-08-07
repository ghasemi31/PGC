using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model;
using kFrameWork.Util;
using System;

public partial class Pages_Admin_BranchReturnOrderTrust_Search : BaseSearchControl<BranchReturnOrderPattern>
{
    public override BranchReturnOrderPattern Pattern
    {
        get
        {
            return new BranchReturnOrderPattern()
            {
                //Title = txtTitle.Text,
                Branch_ID=lkpBranch.GetSelectedValue<long>(),
                OrderTitle_ID=lkpOrderTitle.GetSelectedValue<long>(),
                ID=string.IsNullOrEmpty(txtID.Text)?0:long.Parse(txtID.Text),
                Price=nmrPrice.Pattern,
                Status=lkpOrderStatus.GetSelectedValue<BranchReturnOrderStatus>(),
                OrderedPersianDate=pdrRegDate.DateRange
            };
        }
        set
        {
            //txtTitle.Text = value.Title;
            lkpBranch.SetSelectedValue(value.Branch_ID);
            lkpOrderTitle.SetSelectedValue(value.OrderTitle_ID);
            txtID.Text = (value.ID>0)?value.ID.ToString():"";
            nmrPrice.Pattern=value.Price;
            lkpOrderStatus.SetSelectedValue(value.Status);
            pdrRegDate.DateRange = value.OrderedPersianDate;
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        Session["BranchReturnPrintPattern"] = Pattern;
        var _Page = this.Page as BaseManagementPage<BranchReturnOrderBusiness, BranchReturnOrder, BranchReturnOrderPattern, pgcEntities>;
        _Page.ListControl.Grid.DataBind();
    }
}