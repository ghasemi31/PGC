using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using System;
using System.Linq;
using pgc.Business;

public partial class Pages_Admin_BranchOrderedTitle_Search : BaseSearchControl<BranchOrderedTitlePattern>
{
    public override BranchOrderedTitlePattern Pattern
    {
        get
        {
            return new BranchOrderedTitlePattern()
            {
                GroupTitle_ID = lkpGroup.GetSelectedValue<long>(),
                Branch_ID = lkpBranch.GetSelectedValue<long>(),
                //OrderTitle_ID = lkpOrderTitle.GetSelectedValue<long>(),
                PersianDate = pdrDeliverDate.DateRange,
                Status = lkpStatus.GetSelectedValue<BranchOrderedTitleStatus>()
            };
        }
        set
        {
            lkpGroup.SetSelectedValue(value.GroupTitle_ID);
            lkpBranch.SetSelectedValue(value.Branch_ID);
            //lkpOrderTitle.SetSelectedValue(value.OrderTitle_ID);
            pdrDeliverDate.DateRange = value.PersianDate;
            lkpStatus.SetSelectedValue(value.Status);
        }
    }

    public override BranchOrderedTitlePattern DefaultPattern
    {
        get
        {
            DateRangePattern date = new DateRangePattern()
            {
                SearchMode = DateRangePattern.SearchType.Between,
                FromDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-31)),
                ToDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-1))
            };

            //var bgBus = new BranchOrderTitleGroupBusiness();
            //long gID = bgBus.Search_Where(bgBus.Context.BranchOrderTitleGroups, new BranchOrderTitleGroupPattern()).First().ID;

            return new BranchOrderedTitlePattern()
            {
                Status = BranchOrderedTitleStatus.Prepared,
                PersianDate = date,
                //GroupTitle_ID=gID
            };
        }
    }

    public override BranchOrderedTitlePattern SearchAllPattern
    {
        get
        {
            lkpBranch.SetSelectedValue(-1);
            //lkpOrderTitle.SetSelectedValue(-1);
            pdrDeliverDate.DateRange = new DateRangePattern();
            lkpStatus.SetSelectedValue(-1);

            //var bgBus = new BranchOrderTitleGroupBusiness();
            //long gID = bgBus.Search_Where(bgBus.Context.BranchOrderTitleGroups, new BranchOrderTitleGroupPattern()).First().ID;

            return new BranchOrderedTitlePattern()
            {
                Status = BranchOrderedTitleStatus.Prepared,
                //GroupTitle_ID = gID
            };
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        Session["BranchOrderedTitlePrintPattern"] = Pattern;
    }
}