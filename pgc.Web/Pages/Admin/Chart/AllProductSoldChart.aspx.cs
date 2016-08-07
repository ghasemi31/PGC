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
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Chart_AllProductSoldChart : BasePage
{
    public string chartTitle;
    public string jsonList;
    public ProductSoldChartBusiness business = new ProductSoldChartBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {
        ProductSoldChartPattern pattern = new ProductSoldChartPattern();
        DateRangePattern dateRange = new DateRangePattern();
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode)==DateRangePattern.SearchType.Between.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Between;
            dateRange.FromDate = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            dateRange.ToDate = this.GetQueryStringValue<string>(QueryStringKeys.toDate);
            pattern.PersianDate = dateRange;
            chartTitle = string.Format("نمودار روند فروش کل از تاریخ {0} تا {1}", pattern.PersianDate.FromDate, pattern.PersianDate.ToDate);
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode)==DateRangePattern.SearchType.Equal.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Equal;
                    dateRange.Date = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
                    pattern.PersianDate = dateRange;
                    chartTitle = string.Format("نمودار روند فروش کل در تاریخ {0}", pattern.PersianDate.Date);
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode)==DateRangePattern.SearchType.Greater.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Greater;
            dateRange.Date = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            pattern.PersianDate = dateRange;
            chartTitle = string.Format("نمودار روند فروش کل بعد از تاریخ {0}", pattern.PersianDate.Date);
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Less.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Less;
            dateRange.Date = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            pattern.PersianDate = dateRange;
            chartTitle = string.Format("نمودار روند فروش کل قبل از تاریخ {0}", pattern.PersianDate.Date);
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Nothing.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Nothing;
            pattern.PersianDate = dateRange;
            chartTitle = "نمودار روند فروش کل";
        }
        pattern.Branch_ID = 0;
        pattern.Product_ID = 0;
        pattern.ProductGroup_ID = 0;

        var jsonSerialiser = new JavaScriptSerializer();
        jsonList = jsonSerialiser.Serialize(business.TransactionList(pattern));
    }
}