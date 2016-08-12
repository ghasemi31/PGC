<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="OldNewsDetail.aspx.cs" Inherits="Pages_Guest_NewsDetail" %>

<%@ Register Src="~/UserControls/Project/Gallery.ascx" TagPrefix="kfk" TagName="Gallery" %>


<asp:Content ID="Content4" ContentPlaceHolderID="cphStylePege" runat="Server">
    
    <%this.Title = news.PageTitle; %>
    <meta name="description" content="<%=news.PageDescription %>" />
    <meta name="keywords" content="<%=news.PageKeywords %>" />
    <link href="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/jquery.fancybox.css?v=2.1.5" rel="stylesheet" />
    <link href="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-buttons.css?v=1.0.5" rel="stylesheet" />
    <link href="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-thumbs.css?v=1.0.7" rel="stylesheet" />
    <link href="/assets/css/gallery/gallery.min.css?v=2.2" rel="stylesheet" />
    <!-- BEGIN NEWS PAGE STYLE -->
    <link href="/assets/css/Portal-Page/portal-page.min.css?v=2.2" rel="stylesheet" />
    <!-- END NEWS PAGE STYLE -->
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphContentPage" runat="Server">
    <!-- BEGIN CONTENT -->
    <section id="main-section" class="main-body">
        <div class="container">
            <div class="row direction-rtl">
                <%--<div class="col-md-offset-1 col-md-10">--%>
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
                <%--</div>--%>
            </div>
            <%if (news.IsDisplayGallery==true)
              {%>
                  <div class="row">
                    <div class="col-lg-offset-4 col-md-offset-4 col-sm-offset-3 col-xs-offset-0 col-lg-3 col-md-3 col-lg-6 col-xs-12">
                        <kfk:Gallery runat="server" ID="gallery" />
                    </div>
                </div>
              <%} %>
        </div>
    </section>
    <!-- END CONTENT -->
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphScriptPage" runat="Server">
    <script src="/assets/global/plugins/fancyapps-fancyBox-18d1712/lib/jquery.mousewheel-3.0.6.pack.js"></script>
    <script src="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/jquery.fancybox.js?v=2.1.5"></script>
    <script src="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-buttons.js?v=1.0.5"></script>
    <script src="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-thumbs.js?v=1.0.7"></script>
    <script src="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-media.js?v=1.0.6"></script>

    <!-- BEGIN NEWS PAGE SCRIPT -->
    <script src="/assets/js/Portal-Page/portal-page.min.js?v=2.2"></script>
    <!-- BEGIN NEWS PAGE SCRIPT -->
</asp:Content>


