using pgc.Model;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using pgc.Business.General;
using kFrameWork.UI;
using pgc.Model.Enums;

public partial class Pages_Admin_Chart_BranchSummary : BasePage
{
    public string chartTitle;
    //public List<BranchSummary> myList;
    public string jsonList;
    public long minPrice;
    public BranchSummaryChartBusiness business = new BranchSummaryChartBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {
        BranchSummaryChartPattern pattern = new BranchSummaryChartPattern();
        DateRangePattern dateRange = new DateRangePattern();
        pattern.MinPrice = this.GetQueryStringValue<long>(QueryStringKeys.minPrice);
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Between.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Between;
            dateRange.FromDate = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            dateRange.ToDate = this.GetQueryStringValue<string>(QueryStringKeys.toDate);
            pattern.PersianDate = dateRange;
            chartTitle = string.Format("نمودار بیلان از تاریخ {0} تا {1}", pattern.PersianDate.FromDate, pattern.PersianDate.ToDate);
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Equal.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Equal;
            dateRange.Date = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            pattern.PersianDate = dateRange;
            chartTitle = string.Format("نمودار بیلان در تاریخ {0}", pattern.PersianDate.Date);
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Greater.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Greater;
            dateRange.Date = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            pattern.PersianDate = dateRange;
            chartTitle = string.Format("نمودار بیلان بعد از تاریخ {0}", pattern.PersianDate.Date);
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Less.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Less;
            dateRange.Date = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            pattern.PersianDate = dateRange;
            chartTitle = string.Format("نمودار بیلان قبل از تاریخ {0}", pattern.PersianDate.Date);
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Nothing.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Nothing;
            pattern.PersianDate = dateRange;
            chartTitle = "نمودار بیلان";
        }

        minPrice = pattern.MinPrice;

        var jsonSerialiser = new JavaScriptSerializer();
        jsonList = jsonSerialiser.Serialize(business.Search_Where(pattern));

    }

    //public class BranchSummary
    //{
    //    public string branchName { get; set; }
    //    public long Summary { get; set; }
    //    public long Credit { get; set; }
    //}

}