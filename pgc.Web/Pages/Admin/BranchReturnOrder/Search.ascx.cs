using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model;
using kFrameWork.Util;
using System;
using kFrameWork.Business;

public partial class Pages_Admin_BranchReturnOrder_Search : BaseSearchControl<BranchReturnOrderPattern>
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

    public override BranchReturnOrderPattern DefaultPattern
    {
        get
        {
            var _Page = this.Page as BaseManagementPage<BranchReturnOrderBusiness, BranchReturnOrder, BranchReturnOrderPattern, pgcEntities>;

            if (_Page.HasValidQueryString<long>(QueryStringKeys.id))
            {
                BranchReturnOrder order = _Page.Business.Retrieve(_Page.GetQueryStringValue<long>(QueryStringKeys.id));
                if (order != null && order.ID > 0)
                {
                    _Page.Mode = kFrameWork.Enums.ManagementPageMode.Edit;
                    _Page.SelectedID = order.ID;
                    _Page.DetailControl.BeginMode(kFrameWork.Enums.ManagementPageMode.Edit);
                    _Page.DetailControl.SetEntity(_Page.Business.Retrieve(_Page.SelectedID), kFrameWork.Enums.ManagementPageMode.Edit);
                }
            }

            DateRangePattern deliverDate = new DateRangePattern() { SearchMode = DateRangePattern.SearchType.Equal, Date = DateUtil.GetPersianDateShortString(DateTime.Now) };

            //if ((DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString()).CompareTo(OptionBusiness.GetText(OptionKey.BoundaryTime_For_OrdersSearch)) < 0)
            //    deliverDate.Date = DateUtil.GetPersianDateShortString(DateTime.Now);
            //else
            //    deliverDate.Date = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-1));

            return new BranchReturnOrderPattern() { OrderedPersianDate = deliverDate };
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        Session["BranchReturnPrintPattern"] = Pattern;
        var _page = this.Page as BaseManagementPage<BranchReturnOrderBusiness, BranchReturnOrder, BranchReturnOrderPattern, pgcEntities>;
        _page.ListControl.Grid.DataBind();
    }
}