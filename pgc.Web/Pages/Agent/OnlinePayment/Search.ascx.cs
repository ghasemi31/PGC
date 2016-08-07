using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;
using System.Web.UI.WebControls;
using System;
using kFrameWork.Util;

public partial class Pages_Agent_OnlinePayment_Search : BaseSearchControl<OnlinePaymentPattern>
{
    public override OnlinePaymentPattern Pattern
    {
        get
        {
            long ID=0;
            long.TryParse(txtOrderID.Text, out ID);

            return new OnlinePaymentPattern()
            {
                Amount=nmrAMount.Pattern,
                Order_ID=ID,
                PersianDate=pdrDate.DateRange,
                Status=lkpStatus.GetSelectedValue<OnlineTransactionStatus>(),
                Branch_ID=UserSession.User.Branch_ID.Value
            };
        }
        set
        {
            nmrAMount.Pattern = value.Amount;
            if (value.Order_ID > 0)
                txtOrderID.Text = value.Order_ID.ToString();
            pdrDate.DateRange = value.PersianDate;
            lkpStatus.SetSelectedValue(value.Status);
        }
    }

    public override OnlinePaymentPattern DefaultPattern
    {
        get
        {
            OnlinePaymentPattern p=new OnlinePaymentPattern();
            p.Branch_ID = UserSession.User.Branch_ID.Value;

            if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.fid))
            {
                p.Order_ID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.fid);

                var page = (this.Page as BaseManagementPage<OnlinePaymentBusiness, OnlinePayment, OnlinePaymentPattern, pgcEntities>);

                page.ListControl.FindControl("parentInfo").Visible = true;
                page.ListControl.FindControl("btnEvent").Visible = true;
                string parentInfo = "پرداخت های آنلاین سفارش شماره {0}";
                ((Label)page.ListControl.FindControl("parentInfo")).Text = string.Format(parentInfo, p.Order_ID.ToString());
            }
            return p;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.fid))
        {
            var page = (this.Page as BaseManagementPage<OnlinePaymentBusiness, OnlinePayment, OnlinePaymentPattern, pgcEntities>);
            page.SearchControl.Visible = false;
        }
        base.OnPreRender(e);
    }

    public override OnlinePaymentPattern SearchAllPattern
    {
        get
        {
            return new OnlinePaymentPattern() { Branch_ID = UserSession.User.Branch_ID.Value };
        }
    }
}