using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using kFrameWork.Util;
using System;

public partial class Pages_Admin_BranchSold_Search : BaseSearchControl<BranchSoldPattern>
{
    public override BranchSoldPattern Pattern
    {
        get
        {
            return new BranchSoldPattern()
            {
                //GroupTitle_ID=lkpGroup.GetSelectedValue<long>(),
                Branch_ID=lkpBranch.GetSelectedValue<long>(),
                Type=lkpType.GetSelectedValue<BranchSoldType>(),
                //OrderTitle_ID=lkpOrderTitle.GetSelectedValue<long>(),
                //Price=nmrPrice.Pattern,
                SoldPersianDate=pdrDate.DateRange
            };
        }
        set
        {
            //lkpGroup.SetSelectedValue(value.GroupTitle_ID);
            lkpBranch.SetSelectedValue(value.Branch_ID);
            lkpType.SetSelectedValue(value.Type);
            //lkpOrderTitle.SetSelectedValue(value.OrderTitle_ID);
            //nmrPrice.Pattern = value.Price;
            pdrDate.DateRange = value.SoldPersianDate;
        }
    }

    public override BranchSoldPattern DefaultPattern
    {
        get
        {
            DateRangePattern sodlDefaultDatePattern = new DateRangePattern()
            {
                SearchMode = DateRangePattern.SearchType.Between,
                FromDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-31)),
                ToDate = DateUtil.GetPersianDateShortString(DateTime.Now)
            };
            return new BranchSoldPattern() { Type = BranchSoldType.Sold, SoldPersianDate = sodlDefaultDatePattern };
        }
    }

    public override BranchSoldPattern SearchAllPattern
    {
        get
        {
            lkpBranch.SetSelectedValue(-1);
            lkpType.SetSelectedValue(-1);
            //lkpOrderTitle.SetSelectedValue(-1);
            //nmrPrice.Pattern = new NumericRangePattern();
            pdrDate.DateRange = new DateRangePattern();
            return new BranchSoldPattern() { Type = BranchSoldType.Sold };
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        Session["BranchSoldPrintPattern"] = Pattern;
    }
}