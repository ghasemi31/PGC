<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_OnlinePaymentList_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">مبلغ</td>
        <td class="control">
            <kfk:NumericRange ID="nmrAMount" runat="server" />
        </td>        
    </tr>

    <tr>
        <td class="caption">تاریخ واریز</td>
        <td class="control">
            <kfk:PersianDateRange ID="pdrDate" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="caption">پرداخت کننده</td>
        <td class="control">
            <kfk:LookupCombo ID="lkpStatus" runat="server" CssClass="state" AddDefaultItem="true" EnumParameterType="pgc.Model.Enums.OnlinePaymentStatus" />
        </td>

        <%--<td class="caption">شعبه</td>
        <td class="control">
            <kfk:LookupCombo ID="lkpBranch" runat="server" BusinessTypeName="pgc.Business.Lookup.BranchLookupBusiness" AddDefaultItem="true" />
        </td>--%>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش تمامی تراکنش ها" CssClass="" OnClick="OnSearchAll" />
</div>
