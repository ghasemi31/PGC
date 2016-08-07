using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model;
using kFrameWork.Enums;
using System;
using System.Web.UI.WebControls;

public partial class Pages_Agent_Order_Search : BaseSearchControl<OrdersAgentPattern>
{
    public void IndexChanging(object sender, EventArgs e)
    {
        var page = (this.Page as BaseManagementPage<OrdersAgentBusiness, Order, OrdersAgentPattern, pgcEntities>);
        page.ListControl.DataBind();
    }
    public override OrdersAgentPattern Pattern
    {
        get
        {
            OrdersAgentPattern pattern= new OrdersAgentPattern()
            {
                Status=lkcStatus.GetSelectedValue<OrderStatus>(),
                OrderPersianDate=pdrOrderPersianDate.DateRange,
                OrderPaymentStatus=lkcPaymentStatus.GetSelectedValue<OrderPaymentStatus>(),
                Product_ID=lkcProduct.GetSelectedValue<long>(),
                Amount = nrAmount.Pattern,
                RefNum = txtRefNum.Text,
                UserName = txtUser.Text
            };

            int number = 0;
            int.TryParse(txtNumber.Text, out number);
            pattern.Numbers = number;
            Session["OrderPattern"] = pattern;
            return pattern;
        }
        set
        {
            txtNumber.Text = (value.Numbers == 0) ? "" : value.Numbers.ToString();
            lkcPaymentStatus.SetSelectedValue(value.OrderPaymentStatus);
            lkcProduct.SetSelectedValue(value.Product_ID);
            lkcStatus.SetSelectedValue(value.Status);
            pdrOrderPersianDate.DateRange = value.OrderPersianDate;
            nrAmount.Pattern = value.Amount;
            txtRefNum.Text = value.RefNum;
            txtUser.Text = value.UserName;
        }
    }
    
    public override OrdersAgentPattern DefaultPattern
    {
        get
        {
            OrdersAgentPattern p = new OrdersAgentPattern();
            if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.id))
            {
                if ((this.Page as BasePage).HasValidQueryString<long>(QueryStringKeys.id))
                {
                    var page = (this.Page as BaseManagementPage<OrdersAgentBusiness, Order, OrdersAgentPattern, pgcEntities>);
                    page.SelectedID = (this.Page as BasePage).GetQueryStringValue<long>(QueryStringKeys.id);
                    page.DetailControl.BeginMode(ManagementPageMode.Edit);
                    page.DetailControl.SetEntity(new OrdersBusiness().Retrieve(page.SelectedID), ManagementPageMode.Edit);
                    page.Mode = kFrameWork.Enums.ManagementPageMode.Edit;
                }
            }
            else
            {
                p.Status = OrderStatus.New;
                p.OrderPersianDate = pdrOrderPersianDate.DateRange;
                p.Amount = nrAmount.Pattern;
            }

            if (Session["OrderPattern"] != null)
            {
                try { p = (OrdersAgentPattern)Session["OrderPattern"]; }
                catch (Exception) { }
            }
            return p;
        }
    }

    public override OrdersAgentPattern SearchAllPattern
    {
        get
        {
            Session["OrderPattern"] = null;
            return base.SearchAllPattern;
        }
    }
}