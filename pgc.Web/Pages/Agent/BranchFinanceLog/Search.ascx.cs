using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;
using System;
using kFrameWork.Util;

public partial class Pages_Agent_BranchFinanceLog_Search : BaseSearchControl<BranchFinanceLogPattern>
{
    public override BranchFinanceLogPattern Pattern
    {
        get
        {
            
            return new BranchFinanceLogPattern()
            {
                Title = txtTitle.Text,
                Branch_ID=UserSession.User.Branch_ID.Value,
                PersianDate=pdrRegDate.DateRange,
                ActionType=lkpAction.GetSelectedValue<BranchFinanceLogActionType>(),
                LogType = lkpLogType.GetSelectedValue<BranchFinanceLogType>(),
                LogType_ID = string.IsNullOrEmpty(txtLogTypeID.Text) ? 0 : long.Parse(txtLogTypeID.Text)
            };
        }
        set
        {
            txtTitle.Text=value.Title ;
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
            p.Branch_ID = UserSession.User.Branch_ID.Value;
            p.PersianDate = new DateRangePattern()
            {
                SearchMode = DateRangePattern.SearchType.Between,
                FromDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-7)),
                ToDate = DateUtil.GetPersianDateShortString(DateTime.Now)
            };

            var _Page = this.Page as BaseManagementPage<BranchFinanceLogBusiness, BranchFinanceLog, BranchFinanceLogPattern, pgcEntities>;

            if (_Page.HasValidQueryString<long>(QueryStringKeys.fid) && _Page.HasValidQueryString<string>(QueryStringKeys.logtype))
            {
                p.LogType_ID = _Page.GetQueryStringValue<long>(QueryStringKeys.fid);
                p.LogType = (BranchFinanceLogType)Enum.Parse(typeof(BranchFinanceLogType), _Page.GetQueryStringValue<string>(QueryStringKeys.logtype));
            }

            return p;
        }
    }

    public override BranchFinanceLogPattern SearchAllPattern
    {
        get
        {
            BranchFinanceLogPattern p = new BranchFinanceLogPattern();
            p.Branch_ID = UserSession.User.Branch_ID.Value;
            return p;
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        var _Page = this.Page as BaseManagementPage<BranchFinanceLogBusiness, BranchFinanceLog, BranchFinanceLogPattern, pgcEntities>;

        if (_Page.HasValidQueryString<long>(QueryStringKeys.fid) &&
            _Page.HasValidQueryString<string>(QueryStringKeys.logtype) &&
            _Page.ListControl.Grid.Rows.Count > 0)
        {
            Pattern.Branch_ID = UserSession.User.Branch_ID.Value;
            Pattern.LogType_ID = _Page.GetQueryStringValue<long>(QueryStringKeys.fid);
            Pattern.LogType = (BranchFinanceLogType)Enum.Parse(typeof(BranchFinanceLogType), _Page.GetQueryStringValue<string>(QueryStringKeys.logtype));
            _Page.SearchControl.Visible = false;
        }
        Session["BranchFinanceLogPrintPattern"] = Pattern;
    }
}