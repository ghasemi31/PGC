<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_LotteryDetail_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">قرعه کشی</td>
        <td class="control"><kfk:LookupCombo ID="lkpLottery" 
                                            runat="server" 
                                            BusinessTypeName="pgc.Business.Lookup.LotteryLookupBusiness"
                                            AddDefaultItem="true" /></td>
    </tr>
    <tr>
        <td class="caption">نام/ ایمیل / توضیحات</td>
        <td class="control"><kfk:NormalTextBox ID="txtName" runat="server" /></td>
    </tr>
    <tr>
        <td class="caption">کد</td>
        <td class="control"><kfk:NumericTextBox ID="txtCode" runat="server" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
