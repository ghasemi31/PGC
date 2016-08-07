<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_SentEmailBlock_Detail"  %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">نوع ارسال</td>
        <td class="control"><asp:Label ID="lblEventType" runat="Server"/></td>
        
        <td class="caption" id="EventTitleCell" runat="server">عنوان رخداد</td>
        <td class="control"><asp:HyperLink ID="linkEvent" runat="server" ><asp:Label ID="lblEventTitle" runat="Server"></asp:Label></asp:HyperLink></td>
    </tr>    
    <tr>
        <td class="caption">عنوان</td>
        <td class="control" colspan="3"><asp:Label ID="lblSubject" runat="Server"/></td>
    </tr>
    <tr>
        <td class="caption">متن</td>
        <td class="control html" colspan="3"><panel class="html" ID="lblBody" runat="server"></panel></td>
    </tr>          
    
    <tr>
        <td class="caption">تاریخ ارسال</td>
        <td class="control"><asp:Label ID="lblDate" runat="server"></asp:Label></td>

        <td class="caption">وضعیت</td>
        <td class="control"><asp:Label ID="lblStatus" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">گیرندگان ایمیل</td>
        <td class="control"><asp:Label ID="lblRecipients" runat="server"></asp:Label></td>
        
        <td class="caption">تعداد دریافت کنندگان</td>
        <td class="control"><asp:Label ID="lblSize" runat="server"></asp:Label></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" Visible="false" ID="btnSave" Text="ذخیره راهنما" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="بازگشت &raquo;" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

