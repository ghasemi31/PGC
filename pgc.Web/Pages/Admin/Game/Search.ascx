<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_Game_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" /></td>
    </tr>
    <tr>
        <td class="caption">کلمه کلیدی url</td>
        <td class="control"><kfk:NormalTextBox ID="txtUrlKey" runat="server" /></td>
    </tr>
   <%-- <tr>
        <td class="caption">پذیرای سفارش آنلاین</td>
        <td class="control"><kfk:LookupCombo ID="lkpAllowOnlineOrder" runat="server" AddDefaultItem="true" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
    </tr>--%>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
