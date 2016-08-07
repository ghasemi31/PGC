using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;
using System;
using kFrameWork.Util;

public partial class Pages_Agent_BranchReturnOrderNoMoney_Search : BaseSearchControl<BranchReturnOrderPattern>
{
    public override BranchReturnOrderPattern Pattern
    {
        get
        {
            return new BranchReturnOrderPattern()
            {
                //Title = txtTitle.Text,
                Branch_ID = UserSession.User.Branch_ID.Value,
                OrderTitle_ID = lkpOrderTitle.GetSelectedValue<long>(),
                ID = string.IsNullOrEmpty(txtID.Text) ? 0 : long.Parse(txtID.Text),
                //Price = nmrPrice.Pattern,
                Status = lkpOrderStatus.GetSelectedValue<BranchReturnOrderStatus>(),
                OrderedPersianDate = pdrRegDate.DateRange
            };
        }
        set
        {
            //txtTitle.Text = value.Title;            
            lkpOrderTitle.SetSelectedValue(value.OrderTitle_ID);
            txtID.Text = (value.ID > 0) ? value.ID.ToString() : "";
            //nmrPrice.Pattern = value.Price;
            pdrRegDate.DateRange = value.OrderedPersianDate;
            lkpOrderStatus.SetSelectedValue(value.Status);
        }
    }

    public override BranchReturnOrderPattern DefaultPattern
    {
        get
        {
            var _Page = this.Page as BaseManagementPage<BranchReturnOrderBusiness, BranchReturnOrder, BranchReturnOrderPattern, pgcEntities>;

            if (_Page.HasValidQueryString<long>(QueryStringKeys.id))
            {
                BranchReturnOrder returnOrder = _Page.Business.Retrieve(_Page.GetQueryStringValue<long>(QueryStringKeys.id));
                if (returnOrder != null && returnOrder.ID > 0)
                {
                    _Page.Mode = kFrameWork.Enums.ManagementPageMode.Edit;
                    _Page.SelectedID = returnOrder.ID;
                    _Page.DetailControl.BeginMode(kFrameWork.Enums.ManagementPageMode.Edit);
                    _Page.DetailControl.SetEntity(_Page.Business.Retrieve(_Page.SelectedID), kFrameWork.Enums.ManagementPageMode.Edit);
                }
                else
                {
                    _Page.Mode = kFrameWork.Enums.ManagementPageMode.Add;
                    _Page.DetailControl.BeginMode(kFrameWork.Enums.ManagementPageMode.Add);
                }
            }

            return new BranchReturnOrderPattern()
            {
                Branch_ID = UserSession.User.Branch_ID.Value
                ,
                OrderedPersianDate = new DateRangePattern()
                  {
                      SearchMode = DateRangePattern.SearchType.Between,
                      FromDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-7)),
                      ToDate = DateUtil.GetPersianDateShortString(DateTime.Now)
                  }
            };
        }
    }

    public override BranchReturnOrderPattern SearchAllPattern
    {
        get
        {
            return new BranchReturnOrderPattern() { Branch_ID = UserSession.User.Branch_ID.Value };
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