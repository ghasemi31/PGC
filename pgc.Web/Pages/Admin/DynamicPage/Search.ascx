<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_DynPage_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">عنوان صفحه</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" /></td>
    </tr>
    <tr>
        <td class="caption">متا</td>
        <td class="control"><kfk:NormalTextBox ID="txtMeta" runat="server" /></td>
    </tr>
    <tr>
        <td class="caption">محتوا</td>
        <td class="control"><kfk:NormalTextBox ID="txtContent" runat="server" /></td>
    </tr>
    <tr>
        <td class="caption">کلمه کلیدی url</td>
        <td class="control"><kfk:NormalTextBox ID="txtUrlKey" runat="server" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
