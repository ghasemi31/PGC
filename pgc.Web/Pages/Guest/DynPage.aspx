<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="DynPage.aspx.cs" Inherits="Pages_Guest_DynPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">
    <%this.Title = portal.Heading; %>
    <!-- BEGIN PORTAL PAGE STYLE -->
    <link href="/assets/css/Portal-Page/portal-page.min.css?v=2.2" rel="stylesheet" />
    <!-- END PORTAL PAGE STYLE -->
    <meta name="Keywords" content="<%#portal.MetaKeyWords%>" />
    <meta name="Description" content="<%#portal.MetaDescription%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <!-- BEGIN CONTENT -->
    <section id="main-section" class="main-body">

        <%=portal.Body%>

        <div class="clear"></div>
    </section>
    <!-- END CONTENT -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
    <%-- BEGIN GLOBAL SCRIPT --%>
    <script src="/assets/global/plugins/bootstrap/js/bootstrap.v3.3.2.min.js"></script>
    <%-- END  GLOBAL SCRIPT  --%>
    <!-- BEGIN PORTAL PAGE SCRIPT -->
    <script src="/assets/js/Portal-Page/portal-page.min.js?v=2.2"></script>
    <!-- BEGIN PORTAL PAGE SCRIPT -->
</asp:Content>

