<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/BaseEntityManagement.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Admin_Sample_Default" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="kfk" %>
<%@ Register Src="Detail.ascx" TagName="Detail" TagPrefix="kfk" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="kfk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/UserControls/Common/FileUploader/FileUploader.css") %>" type="text/css"/>--%>
    <%--<script src="<%=ResolveClientUrl("~/Scripts/UserControls/Common/FileUploader/FileUploader.js")%>" language="javascript" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_navigation" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_search" Runat="Server">
    <kfk:Search ID="pnlSearch" runat="server" OnSearch="Search" OnSearchAll="SearchAll" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_list" Runat="Server">
    <kfk:List ID="pnlList" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_detail" Runat="Server">
    <kfk:Detail ID="pnlDetail" runat="server" OnSave="Save" OnCancel="Cancel" />
</asp:Content>

