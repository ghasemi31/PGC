using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using kFrameWork.Util;
using System;

public partial class Pages_Admin_BranchPayment_Search : BaseSearchControl<BranchPaymentPattern>
{
    public override BranchPaymentPattern Pattern
    {
        get
        {
            return new BranchPaymentPattern()
            {
                Title = txtTitle.Text,
                Branch_ID = UserSession.User.Branch_ID.Value,
                PayDate = pdrDate.DateRange,
                Price = nmrPrice.Pattern,
                Type = lkpPaymentType.GetSelectedValue<BranchPaymentTypeForSearch>()
            };
        }
        set
        {
            txtTitle.Text = value.Title;
            pdrDate.DateRange = value.PayDate;
            nmrPrice.Pattern = value.Price;
            lkpPaymentType.SetSelectedValue(value.Type);
        }
    }

    public override BranchPaymentPattern DefaultPattern
    {
        get
        {
            return new BranchPaymentPattern()
            {
                Branch_ID = UserSession.User.Branch_ID.Value,
                PayDate = new DateRangePattern()
               {
                   SearchMode = DateRangePattern.SearchType.Between,
                   FromDate = DateUtil.GetPersianDateShortString(DateTime.Now.AddDays(-7)),
                   ToDate = DateUtil.GetPersianDateShortString(DateTime.Now)
               }
            };
        }
    }

    public override BranchPaymentPattern SearchAllPattern
    {
        get
        {
            return new BranchPaymentPattern() { Branch_ID = UserSession.User.Branch_ID.Value };
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        Session["BranchPaymentPrintPattern"] = Pattern;
    }
}