<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="Laws.aspx.cs" Inherits="Pages_Guest_Laws" %>

<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="kFrameWork.Business" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <%this.Title = OptionBusiness.GetText(OptionKey.Laws_Title); %>
    <meta name="description" content="<%=OptionBusiness.GetLargeText(OptionKey.Laws_Description) %>" />
    <meta name="keywords" content="<%=OptionBusiness.GetLargeText(OptionKey.Laws_Keywords) %>" />
    <link href="/assets/Guest/css/AboutUs.css?v=2" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" runat="Server">
    <section id="page">
        <header id="about-header">
        </header>
        <!-- main content -->
        <section id="main-content">
            <div class="container">
                <div id="about-content" class="row">
                   <header>
                       <h1>قوانین و مقررات بازی <%=game.Title %></h1>
                   </header>
                    <div>
                        <%=game.LawsGame %>
                    </div>
                </div>
            </div>
        </section>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" runat="Server">
</asp:Content>
