using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;
using kFrameWork.Util;
using System;

public partial class Pages_Agent_BranchTransaction_Search : BaseSearchControl<BranchTransactionPattern>
{
    public override BranchTransactionPattern Pattern
    {
        get
        {
            return new BranchTransactionPattern()
            {
                Title = txtLogTypeID.Text,
                Branch_ID = UserSession.User.Branch_ID.Value,
                BranchCreditPrice = nmrBranchCredit.Pattern,
                BranchDebtPrice = nmrBranchDebt.Pattern,
                PersianDate = pdrRegDate.DateRange,
                Type = lkpTranType.GetSelectedValue<BranchTransactionTypeForSearch>()
            };
        }
        set
        {
            txtLogTypeID.Text = value.Title;
            nmrBranchCredit.Pattern = value.BranchCreditPrice;
            nmrBranchDebt.Pattern = value.BranchDebtPrice;
            pdrRegDate.DateRange = value.PersianDate;
            lkpTranType.SetSelectedValue(value.Type);
        }
    }

    public override BranchTransactionPattern DefaultPattern
    {
        get
        {
            BranchTransactionPattern p = new BranchTransactionPattern();
            p.Branch_ID = UserSession.User.Branch_ID.Value;
            p.PersianDate = new DateRangePattern()
               {
                   SearchMode = DateRangePattern.SearchType.Between,
                   FromDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-7)),
                   ToDate = DateUtil.GetPersianDateShortString(DateTime.Now)
               };
            var _Page = this.Page as BaseManagementPage<BranchTransactionBusiness, BranchTransaction, BranchTransactionPattern, pgcEntities>;

            if (_Page.HasValidQueryString<long>(QueryStringKeys.fid))
            {
                p.Branch_ID = _Page.GetQueryStringValue<long>(QueryStringKeys.fid);
            }
            return p;
        }
    }

    public override BranchTransactionPattern SearchAllPattern
    {
        get
        {
            BranchTransactionPattern p = new BranchTransactionPattern();
            p.Branch_ID = UserSession.User.Branch_ID.Value;
            return p;
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        Session["BranchTransactionPrintPattern"] = Pattern;
    }
}