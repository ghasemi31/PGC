using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using pgc.Model.Enums;
using pgc.Model.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Chart_BranchSoldChart : BasePage
{
    public string chartTitle;
    public string jsonList;
    public ProductSoldChartBusiness business = new ProductSoldChartBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {
        ProductSoldChartPattern pattern = new ProductSoldChartPattern();
        DateRangePattern dateRange = new DateRangePattern();
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Between.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Between;
            dateRange.FromDate = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            dateRange.ToDate = this.GetQueryStringValue<string>(QueryStringKeys.toDate);
            pattern.PersianDate = dateRange;
            chartTitle = string.Format("نمودار روند فروش {2} از تاریخ {0} تا {1}", pattern.PersianDate.FromDate, pattern.PersianDate.ToDate, business.RetriveBranchName(this.GetQueryStringValue<long>(QueryStringKeys.id)));
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Equal.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Equal;
            dateRange.Date = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            pattern.PersianDate = dateRange;
            chartTitle = string.Format("نمودار روند فروش {1} در تاریخ {0}", pattern.PersianDate.Date, business.RetriveBranchName(this.GetQueryStringValue<long>(QueryStringKeys.id)));
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Greater.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Greater;
            dateRange.Date = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            pattern.PersianDate = dateRange;
            chartTitle = string.Format("نمودار روند فروش {1} بعد از تاریخ {0}", pattern.PersianDate.Date, business.RetriveBranchName(this.GetQueryStringValue<long>(QueryStringKeys.id)));
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Less.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Less;
            dateRange.Date = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            pattern.PersianDate = dateRange;
            chartTitle = string.Format("نمودار روند فروش {1} قبل از تاریخ {0}", pattern.PersianDate.Date, business.RetriveBranchName(this.GetQueryStringValue<long>(QueryStringKeys.id)));
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Nothing.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Nothing;
            pattern.PersianDate = dateRange;
            chartTitle = string.Format("نمودار روند فروش {0}", business.RetriveBranchName(this.GetQueryStringValue<long>(QueryStringKeys.id)));
        }
        pattern.Branch_ID = this.GetQueryStringValue<long>(QueryStringKeys.id);
        pattern.Product_ID = 0;
        pattern.ProductGroup_ID = 0;

        var jsonSerialiser = new JavaScriptSerializer();
        jsonList = jsonSerialiser.Serialize(business.TransactionList(pattern));
        
    }

}