using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;
using kFrameWork.Enums;
using System;
using kFrameWork.Util;

public partial class Pages_Agent_BranchLackOrder_Search : BaseSearchControl<BranchLackOrderPattern>
{
    public override BranchLackOrderPattern Pattern
    {
        get
        {
            return new BranchLackOrderPattern()
            {
                //Title = txtTitle.Text,
                Branch_ID = UserSession.User.Branch_ID.Value,
                OrderTitle_ID = lkpOrderTitle.GetSelectedValue<long>(),
                Price = nmrPrice.Pattern,
                ID = string.IsNullOrEmpty(txtLackID.Text) ? 0 : long.Parse(txtLackID.Text),
                BranchOrder_ID = string.IsNullOrEmpty(txtOrderID.Text) ? 0 : long.Parse(txtOrderID.Text),
                Status = lkpOrderStatus.GetSelectedValue<BranchLackOrderStatus>(),
                OrderedPersianDate = pdrRegDate.DateRange
            };
        }
        set
        {
            //txtTitle.Text = value.Title;
            txtOrderID.Text = (value.BranchOrder_ID > 0) ? value.BranchOrder_ID.ToString() : "";
            txtLackID.Text = (value.ID > 0) ? value.ID.ToString() : "";
            lkpOrderTitle.SetSelectedValue(value.OrderTitle_ID);
            nmrPrice.Pattern = value.Price;
            lkpOrderStatus.SetSelectedValue(value.Status);
            pdrRegDate.DateRange = value.OrderedPersianDate;
        }
    }

    public override BranchLackOrderPattern DefaultPattern
    {
        get
        {
            var _Page = this.Page as BaseManagementPage<BranchLackOrderBusiness, BranchLackOrder, BranchLackOrderPattern, pgcEntities>;

            if (_Page.HasValidQueryString<long>(QueryStringKeys.id))
            {
                BranchLackOrder lackOrder = _Page.Business.Retrieve(_Page.GetQueryStringValue<long>(QueryStringKeys.id));
                if (lackOrder != null && lackOrder.ID > 0)
                {
                    _Page.Mode = kFrameWork.Enums.ManagementPageMode.Edit;
                    _Page.SelectedID = lackOrder.ID;
                    _Page.DetailControl.BeginMode(kFrameWork.Enums.ManagementPageMode.Edit);
                    _Page.DetailControl.SetEntity(_Page.Business.Retrieve(_Page.SelectedID), ManagementPageMode.Edit);
                }
                else
                {
                    _Page.Mode = kFrameWork.Enums.ManagementPageMode.Add;
                    _Page.DetailControl.BeginMode(kFrameWork.Enums.ManagementPageMode.Add);
                }
            }


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

            return new BranchLackOrderPattern()
            {
                Branch_ID = UserSession.User.Branch_ID.Value,
                OrderedPersianDate = new DateRangePattern()
                {
                    SearchMode = DateRangePattern.SearchType.Between,
                    FromDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-7)),
                    ToDate = DateUtil.GetPersianDateShortString(DateTime.Now)
                }
            };
        }
    }

    public override BranchLackOrderPattern SearchAllPattern
    {
        get
        {
            return new BranchLackOrderPattern() { Branch_ID = UserSession.User.Branch_ID.Value };
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