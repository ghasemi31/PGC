<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Agent_BranchPic_Search" %>
<legend>جستجو</legend>
<table>
    <tr>

    <tr>
        <td class="caption">نام فایل</td>
        <td class="control"><kfk:NormalTextBox ID="txtFileName" runat="server" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
