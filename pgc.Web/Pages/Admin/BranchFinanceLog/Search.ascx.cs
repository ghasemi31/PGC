using kFrameWork.UI;
using kFrameWork.Util;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;
using System;

public partial class Pages_Admin_BranchFinanceLog_Search : BaseSearchControl<BranchFinanceLogPattern>
{
    public override BranchFinanceLogPattern Pattern
    {
        get
        {

            return new BranchFinanceLogPattern()
            {
                Title = txtTitle.Text,
                Branch_ID = lkpBranch.GetSelectedValue<long>(),
                PersianDate = pdrRegDate.DateRange,
                ActionType = lkpAction.GetSelectedValue<BranchFinanceLogActionType>(),
                LogType=lkpLogType.GetSelectedValue<BranchFinanceLogType>(),
                LogType_ID=string.IsNullOrEmpty(txtLogTypeID.Text)?0:long.Parse(txtLogTypeID.Text)
            };
        }
        set
        {
            txtTitle.Text = value.Title;
            lkpBranch.SetSelectedValue(value.Branch_ID);
            pdrRegDate.DateRange = value.PersianDate;
            lkpAction.SetSelectedValue(value.ActionType);
            lkpLogType.SetSelectedValue(value.LogType);
            txtLogTypeID.Text = (value.LogType_ID > 0) ? value.LogType_ID.ToString() : "";
        }
    }

    public override BranchFinanceLogPattern DefaultPattern
    {
        get
        {
            BranchFinanceLogPattern p = new BranchFinanceLogPattern();

            var _Page = this.Page as BaseManagementPage<BranchFinanceLogBusiness, BranchFinanceLog, BranchFinanceLogPattern, pgcEntities>;

            if (_Page.HasValidQueryString<long>(QueryStringKeys.fid) && _Page.HasValidQueryString<string>(QueryStringKeys.logtype))
            {
                p.LogType_ID = _Page.GetQueryStringValue<long>(QueryStringKeys.fid);
                p.LogType = (BranchFinanceLogType)Enum.Parse(typeof(BranchFinanceLogType), _Page.GetQueryStringValue<string>(QueryStringKeys.logtype));
            }


            DateRangePattern date = new DateRangePattern()
            {
                SearchMode = DateRangePattern.SearchType.Between,
                FromDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-31)),
                ToDate = DateUtil.GetPersianDateShortString(DateTime.Now)
            };

            p.PersianDate = date;
            return p;
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        var _Page = this.Page as BaseManagementPage<BranchFinanceLogBusiness, BranchFinanceLog, BranchFinanceLogPattern, pgcEntities>;

        if (_Page.HasValidQueryString<long>(QueryStringKeys.fid) && _Page.HasValidQueryString<string>(QueryStringKeys.logtype))
            _Page.SearchControl.Visible = false;
        
        Session["BranchFinanceLogPrintPattern"] = Pattern;
    }

}