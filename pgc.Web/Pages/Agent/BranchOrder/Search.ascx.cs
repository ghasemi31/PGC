using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model;
using System;
using kFrameWork.Util;

public partial class Pages_Agent_BranchOrder_Search : BaseSearchControl<BranchOrderPattern>
{
    public override BranchOrderPattern Pattern
    {
        get
        {
            return new BranchOrderPattern()
            {
                Branch_ID = UserSession.User.Branch_ID.Value,
                ID = string.IsNullOrEmpty(txtID.Text) ? 0 : long.Parse(txtID.Text),
                OrderTitle_ID = lkpOrderTitle.GetSelectedValue<long>(),
                Price = nmrPrice.Pattern,
                OrderedPersianDate = pdrRegDate.DateRange,
                Status = lkpStatus.GetSelectedValue<BranchOrderStatus>()
            };
        }
        set
        {
            lkpOrderTitle.SetSelectedValue(value.OrderTitle_ID);
            pdrRegDate.DateRange = value.OrderedPersianDate;
            txtID.Text = (value.ID > 0) ? value.ID.ToString() : "";
            nmrPrice.Pattern = value.Price;
            lkpStatus.SetSelectedValue(value.Status);
        }
    }

    public override BranchOrderPattern DefaultPattern
    {
        get
        {
            var _page = this.Page as BaseManagementPage<BranchOrderBusiness, BranchOrder, BranchOrderPattern, pgcEntities>;

            if (_page.HasValidQueryString<long>(QueryStringKeys.id))
            {
                _page.SelectedID = _page.GetQueryStringValue<long>(QueryStringKeys.id);
                _page.Mode = kFrameWork.Enums.ManagementPageMode.Edit;
                _page.DetailControl.SetEntity(_page.Business.Retrieve(_page.SelectedID), kFrameWork.Enums.ManagementPageMode.Edit);
            }

            return new BranchOrderPattern()
            {
                Branch_ID = UserSession.User.Branch_ID.Value
                ,
                OrderedPersianDate = new DateRangePattern()
                {
                    SearchMode = DateRangePattern.SearchType.Between,
                    FromDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-6)),
                    ToDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(1))
                }
            };
        }
    }

    public override BranchOrderPattern SearchAllPattern
    {
        get
        {
            return new BranchOrderPattern() { Branch_ID = UserSession.User.Branch_ID.Value };
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        (this.Page as BaseManagementPage<BranchOrderBusiness, BranchOrder, BranchOrderPattern, pgcEntities>).ListControl.Grid.DataBind();
        Session["BranchOrderPrintPattern"] = Pattern;
    }
}