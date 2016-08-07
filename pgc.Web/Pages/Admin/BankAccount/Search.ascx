<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_BankAccount_Search" %>
<legend>جستجو</legend>
<table>    
    <tr>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" CssClass="large" /></td>
    </tr>
    <tr>
        <td class="caption">توضیحات</td>
        <td class="control"><kfk:NormalTextBox ID="txtDescription" runat="server" CssClass="large" /></td>
    </tr>
    <tr>
        <td class="caption">وضعیت حساب</td>
        <td class="control"><kfk:LookupCombo runat="server" ID="lkpStatus" AddDefaultItem="true" CssClass="large" EnumParameterType="pgc.Model.Enums.OfflineBankAccountStatus" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو حساب" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش تمامی حساب ها" CssClass="xlarge" OnClick="OnSearchAll" />
</div>
