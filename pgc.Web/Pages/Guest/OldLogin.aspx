<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="OldLogin.aspx.cs" Inherits="Pages_Guest_Login" %>

<%@ Register Src="~/UserControls/Project/LoginControl.ascx" TagName="LoginControl" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Common/UserMessageViewer.ascx" TagName="UserMessageViewer" TagPrefix="uc2" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="kFrameWork.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%this.Title = OptionBusiness.GetText(OptionKey.Login_Title); %>
    <meta name="description" content="<%=OptionBusiness.GetLargeText(OptionKey.Login_Description) %>" />
    <meta name="keywords" content="<%=OptionBusiness.GetLargeText(OptionKey.Login_Keywords) %>" />
    <!-- BEGIN LOGIN PAGE STYLE -->
    <link href="/assets/css/Login/login.min.css?v=2.2" rel="stylesheet" />
    <!-- END LOGIN PAGE STYLE -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" runat="Server">
    <!-- BEGIN CONTENT -->
    <section class="main-body">
        <div class="container-fluid">
            <div id="login-section" class="row">
                <div id="login" class="col-lg-offset-4 col-lg-4 col-md-offset-4 col-md-4 col-sm-offset-3 col-sm-6 col-xs-offset-0 col-xs-12">
                    <header>
                        <h1>ورود به حساب کاربری</h1>
                        <hr />
                    </header>
                    <form id="form1" runat="server">
                        <div id="login-form">
                            <uc1:LoginControl runat="server" ID="loc" />
                        </div>
                        <uc2:UserMessageViewer runat="server" ID="umv" />
                        <div class="clear"></div>
                    </form>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </section>
    <!-- END CONTENT -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" runat="Server">
    <!-- BEGIN LOGIN PAGE SCRIPT -->
    <script src="/assets/js/Login/login.min.js?v=2.2"></script>
    <!-- BEGIN LOGIN PAGE SCRIPT -->
</asp:Content>

