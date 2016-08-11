using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;
using System.Web.UI.WebControls;
using System;

public partial class Pages_Admin_Payment_Search : BaseSearchControl<PaymentPattern>
{
    public override PaymentPattern Pattern
    {
        get
        {
            long ID=0;
            long.TryParse(txtOrderID.Text, out ID);

            return new PaymentPattern()
            {
                Amount=nmrAMount.Pattern,
                Order_ID=ID,
                PersianDate=pdrDate.DateRange,
                Status = lkpStatus.GetSelectedValue<GameOrderPaymentStatus>(),
                Game_ID=lkcGame.GetSelectedValue<long>()
            };
        }
        set
        {
            nmrAMount.Pattern = value.Amount;
            if (value.Order_ID > 0)
                txtOrderID.Text = value.Order_ID.ToString();
            pdrDate.DateRange = value.PersianDate;
            lkpStatus.SetSelectedValue(value.Status);
            lkcGame.SetSelectedValue(value.Game_ID);
        }
    }

    public override PaymentPattern DefaultPattern
    {
        get
        {
            PaymentPattern p=new PaymentPattern();

            if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.fid))
            {
                p.Order_ID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.fid);

                var page = (this.Page as BaseManagementPage<PaymentBusiness, Payment, PaymentPattern, pgcEntities>);

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
            var page = (this.Page as BaseManagementPage<PaymentBusiness, Payment, PaymentPattern, pgcEntities>);
            page.SearchControl.Visible = false;
        }
        base.OnPreRender(e);
    }
}