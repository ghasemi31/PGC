<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/BaseEntityManagement.master" AutoEventWireup="true" CodeFile="OldDefault.aspx.cs" Inherits="Pages_Admin_SentMessage_Default" ValidateRequest="false" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="kfk" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="kfk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_navigation" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_search" Runat="Server">
    <kfk:Search ID="pnlSearch" runat="server" OnSearch="Search" OnSearchAll="SearchAll" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_list" Runat="Server">
    <kfk:List ID="pnlList" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_detail" Runat="Server">
</asp:Content>

--%>