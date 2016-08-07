<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridViewExport.ascx.cs" Inherits="UserControl_GridViewExport" %>
<div id="file-dropdown">
    <a href="javascript:void(0);" class="export" onclick="filedropdown_Click()"> دریافت فایل </a>
    <ul>
        <li><asp:LinkButton ID="lnkExportToExcel" runat="server" OnClick="lnkExportToExcel_Click" CssClass="excel">دریافت فایل Excel</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkExportToWord" runat="server" OnClick="lnkExportToWord_Click" CssClass="word">دریافت فایل Word</asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkExportToHtml" runat="server" OnClick="lnkExportToHtml_Click" CssClass="html">دریافت فایل Html</asp:LinkButton></li>
    </ul>
</div>


