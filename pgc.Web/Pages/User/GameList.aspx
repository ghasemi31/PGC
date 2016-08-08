<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="GameList.aspx.cs" Inherits="Pages_User_GameList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <%this.Title = (!string.IsNullOrEmpty(kFrameWork.UI.UserSession.User.FullName)) ? "لیست بازیهای من-" + kFrameWork.UI.UserSession.User.FullName : ""; %>
    <link href="/assets/User/UserProfile.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" Runat="Server">
    <section class="main-body">
        <div class="container">
            <div class="row">
                <form runat="server">
                    <input type="hidden" id="SelectedOrder" runat="server" clientidmode="Static" />
                    <div class="col-lg-offset-1 col-md-offset-1 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-10 col-sm-12 col-xs-12">
                        <div class="order-code">
                            <ul class="list-inline">
                                <li>حساب کاربری من  <i class="fa fa-angle-left" aria-hidden="true"></i></li>
                                <li>بازیهای من</li>
                            </ul>
                        </div>
                        <div class="user-info">
                            <header>
                                <i class="fa fa-list" aria-hidden="true"></i>
                                <span>بازیهای من</span>
                                <hr />
                            </header>
                            <div class="row margin0" style="padding: 0 20px;">
                                <table class="table">
                                    <tr class="order-title table-header">
                                        <td>کد سفارش</td>
                                        <td class="text-align-center">تاریخ</td>
                                        <td>وضعیت</td>
                                        <td class="text-align-center">مبلغ قابل پرداخت(تومان)</td>
                                        <td>نحوه پرداخت</td>
                                        <td class="text-align-center">وضعیت پرداخت</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <asp:ObjectDataSource ID="odsOrder"
                                        runat="server"
                                        EnablePaging="True"
                                        SelectCountMethod="Order_Count"
                                        SelectMethod="Order_List"
                                        TypeName="pgc.Business.General.OrderBusiness"
                                        EnableViewState="false"></asp:ObjectDataSource>
                                    <asp:ListView ID="lsvOrder" runat="server" DataSourceID="odsOrder" EnableViewState="false">
                                        <ItemTemplate>
                                            <tr class="order-tb-row">
                                                <td><%#Eval("ID") %></td>
                                                <td><%#kFrameWork.Util.DateUtil.GetPersianDateWithTime(Convert.ToDateTime(Eval("OrderDate")))%></td>
                                                <td><%# kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.OrderStatus)Eval("OrderStatus"))%></td>
                                                <td class="text-align-center"><%#kFrameWork.Util.UIUtil.GetCommaSeparatedOf((Convert.ToInt64(Eval("PayableAmount"))/10).ToString())%></td>
                                                <td><%# kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.PaymentType)Eval("PaymentType"))%></td>
                                                <td class="text-align-center" data-tooltip="<%# ((bool)Eval("IsPaid")&&((pgc.Model.Enums.PaymentType)Eval("PaymentType")==pgc.Model.Enums.PaymentType.Online))?"پرداخت شده":"پرداخت نشده" %>" data-tooltip-position="top" style="display: block"><i class="fa <%# ((bool)Eval("IsPaid")&&((pgc.Model.Enums.PaymentType)Eval("PaymentType")==pgc.Model.Enums.PaymentType.Online))?"fa-check paid":"fa-times unpaid" %>" aria-hidden="true"></i></td>
                                                <td>
                                                    <input type="hidden" order="<%# Eval("ID") %>" ispaid="<%# Eval("IsPaid") %>" />
                                                    <asp:Button ID="BtnPay" runat="server" Text="پرداخت آنلاین" OnClick="Btn_Pay_Click" OnClientClick="javascript:setID(this)" CssClass="btn-table" />
                                                </td>
                                                <td class="tbl-row"><a href="<%#GetRouteUrl("guest-orderdetail",new { id = Eval("ID") })%>" class="btn-table">جزئیات</a></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </table>
                            </div>
                        </div>
                    </div>
                </form>
                <!-- Pager -->
                <%if (dprOrder.TotalRowCount > dprOrder.MaximumRows)
                  {%>
                <div class="pagination">
                    <span>صفحات دیگر: </span>
                    <asp:DataPager ID="dprOrder" runat="server" PagedControlID="lsvOrder" PageSize="10" QueryStringField="page">
                        <Fields>
                            <asp:NextPreviousPagerField PreviousPageText="قبلی" ButtonCssClass="button prev" ShowNextPageButton="false" />
                            <asp:TemplatePagerField>
                                <PagerTemplate><span class="pages"></PagerTemplate>
                            </asp:TemplatePagerField>
                            <asp:NumericPagerField ButtonCount="6" NumericButtonCssClass="page" NextPreviousButtonCssClass="page" CurrentPageLabelCssClass="current" />
                            <asp:TemplatePagerField>
                                <PagerTemplate></span></PagerTemplate>
                            </asp:TemplatePagerField>
                            <asp:NextPreviousPagerField NextPageText="بعدی" ButtonCssClass="button next" ShowPreviousPageButton="false" />
                        </Fields>
                    </asp:DataPager>
                </div>
                <%} %>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" Runat="Server">
    <script language="javascript" type="text/javascript">
        function setID(e) {
            $('#SelectedOrder').val($(e).prev().attr('order'));
        };

        $(document).ready(function () {
            $('input[IsPaid=True]').each(function () {
                $(this).next().hide();
                //$(this).next().next().hide();
            });
        });
    </script>
</asp:Content>

