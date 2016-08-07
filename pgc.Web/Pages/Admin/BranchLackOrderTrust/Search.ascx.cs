using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model;
using kFrameWork.Enums;
using kFrameWork.Util;
using System;

public partial class Pages_Admin_BranchLackOrderTrust_Search : BaseSearchControl<BranchLackOrderPattern>
{
    public override BranchLackOrderPattern Pattern
    {
        get
        {
            return new BranchLackOrderPattern()
            {
                Branch_ID = lkpBranch.GetSelectedValue<long>(),
                BranchOrder_ID = string.IsNullOrEmpty(txtOrderID.Text) ? 0 : long.Parse(txtOrderID.Text),
                ID = string.IsNullOrEmpty(txtLackID.Text) ? 0 : long.Parse(txtLackID.Text),
                OrderTitle_ID = lkpOrderTitle.GetSelectedValue<long>(),
                Price = nmrPrice.Pattern,
                Status = lkpOrderStatus.GetSelectedValue<BranchLackOrderStatus>(),
                OrderedPersianDate = pdrRegDate.DateRange
            };
        }
        set
        {
            lkpBranch.SetSelectedValue(value.Branch_ID);
            lkpOrderTitle.SetSelectedValue(value.OrderTitle_ID);
            nmrPrice.Pattern = value.Price;
            lkpOrderStatus.SetSelectedValue(value.Status);
            txtLackID.Text = (value.ID > 0) ? value.ID.ToString() : "";
            txtOrderID.Text = (value.BranchOrder_ID > 0) ? value.BranchOrder_ID.ToString() : "";
            pdrRegDate.DateRange = value.OrderedPersianDate;
        }
    }

    public override BranchLackOrderPattern DefaultPattern
    {
        get
        {
            var _Page = this.Page as BaseManagementPage<BranchLackOrderBusiness, BranchLackOrder, BranchLackOrderPattern, pgcEntities>;

            if (_Page.HasValidQueryString<long>(QueryStringKeys.fid))
            {
                BranchLackOrder lackOrder = _Page.Business.RetrieveLackByOrder(_Page.GetQueryStringValue<long>(QueryStringKeys.fid));
                if (lackOrder != null && lackOrder.ID > 0)
                {
                    _Page.Mode = kFrameWork.Enums.ManagementPageMode.Edit;
                    _Page.SelectedID = lackOrder.ID;
                    _Page.DetailControl.BeginMode(ManagementPageMode.Edit);
                    _Page.DetailControl.SetEntity(_Page.Business.Retrieve(_Page.SelectedID), ManagementPageMode.Edit);
                }
            }

            if (_Page.HasValidQueryString<long>(QueryStringKeys.id))
            {
                BranchLackOrder lackOrder = _Page.Business.RetrieveLackByOrder(_Page.GetQueryStringValue<long>(QueryStringKeys.id));
                if (lackOrder != null && lackOrder.ID > 0)
                {
                    _Page.Mode = kFrameWork.Enums.ManagementPageMode.Edit;
                    _Page.SelectedID = lackOrder.ID;
                    _Page.DetailControl.BeginMode(ManagementPageMode.Edit);
                    _Page.DetailControl.SetEntity(_Page.Business.Retrieve(_Page.SelectedID), ManagementPageMode.Edit);
                }
            }
            return new BranchLackOrderPattern();
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        Session["BranchLackPrintPattern"] = Pattern;
        var _Page = this.Page as BaseManagementPage<BranchLackOrderBusiness, BranchLackOrder, BranchLackOrderPattern, pgcEntities>;
        _Page.ListControl.Grid.DataBind();
    }
}