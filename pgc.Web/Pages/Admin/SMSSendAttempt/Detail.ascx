<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_SMSSendAttempt_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">نوع ارسال</td>
        <td class="control"><asp:Label ID="lblEventType" runat="Server"/></td>
        
        <td class="caption" id="EventTitleCell" runat="server">عنوان رخداد</td>
        <td class="control">
            <asp:HyperLink ID="linkEvent" runat="server" >
                <asp:Label ID="lblEventTitle" runat="Server"></asp:Label>
            </asp:HyperLink>
        </td>
    </tr> 
    <tr>
        <td class="caption">تاریخ ارسال</td>
        <td class="control"><asp:Label ID="lblDate" runat="server"></asp:Label></td>

        <td class="caption">وضعیت</td>
        <td class="control"><asp:Label ID="lblStatus" runat="server"></asp:Label></td>
    </tr>       
    <tr>
        <td class="caption">متن</td>
        <td class="control html" colspan="3" ><panel class="html" ID="lblBody" runat="server" ></panel></td>
    </tr>
    <tr>
        <td class="caption">تعداد کل پیامک ها</td>
        <td class="control"><asp:Label ID="lblTotalMail" runat="Server"/></td>
    
        <td class="caption">پیامک های ارسالی</td>
        <td class="control"><asp:Label ID="lblSentEmail" runat="Server"/></td>
    </tr> 
    <tr>
        <td class="caption">پیامک های خطا دار</td>
        <td class="control"><asp:Label ID="lblfailedEmail" runat="Server"/></td>
    
        <td class="caption">پیامک های نا مشخص</td>
        <td class="control"><asp:Label ID="lblUnknown" runat="Server"/></td>
    </tr> 
    <tr>
        <td class="caption">حداکثر سایز بسته ها</td>
        <td class="control"><asp:Label ID="lblBlockSize" runat="Server"/></td>

        <td class="caption">شماره اختصاصی ارسال کننده</td>
        <td class="control"><asp:Label ID="lblPrivateNo" runat="Server"/></td>
    </tr> 
    <tr>
        <td class="caption">گیرندگان</td>
        <td class="control"><asp:Label ID="lblRecipient" runat="server"></asp:Label></td>

        <td></td>
        <td></td>
    </tr>    
</table>
<div class="commands">
    <%--<asp:Button runat="server" ID="btnSave" Text="ثبت" CssClass="large" OnClick="OnSave" CausesValidation="true" />--%>
    <asp:Button runat="server" ID="btnSMSPacket" Text="ریز پیامک های ارسالی" CssClass="xlarge" OnClick="btnSMSPacket_Click" CausesValidation="false" />    
    <asp:Button runat="server" ID="btnCancel" Text="بازگشت &raquo;" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

