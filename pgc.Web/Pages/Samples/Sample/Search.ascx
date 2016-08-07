<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_Sample_Search" %>
<legend>جستجو</legend>
<table>
    <%--<tr>
        <td class="caption">تاریخ شروع</td>
        <td class="control"><kfk:PersianDateRange ID="drgStartDate" runat="server" /></td>

        <td class="caption">تاریخ پایان</td>
        <td class="control"><kfk:PersianDateRange ID="drgEndDate" runat="server" /></td>
    </tr>--%>
    <tr>
        <td class="caption">عنوان و توضیحات</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
