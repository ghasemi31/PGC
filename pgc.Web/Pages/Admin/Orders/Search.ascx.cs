using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Model;
using pgc.Business;
using System;
using System.Web.UI.WebControls;
using kFrameWork.Enums;

public partial class Pages_Admin_Order_Search : BaseSearchControl<OrdersPattern>
{
  
    public override OrdersPattern Pattern
    {
        get
        {
            OrdersPattern pattern= new OrdersPattern()
            {       
                Status = lkcStatus.GetSelectedValue<OrderStatus>(),
                OrderPersianDate = pdrOrderPersianDate.DateRange,
                OrderPaymentStatus = lkcPaymentStatus.GetSelectedValue<OrderPaymentStatus>(),
                Branch_ID = lkcBranch.GetSelectedValue<long>(),
                Product_ID = lkcProduct.GetSelectedValue<long>(),
                Amount = nrAmount.Pattern,
                RefNum=txtRefNum.Text,                
                UserName=txtUser.Text
            };
            
            int number=0;
            int.TryParse(txtNumber.Text,out number);
            pattern.Numbers = number;
            Session["OrderPattern"] = pattern;
            return pattern;
        }
        set
        {

            txtNumber.Text = (value.Numbers == 0) ? "" : value.Numbers.ToString();
            lkcBranch.SetSelectedValue(value.Branch_ID);
            lkcPaymentStatus.SetSelectedValue(value.OrderPaymentStatus);
            lkcProduct.SetSelectedValue(value.Product_ID);
            lkcStatus.SetSelectedValue(value.Status);
            pdrOrderPersianDate.DateRange = value.OrderPersianDate;
            nrAmount.Pattern = value.Amount;
            txtRefNum.Text = value.RefNum;
            txtUser.Text = value.UserName;
        }
    }
    public override OrdersPattern DefaultPattern
    {
        get
        {
            OrdersPattern p = new OrdersPattern();
            if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.id))
            {
                if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.id))
                {
                    var page = (this.Page as BaseManagementPage<OrdersBusiness, Order, OrdersPattern, pgcEntities>);
                    page.SelectedID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.id);
                    page.DetailControl.BeginMode(ManagementPageMode.Edit);
                    page.DetailControl.SetEntity(new OrdersBusiness().Retrieve(page.SelectedID), ManagementPageMode.Edit);
                    page.Mode = kFrameWork.Enums.ManagementPageMode.Edit;
                }                
            }
            
            if (Session["OrderPattern"] != null)
            {
                try { p = (OrdersPattern)Session["OrderPattern"]; }
                catch (Exception) { }
            }
            return p;
        }
    }

    public override OrdersPattern SearchAllPattern
    {
        get
        {
            Session["OrderPattern"] = null;
            return base.SearchAllPattern;
        }
    }
}