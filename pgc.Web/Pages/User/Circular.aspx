<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="Circular.aspx.cs" Inherits="Pages_User_Circular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">
    <title>بخشنامه های مستردیزی</title>
    <link href="/assets/css/UserProfile/UserProfile.min.css?v=2.2" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <section class="main-body content">
        <div class="container">
            <div class="row">
                <div class="col-lg-offset-1 col-md-offset-0 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-12 col-sm-12 col-xs-12">
                    <div class="order-code">
                            <ul class="list-inline">
                                <li>حساب کاربری من  <i class="fa fa-angle-left" aria-hidden="true"></i></li>
                                <li>بخشنامه ها</li>
                            </ul>
                        </div>     
                    <div class="user-info">
                        <%if (business.Circular_Count() > 0)
                          {%>
                        <header>
                            <i class="fa fa-list-alt" aria-hidden="true"></i>
                            <span>بخشنامه های مستردیزی</span>
                            <hr />
                        </header>
                        <div class="row margin0" style="padding: 0 20px;">
                            <table class="table">
                                <tr class="table-header">
                                    <td>عنوان</td>
                                    <td>تاریخ</td>
                                    <td></td>
                                </tr>
                                <asp:ObjectDataSource ID="odsOrder"
                                    runat="server"
                                    EnablePaging="True"
                                    SelectCountMethod="Circular_Count"
                                    SelectMethod="Circular_List"
                                    TypeName="pgc.Business.General.CircularBusiness"
                                    EnableViewState="false"></asp:ObjectDataSource>
                                <asp:ListView ID="lsvOrder" runat="server" DataSourceID="odsOrder" EnableViewState="false">
                                    <ItemTemplate>
                                        <tr class="table-row">
                                            <td><%#Eval("Title") %></td>
                                            <td><%#kFrameWork.Util.DateUtil.GetPersianDateWithTime(Convert.ToDateTime(Eval("Date"))) %></td>
                                            <td>
                                                <a class="btn-table" href="<%#GetRouteUrl("user-circulardetail", new {id=Eval("ID") })%>">نمایش </a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </table>
                        </div>
                        <%}
                          else
                          {%>
                        <div style="text-align: center; line-height: 85px;">
                            بخشنامه ای برای نمایش وجود ندارد.
                        </div>
                        <%} %>
                    </div>
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
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
</asp:Content>

