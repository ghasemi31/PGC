<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_SentEmailBlock_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">نوع رخداد</td>
        <td class="control"><kfk:LookupCombo ID="lkpEventType" AddDefaultItem="true" runat="server" EnumParameterType="pgc.Model.Enums.EventType" /></td>

        <td class="caption">عنوان رخداد</td>
        <td class="control"><kfk:NormalTextBox ID="txtEventTitle" runat="server" /></td>
    </tr>
    <tr>
        <td class="caption">پیامک دریافت کننده</td>
        <td class="control"><kfk:NormalTextBox ID="txtRecipients" runat="server" /></td>

        <td class="caption">عنوان پیامک ارسالی</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" /></td>
    </tr>
    <tr>
        <td class="caption">تاریخ ارسال</td>
        <td class="control"><kfk:PersianDateRange ID="pdrDate" runat="server" /></td>
    
        <td class="caption">تعداد پیامک ها</td>
        <td class="control"><kfk:NumericRange ID="nrSentEmail" runat="server" /></td>
    </tr>
    <tr>
        <td class="caption">وضعیت ارسال</td>
        <td class="control"><kfk:LookupCombo ID="lkpStatus" runat="server" AddDefaultItem="true" EnumParameterType="pgc.Model.Enums.SentEmailBlockStatus" /></td>

        <td></td>
        <td></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجوي بسته ارسالی" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش تمامی بسته ها" CssClass="" OnClick="OnSearchAll" />
</div>
