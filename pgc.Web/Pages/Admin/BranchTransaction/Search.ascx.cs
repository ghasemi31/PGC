using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;

public partial class Pages_Admin_BranchTransaction_Search : BaseSearchControl<BranchTransactionPattern>
{
    public override BranchTransactionPattern Pattern
    {
        get
        {
            return new BranchTransactionPattern()
            {
                Title = txtLogTypeID.Text,
                Branch_ID=lkpBranch.GetSelectedValue<long>(),
                BranchCreditPrice=nmrBranchCredit.Pattern,
                BranchDebtPrice=nmrBranchDebt.Pattern,
                PersianDate=pdrRegDate.DateRange,
                Type=lkpTranType.GetSelectedValue<BranchTransactionTypeForSearch>(),
            };
        }
        set
        {
            txtLogTypeID.Text = value.Title;
            lkpBranch.SetSelectedValue(value.Branch_ID);
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

            var _Page = this.Page as BaseManagementPage<BranchTransactionBusiness, BranchTransaction, BranchTransactionPattern, pgcEntities>;
            
            if (_Page.HasValidQueryString<long>(QueryStringKeys.fid))
            {
                p.Branch_ID = _Page.GetQueryStringValue<long>(QueryStringKeys.fid);
            }
            return p;
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        Session["BranchTransactionPrintPattern"] = Pattern;
    }
}