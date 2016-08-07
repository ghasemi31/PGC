<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="NewsList.aspx.cs" Inherits="Pages_Guest_NewsList" %>

<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Business" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">

    <%this.Title = OptionBusiness.GetText(pgc.Model.Enums.OptionKey.NewsListTitle); %>
    <meta name="description" content="<%=OptionBusiness.GetLargeText(OptionKey.Description_NewsList) %>" />
    <meta name="keywords" content="<%=OptionBusiness.GetLargeText(OptionKey.Keywords_NewsList) %>" />
    <!-- BEGIN AECHIVE PAGE STYLE -->
    <link href="/assets/css/Archive/archive.min.css?v=2.2" rel="stylesheet" />
    <!-- END AECHIVE PAGE STYLE -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <!-- BEGIN CONTENT -->
    <section id="main-content" class="main-body">
        <div class="container">
            <div class="col-lg-offset-1 col-lg-10 col-md-offset-1 col-md-10 col-sm-offset-0 col-sm-12 col-xs-offset-0 col-xs-12">
                <asp:ObjectDataSource ID="odsNews"
                    runat="server"
                    EnablePaging="True"
                    SelectCountMethod="News_Count"
                    SelectMethod="News_List"
                    TypeName="pgc.Business.General.NewsBusiness"
                    EnableViewState="false"></asp:ObjectDataSource>

                <!-- آیتم نمونه در لیست -->
                <asp:ListView ID="lsvNews" runat="server" DataSourceID="odsNews" EnableViewState="false">
                    <ItemTemplate>
                        <div class="row archive-content bg-color">
                            <div class="col-lg-3 col-md-3 col-sm-4 col-xs-12">
                                <a href="<%#GetRouteUrl("guest-newsdetail",new { id = Eval("ID"),title=Eval("Title").ToString().Replace(" ","-") })%>">
                                    <%#(Eval("NewsPicPath") == "") ? "" : "<img class="+"width100"+" alt="+Eval("Title")+" src=\"" + ResolveClientUrl(Eval("NewsPicPath").ToString()) + "?height=410&width=436&mode=cropandscale\" alt=\"" + Eval("Title") + "\" />"%>
                                </a>
                            </div>
                            <div class="col-lg-9 col-md-9 col-sm-8 col-xs-12">
                                <header class="row">
                                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-12">
                                        <a href="<%#GetRouteUrl("guest-newsdetail",new { id = Eval("ID"),title=Eval("Title").ToString().Replace(" ","-") })%>">
                                            <h1><%#Eval("Title")%> </h1>
                                        </a>
                                    </div>
                                    <div class="archive-date col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                        <span><%#kFrameWork.Util.DateUtil.GetPersianDateWithMonthName((DateTime)Eval("NewsDate")) %></span>
                                    </div>
                                </header>
                                <article>
                                    <p>
                                        <%#Eval("Summary") %>.
                                    </p>
                                </article>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>

                <!-- Pager -->
                <%if (dprNews.TotalRowCount > dprNews.MaximumRows)
                  {%>
                <div class="fontClass pagination display-center">
                    <%--   <span class="pages-label">صفحات دیگر: </span>--%>
                    <asp:DataPager ID="dprNews" runat="server" PagedControlID="lsvNews" PageSize="10" QueryStringField="page">
                        <Fields>
                            <asp:NextPreviousPagerField PreviousPageText="صفحه قبلی" ButtonCssClass="button prev" ShowNextPageButton="false" />
                            <asp:TemplatePagerField>
                                <PagerTemplate><span class="pages"></PagerTemplate>
                            </asp:TemplatePagerField>
                            <asp:NumericPagerField ButtonCount="6" NumericButtonCssClass="page" NextPreviousButtonCssClass="page" CurrentPageLabelCssClass="current" />
                            <asp:TemplatePagerField>
                                <PagerTemplate></span></PagerTemplate>
                            </asp:TemplatePagerField>
                            <asp:NextPreviousPagerField NextPageText="صفحه بعدی" ButtonCssClass="button next" ShowPreviousPageButton="false" />
                        </Fields>
                    </asp:DataPager>
                </div>
                <%} %>
            </div>
        </div>
    </section>
    <!-- END CONTENT -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
</asp:Content>
