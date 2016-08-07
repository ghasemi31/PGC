<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_Lottery_Search" %>
<legend>جستجو</legend>
<table>

    <tr>
        <td class="caption">تاریخ ثبت</td>
        <td class="control"><kfk:PersianDateRange runat="server" ID="pdrRegPersianDate"/></td>

    </tr>
    
    <tr>
        <td class="caption">عنوان / توضیحات</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtDesc"/></td>

    </tr>
    <tr>
        <td class="caption">وضعیت</td>
        <td class="control"><kfk:LookupCombo ID="lkcStatus" 
                                    runat="server" 
                                    EnumParameterType="pgc.Model.Enums.LotteryStatus"
                                    AddDefaultItem="true"/></td>

    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
