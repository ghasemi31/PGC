using System;
using kFrameWork.UI;
using pgc.Model;
using kFrameWork.Enums;
using pgc.Model.Enums;
using pgc.Business;
using pgc.Model.Patterns;
using kFrameWork.Util;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Order_Detail : BaseDetailControl<Order>
{
    public OrdersBusiness business = new OrdersBusiness();
    public Order order = new Order();

    public override Order GetEntity(Order Data, ManagementPageMode Mode)
    {
        if (Data == null)
            Data = new Order();
        Data.OrderStatus = lkcOrderStatus.GetSelectedValue<int>();

        if (lkcBranch.GetSelectedValue<long>() == -1)
            Data.Branch_ID = null;
        else
        {
            Data.Branch_ID = lkcBranch.GetSelectedValue<long>();
            Data.BranchTitle = new BranchBusiness().Retrieve(lkcBranch.GetSelectedValue<long>()).Title;
        }


        return Data;
    }

    public override void SetEntity(Order Data, ManagementPageMode Mode)
    {
        order = Data;
        lblOrderID.Text = Data.ID.ToString();
        lblAddress.Text = Data.Address;
        lblComment.Text = Data.Comment;
        lblFullName.Text = Data.User.FullName;
        lblOrderPersianDate.Text = DateUtil.GetPersianDateWithTime(Data.OrderDate).ToString();
        lblPayableAmount.Text = UIUtil.GetCommaSeparatedOf(Data.PayableAmount) + " ریال";
        //lblPaymentType.Text = EnumUtil.GetEnumElementPersianTitle((PaymentType)Data.PaymentType);
        lblOrderPaymentStatus.Text = EnumUtil.GetEnumElementPersianTitle(business.GetPaymentStatus(Data.ID));
        lblTel.Text = Data.Tel;
        lblUserTel.Text = string.IsNullOrEmpty(Data.User.Mobile) ? Data.User.Tel : Data.User.Mobile;
        lblTotalAmount.Text = UIUtil.GetCommaSeparatedOf(Data.TotalAmount) + " ریال";
        lblEmail.Text = Data.User.Email;
        if (Data.Branch_ID == null)
            lkcBranch.SetSelectedValue(-1);
        else
            lkcBranch.SetSelectedValue(Data.Branch_ID);

        lkcOrderStatus.SetSelectedValue(Data.OrderStatus);

        lsvOrderDetail.DataSource = business.OrderDetail_List(Data.ID);
        lsvOrderDetail.DataBind();

    }

    public override void EndMode(ManagementPageMode Mode)
    {
        (this.Page as BaseManagementPage<OrdersBusiness, Order, OrdersPattern, pgcEntities>).ListControl.Grid.DataBind();
        base.EndMode(Mode);
    }

    protected void btnOnline_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("admin-onlinepayment", null) + "?fid=" + (this.Page as BaseManagementPage<OrdersBusiness, Order, OrdersPattern, pgcEntities>).SelectedID.ToString());
    }
}