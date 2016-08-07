<%--<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OldSearch.ascx.cs" Inherits="Pages_Admin_SentMessage_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">وضعیت ارسال</td>
        <td class="control"><kfk:LookupCombo ID="lkcSendStatus" 
                                runat="server" 
                                EnumParameterType="pgc.Model.Enums.SendStatus" 
                                AddDefaultItem="true" /></td>
    </tr>
        <tr>
        <td class="caption">نوع پیامک</td>
        <td class="control"><kfk:LookupCombo ID="lkcMessageType" 
                                runat="server" 
                                EnumParameterType="pgc.Model.Enums.MessageType" 
                                AddDefaultItem="true" /></td>
    </tr>
        <tr>
        <td class="caption">گیرنده</td>
        <td class="control"><kfk:NormalTextBox ID="txtRecipient" runat="server" /></td>
    </tr>
        <tr>
        <td class="caption">تاریخ ارسال</td>
        <td class="control"><kfk:PersianDateRange ID="pdrSendPersianDate" runat="server" /></td>
    </tr>
        <tr>
        <td class="caption">شرح پیام</td>
        <td class="control"><kfk:NormalTextBox ID="txtBody" runat="server" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
--%>