using kFrameWork.UI;
using pgc.Business.General;
using pgc.Model;
using pgc.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Chart_SoldCostPie : BasePage
{
    public string chartTitle;
    public string jsonList;
    protected void Page_Load(object sender, EventArgs e)
    {
        DateRangePattern dateRange = new DateRangePattern();
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Between.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Between;
            dateRange.FromDate = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            dateRange.ToDate = this.GetQueryStringValue<string>(QueryStringKeys.toDate);
            chartTitle = string.Format("نمودار سهم شعب از درآمد از تاریخ {0} تا {1}", dateRange.FromDate, dateRange.ToDate);
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Equal.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Equal;
            dateRange.Date = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            chartTitle = string.Format("نمودار سهم شعب از درآمد در تاریخ {0}", dateRange.Date);
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Greater.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Greater;
            dateRange.Date = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            chartTitle = string.Format("نمودار سهم شعب از درآمد بعد از تاریخ {0}", dateRange.Date);
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Less.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Less;
            dateRange.Date = this.GetQueryStringValue<string>(QueryStringKeys.fromDate);
            chartTitle = string.Format("نمودار سهم شعب از درآمد قبل از تاریخ {0}", dateRange.Date);
        }
        if (this.GetQueryStringValue<string>(QueryStringKeys.searchMode) == DateRangePattern.SearchType.Nothing.ToString())
        {
            dateRange.SearchMode = DateRangePattern.SearchType.Nothing;
            chartTitle = "نمودار سهم شعب از درآمد";
        }

        SoldCostPieChartBusiness business = new SoldCostPieChartBusiness();
        var jsonSerialiser = new JavaScriptSerializer();
        jsonList = jsonSerialiser.Serialize(business.TransactionList(dateRange));


    }
}