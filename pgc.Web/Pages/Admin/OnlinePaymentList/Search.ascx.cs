using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;
using System.Web.UI.WebControls;
using System;

public partial class Pages_Admin_OnlinePaymentList_Search : BaseSearchControl<OnlinePaymentListPattern>
{
    public override OnlinePaymentListPattern Pattern
    {
        get
        {
            return new OnlinePaymentListPattern()
            {
                Amount = nmrAMount.Pattern,
                //Branch_ID = lkpBranch.GetSelectedValue<long>(),
                PersianDate = pdrDate.DateRange,
                Status = lkpStatus.GetSelectedValue<OnlinePaymentStatus>()
            };
        }
        set
        {
            nmrAMount.Pattern = value.Amount;
            pdrDate.DateRange = value.PersianDate;
            //lkpBranch.SetSelectedValue(value.Branch_ID);
            lkpStatus.SetSelectedValue(value.Status);
        }
    }

    public override OnlinePaymentListPattern DefaultPattern
    {
        get
        {
            OnlinePaymentListPattern p = new OnlinePaymentListPattern();

            if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.fid))
            {
                p.Order_ID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.fid);

                var page = (this.Page as BaseManagementPage<OnlinePaymentListBusiness, OnlinePaymentList, OnlinePaymentListPattern, pgcEntities>);

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
            var page = (this.Page as BaseManagementPage<OnlinePaymentListBusiness, OnlinePaymentList, OnlinePaymentListPattern, pgcEntities>);
            page.SearchControl.Visible = false;
        }
        base.OnPreRender(e);
        Session["OnlinePaymentListPattern"] = Pattern;
    }
}