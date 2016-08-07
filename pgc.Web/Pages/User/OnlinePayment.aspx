<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="OnlinePayment.aspx.cs" Inherits="Pages_User_OnlinePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">
    <%this.Title = (!string.IsNullOrEmpty(kFrameWork.UI.UserSession.User.FullName)) ? "پرداخت های من-" + kFrameWork.UI.UserSession.User.FullName : ""; %>
    <link href="/assets/css/UserProfile/UserProfile.min.css?v=2.2" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <section class="main-body content">
        <div class="container">
            <div class="row">
                <div class="col-lg-offset-1 col-md-offset-1 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-10 col-sm-12 col-xs-12">
                    <div class="order-code">
                        <ul class="list-inline">
                            <li>حساب کاربری من<i class="fa fa-angle-left" aria-hidden="true"></i></li>
                            <li>پرداخت های من </li>
                        </ul>
                    </div>
                    <div class="user-info">
                        <header>
                            <i class="fa fa-list" aria-hidden="true"></i>
                            <span>پرداخت های من</span>
                            <hr />
                        </header>
                        <div class="row margin0" style="padding: 0 20px;">
                            <table class="table">
                                <tr>
                                    <td>کد سفارش</td>
                                    <td>مبلغ(تومان)</td>
                                    <td class="text-align-center">رسید دیجیتالی</td>
                                    <td>تاریخ تراکنش</td>
                                    <td>وضعیت تراکنش</td>
                                </tr>
                                <asp:ObjectDataSource ID="odsOnlinePayment"
                                    runat="server"
                                    EnablePaging="True"
                                    SelectCountMethod="OnlinePayment_Count"
                                    SelectMethod="OnlinePayment_List"
                                    TypeName="pgc.Business.General.OrderBusiness"
                                    EnableViewState="false"></asp:ObjectDataSource>
                                <asp:ListView ID="lsvOnlinePayment" runat="server" DataSourceID="odsOnlinePayment" EnableViewState="false">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("Order_ID") %></td>
                                            <td><%#kFrameWork.Util.UIUtil.GetCommaSeparatedOf((Convert.ToInt64(Eval("Amount"))/10).ToString()) %></td>
                                            <td class="text-align-center color-green"><i class="<%#(string.IsNullOrEmpty(Eval("RefNum").ToString())?"fa fa-times unpaid":"") %>" aria-hidden="true"></i><%#(!string.IsNullOrEmpty(Eval("RefNum").ToString())?Eval("RefNum").ToString():"") %></td>
                                            <td><%#kFrameWork.Util.DateUtil.GetPersianDateWithTime(Convert.ToDateTime(Eval("Date"))) %></td>
                                            <td><%#test((long)Eval("ResultTransaction"),Eval("TransactionState").ToString()) %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Pager -->
            <%if (dprOnlinepayment.TotalRowCount > dprOnlinepayment.MaximumRows)
              {%>
            <div class="pagination">
                <span>صفحات دیگر: </span>
                <asp:DataPager ID="dprOnlinepayment" runat="server" PagedControlID="lsvOnlinePayment" PageSize="10" QueryStringField="page">
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
            <kfk:UserMessageViewer runat="server" ID="UserMessageViewer" />
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
</asp:Content>

