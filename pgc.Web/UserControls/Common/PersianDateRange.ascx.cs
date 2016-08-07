using System;
using System.Web.UI;
using kFrameWork.Util;
using kFrameWork.Model;
using kFrameWork.UI;
using pgc.Model;

public partial class UserControl_PersianDateRange : BaseUserControl
{
    public DateRangePattern DateRange
    {
        set
        {
            SearchModeSelector.SelectedValue = ConvertorUtil.ToInt16(value.SearchMode).ToString();
            txtFromDate.PersianDate = (value.SearchMode == DateRangePattern.SearchType.Between) ? value.FromDate : value.Date;
            txtToDate.PersianDate = value.ToDate;
        }
        get
        {
            DateRangePattern.SearchType Mode= (DateRangePattern.SearchType)ConvertorUtil.ToInt16(SearchModeSelector.SelectedValue);
            DateRangePattern Res = new DateRangePattern();
            Res.SearchMode = Mode;
            switch (Mode)
            {
                case DateRangePattern.SearchType.Between:
                    //if (txtFromDate.HasDate)
                        Res.FromDate = txtFromDate.PersianDate;
                    //if (txtToDate.HasDate)
                        Res.ToDate = txtToDate.PersianDate;
                    break;
                case DateRangePattern.SearchType.Equal:
                case DateRangePattern.SearchType.Greater:
                case DateRangePattern.SearchType.Less:
                    //if (txtFromDate.HasDate)
                        Res.Date =  txtFromDate.PersianDate;
                    break;
            }
            return Res;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UIUtil.AddStartupScript(string.Format("SearchModeSelectorChanged( '{0}' , '{1}' , '{2}' , '{3}' )",
                SearchModeSelector.ClientID,
                FromDatePnl.ClientID,
                AndPnl.ClientID,
                ToDatePnl.ClientID),this);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        UIUtil.SetAttribute(SearchModeSelector, "onchange", string.Format("SearchModeSelectorChanged( '{0}' , '{1}' , '{2}' , '{3}' )",
                SearchModeSelector.ClientID,
                FromDatePnl.ClientID,
                AndPnl.ClientID,
                ToDatePnl.ClientID));

        switch (SearchModeSelector.SelectedValue)
        {
            case ("0"):
                FromDatePnl.Style["display"] = "none";
                AndPnl.Style["display"] = "none";
                ToDatePnl.Style["display"] = "none";
                break;
            case ("1"):
            case ("2"):
            case ("3"):
                FromDatePnl.Style["display"] = "block";
                AndPnl.Style["display"] = "none";
                ToDatePnl.Style["display"] = "none";
                break;
            case ("4"):
                FromDatePnl.Style["display"] = "block";
                AndPnl.Style["display"] = "block";
                ToDatePnl.Style["display"] = "block";
                break;
        }
    }
}