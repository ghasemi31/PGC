<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_SentMessageView_Detail"  %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">نوع ارسال</td>
        <td class="control"><panel ID="lblEventType" runat="Server"/></td>
        
        <td class="caption" id="EventTitleCell" runat="server">عنوان رخداد</td>
        <td class="control"><asp:HyperLink ID="linkEvent" runat="server" ><panel ID="lblEventTitle" runat="Server"></panel></asp:HyperLink></td>
    </tr>        
    <tr>
        <td class="caption">متن</td>
        <td class="control html" colspan="3"><panel class="html" ID="lblBody" runat="server"></panel></td>
    </tr>          
    <tr>
        <td class="caption">تاریخ ارسال</td>
        <td class="control"><panel ID="lblDate" runat="server"></panel></td>

        <td class="caption">وضعیت</td>
        <td class="control"><panel ID="lblStatus" runat="server"></panel></td>
    </tr>
    <tr>
        <td class="caption">گیرنده پیامک</td>
        <td class="control"><panel ID="lblRecipients" runat="server"></panel></td>
        
        <td class="caption">تعداد صفحه پیامک</td>
        <td class="control"><panel ID="lblSize" runat="server"></panel></td>
    </tr>
    <tr>
        <td class="caption">شماره ارسال کننده</td>
        <td class="control"><panel ID="lblPrivateNo" runat="server"></panel></td>

        <td class="caption">نوع پیامک</td>
        <td class="control"><panel ID="lblMessageType" runat="server"></panel></td>
    </tr>
    <tr>
        <td class="caption" ID="MessageGetWayIDCell" runat="server">کد درگاه پیامک</td>
        <td class="control"><panel ID="lblMessageGetWayID" runat="server"></panel></td>
        
        <td class="caption" ID="FaultTypeCell" runat="server">کد خطا</td>
        <td class="control"><panel ID="lblFault" runat="server"></panel></td>
    </tr>    
</table>
<div class="commands">
    <asp:Button runat="server" Visible="false" ID="btnSave" Text="ذخیره راهنما" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="بازگشت &raquo;" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

