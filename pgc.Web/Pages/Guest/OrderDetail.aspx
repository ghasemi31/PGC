<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="Pages_Guest_OrderDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">
    <%this.Title = "جزئیات سفارش-"+order.ID; %>
    <link href="/assets/css/UserProfile/UserProfile.min.css?v=2.2" rel="stylesheet" />
    <link href="/assets/css/OrderDetail/tooltip.min.css?v=2.2" rel="stylesheet" />
    <style type="text/css">
        .user-info {
            margin-top: 0 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <section class="main-body content">
        <div class="container">
            <div class="row">
                <form runat="server">
                    <input type="hidden" id="SelectedOrder" runat="server" clientidmode="Static" />
                    <div class="col-lg-offset-1 col-md-offset-1 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-10 col-sm-12 col-xs-12">
                        <div class="order-code">
                            <ul class="list-inline">
                                <li>حساب کاربری من  <i class="fa fa-angle-left" aria-hidden="true"></i></li>
                                <li><a href="<%=GetRouteUrl("guest-orderlist",null) %>">سفارشات من</a><i class="fa fa-angle-left" aria-hidden="true"></i></li>
                                <li>سفارش <%=order.ID %></li>
                            </ul>
                        </div>
                        <div class="float-left" style="margin-top:3em;">
                            <a class="btn-profile" href="<%=GetRouteUrl("guest-orderlist",null) %>" style="margin-bottom: 5px;"><i class="fa fa-arrow-right" aria-hidden="true"></i>بازگشت</a>
                        </div>

                    </div>
                    <div class="col-lg-offset-1 col-md-offset-1 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-10 col-sm-12 col-xs-12">
                        <div class="user-info">
                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 order-list-item">
                                <span><span class="order-title">کد سفارش:  </span><%=order.ID %></span>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 order-list-item">
                                <span><span class="order-title">وضعیت سفارش:   </span><%= kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.OrderStatus)order.OrderStatus)%></span>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 order-list-item">
                                <span><span class="order-title">تاریخ سفارش:   </span><%=kFrameWork.Util.DateUtil.GetPersianDateWithTime(Convert.ToDateTime(order.OrderDate))%></span>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 order-list-item">
                                <span><span class="order-title">شعبه:   </span><%=order.BranchTitle%></span>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 order-list-item">
                                <span><span class="order-title">مبلغ قابل پرداخت:   </span><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(order.PayableAmount/10)%> تومان</span>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 order-list-item">
                                <span data-tooltip="<%= ((bool)order.IsPaid&&((pgc.Model.Enums.PaymentType)order.PaymentType==pgc.Model.Enums.PaymentType.Online))?"پرداخت شده":"پرداخت نشده" %>" data-tooltip-position="top"><span class="order-title">نحوه پرداخت:   </span><%= kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.PaymentType)order.PaymentType)%><i class="fa <%= ((bool)order.IsPaid&&((pgc.Model.Enums.PaymentType)order.PaymentType==pgc.Model.Enums.PaymentType.Online))?"fa-check paid":"fa-times unpaid" %>" aria-hidden="true"></i></span>
                            </div>
                            <%if ((bool)order.IsPaid && ((pgc.Model.Enums.PaymentType)order.PaymentType == pgc.Model.Enums.PaymentType.Online))
                              {%>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 order-list-item">
                                <span class="color-green"><span class="order-title">رسید دیجیتالی:   </span><%=order.OnlinePayments.FirstOrDefault(o=>o.TransactionState=="OK").RefNum %></span>
                            </div>
                            <%} %>
                            <%if (!(bool)order.IsPaid && ((pgc.Model.Enums.PaymentType)order.PaymentType == pgc.Model.Enums.PaymentType.Online))
                              {%>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 order-list-item">
                                <input type="hidden" order="<%=order.ID %>" ispaid="<%=order.IsPaid %>" />
                                <asp:Button ID="BtnPay" runat="server" Text="پرداخت آنلاین" OnClick="Btn_Pay_Click" OnClientClick="javascript:setID(this)" CssClass="btn-profile float-left" />
                            </div>
                            <%} %>
                            <div class="clear"></div>
                        </div>
                    </div>
                    <div class="col-lg-offset-1 col-md-offset-1 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-10 col-sm-12 col-xs-12">
                        <div class="user-info">
                            <header>
                                <i class="fa fa-user" aria-hidden="true"></i>
                                <span>تحویل گیرنده</span>
                                <hr />
                            </header>
                            <div class="row margin0" style="padding: 0 20px;">
                                <div class="row order-list-item">
                                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 order-list-item">
                                        <span><span class="order-title">نام: </span><%=order.Name %></span>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 order-list-item">
                                        <span><span class="order-title">تلفن:  </span><%=order.Tel%></span>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 order-list-item">
                                        <span><span class="order-title">تلفن همراه:  </span><%=order.Mobile%></span>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 order-list-item">
                                        <span><span class="order-title">آدرس:  </span><%=order.Address%></span>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 order-list-item">
                                        <span><span class="order-title">توضیحات:  </span><%=order.Comment%></span>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="col-lg-offset-1 col-md-offset-1 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-10 col-sm-12 col-xs-12">
                        <div class="user-info">
                            <header>
                                <i class="fa fa-list" aria-hidden="true"></i>
                                <span>ریز سفارش</span>
                                <hr />
                            </header>
                            <div class="row margin0" style="padding: 0 20px;">
                                <table class="table">
                                    <tr>
                                        <td>غذا</td>
                                        <td>تعداد</td>
                                        <td>قیمت واحد(تومان)</td>
                                        <td>قیمت کل (تومان)</td>
                                    </tr>
                                    <%foreach (var item in orderDetail)
                                      {%>
                                    <tr>
                                        <td><%=item.ProductTitle %></td>
                                        <td><%= item.Quantity%></td>
                                        <td><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(item.UnitPrice/10)%></td>
                                        <td><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(item.SumPrice/10)%></td>
                                    </tr>
                                    <%} %>
                                </table>
                            </div>
                        </div>

                    </div>
                </form>
            </div>
            <kfk:UserMessageViewer runat="server" ID="UserMessageViewer" />
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
    <script type="text/javascript">
        function setID(e) {
            $('#SelectedOrder').val($(e).prev().attr('order'));
        };
    </script>
</asp:Content>

