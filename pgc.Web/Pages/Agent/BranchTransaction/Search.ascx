<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Agent_BranchTransaction_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">عنوان (کد فاکتور)</td>
        <td class="control">
            <kfk:NormalTextBox Mode="Numeric" ID="txtLogTypeID" runat="server" />
            <br />
            <span>(درخواست،کسری،مرجوعی)</span>
        </td>  
    </tr>
    <tr>
        <td class="caption">نوع تراکنش</td>
        <td class="control"><kfk:LookupCombo ID="lkpTranType" 
                                            runat="server" 
                                            EnumParameterType="pgc.Model.Enums.BranchTransactionTypeForSearch"
                                            AddDefaultItem="true" /></td>

        <td class="caption">تاریخ</td>
        <td class="control"><kfk:PersianDateRange ID="pdrRegDate" runat="server" /></td>
    </tr>
    <tr>
        <td class="caption">مبلغ دریافتی</td>
        <td class="control"><kfk:NumericRange ID="nmrBranchDebt" runat="server" /></td>

        <td class="caption">مبلغ پرداختی</td>
        <td class="control"><kfk:NumericRange ID="nmrBranchCredit" runat="server" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش تمامی تراکنش ها" CssClass="" OnClick="OnSearchAll" />
</div>
