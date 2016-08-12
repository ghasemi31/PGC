<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="NewsDetail.aspx.cs" Inherits="Pages_Guest_NewsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%this.Title = news.PageTitle; %>
    <meta name="description" content="<%=news.PageDescription %>" />
    <meta name="keywords" content="<%=news.PageKeywords %>" />
    <link href="/assets/Guest/css/News.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" runat="Server">
    <section  class="main-body">
        <div class="container">
            <div class="row direction-rtl">
                <header>
                    <h1 class="news-title"><%=news.Title%></h1>
                    <span><%=kFrameWork.Util.DateUtil.GetPersianDateWithMonthName(news.NewsDate) %></span>
                    <ul class="list-inline">
                        <li><a href="javascript:;"><i class="fa fa-envelope"></i></a></li>
                        <li><a href="javascript:;"><i class="fa fa-fax"></i></a></li>
                        <li><a href="javascript:;"><i class="fa fa-file-pdf-o"></i></a></li>
                        <li><a href="javascript:;"><i class="fa fa-calendar"></i></a></li>
                    </ul>
                </header>
                <article>
                    <p>
                        ‎ <%=news.Body%>.
                    </p>
                </article>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" runat="Server">
</asp:Content>

