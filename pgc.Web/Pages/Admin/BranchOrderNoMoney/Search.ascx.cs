using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;
using kFrameWork.Util;
using System;
using kFrameWork.Business;

public partial class Pages_Admin_BranchOrderNoMoney_Search : BaseSearchControl<BranchOrderPattern>
{
    public override BranchOrderPattern Pattern
    {
        get
        {
            return new BranchOrderPattern()
            {
                ID = string.IsNullOrEmpty(txtID.Text) ? 0 : long.Parse(txtID.Text),
                Branch_ID=lkpBranch.GetSelectedValue<long>(),
                OrderTitle_ID=lkpOrderTitle.GetSelectedValue<long>(),
                OrderedPersianDate=pdrDeliver.DateRange,
                //Price=nmrPrice.Pattern,
                Status=lkpOrderStatus.GetSelectedValue<BranchOrderStatus>()
            };
        }
        set
        {
            txtID.Text = (value.ID > 0) ? value.ID.ToString() : "";
            lkpBranch.SetSelectedValue(value.Branch_ID);
            lkpOrderTitle.SetSelectedValue(value.OrderTitle_ID);
            pdrDeliver.DateRange = value.OrderedPersianDate;
            //nmrPrice.Pattern=value.Price;
            lkpOrderStatus.SetSelectedValue(value.Status);            
        }
    }

    public override BranchOrderPattern DefaultPattern
    {
        get
        {
            var _Page = this.Page as BaseManagementPage<BranchOrderBusiness, BranchOrder, BranchOrderPattern, pgcEntities>;

            if (_Page.HasValidQueryString<long>(QueryStringKeys.id))
            {
                BranchOrder order = _Page.Business.Retrieve(_Page.GetQueryStringValue<long>(QueryStringKeys.id));
                if (order != null && order.ID > 0)
                {
                    _Page.Mode = kFrameWork.Enums.ManagementPageMode.Edit;
                    _Page.SelectedID = order.ID;
                    _Page.DetailControl.BeginMode(kFrameWork.Enums.ManagementPageMode.Edit);
                    _Page.DetailControl.SetEntity(_Page.Business.Retrieve(_Page.SelectedID), kFrameWork.Enums.ManagementPageMode.Edit);
                }
            }

            DateRangePattern deliverDate = new DateRangePattern() { SearchMode = DateRangePattern.SearchType.Equal, Date = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(1)) };
            if ((DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString()).CompareTo(OptionBusiness.GetText(OptionKey.BoundaryTime_For_OrdersSearch)) < 0)
                deliverDate.Date = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(1));
            else
                deliverDate.Date = DateUtil.GetPersianDateShortString(DateTime.Now);
                
            BranchOrderPattern p = new BranchOrderPattern() { OrderedPersianDate = deliverDate };
            return p;
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        Session["BranchOrderPrintPattern"] = Pattern;
        var _Page = this.Page as BaseManagementPage<BranchOrderBusiness, BranchOrder, BranchOrderPattern, pgcEntities>;
        _Page.ListControl.Grid.DataBind();
    }
}