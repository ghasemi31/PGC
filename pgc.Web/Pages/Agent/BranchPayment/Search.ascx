<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_BranchPayment_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" /></td>
        
        <td class="caption">نوع پرداخت</td>
        <td class="control"><kfk:LookupCombo ID="lkpPaymentType" runat="server" EnumParameterType="pgc.Model.Enums.BranchPaymentTypeForSearch" AddDefaultItem="true" /></td>
    </tr>
    <tr>
        <td class="caption">مبلغ</td>
        <td class="control"><kfk:NumericRange ID="nmrPrice" runat="server" /></td>
    
        <td class="caption">تاریخ پرداخت</td>
        <td class="control"><kfk:PersianDateRange ID="pdrDate" runat="server" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش تمامی پرداختی ها" CssClass="" OnClick="OnSearchAll" />
</div>
