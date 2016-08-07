<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_EmailSendAttempt_Detail" %>
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
        <td class="caption">عنوان ایمیل</td>
        <td class="control" colspan="3"><asp:Label ID="lblSubject" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">متن</td>
        <td class="control html" colspan="3"><panel class="html" ID="lblBody" runat="server"></panel></td>
    </tr>
    <tr>
        <td class="caption">تعداد کل ایمیل ها</td>
        <td class="control"><asp:Label ID="lblTotalMail" runat="Server"/></td>
        
        <td class="caption">تعداد کل بسته ها</td>
        <td class="control"><asp:Label ID="lblTotalBlock" runat="Server"/></td>
    </tr> 
    <tr>
        <td class="caption">ایمیل های ارسالی</td>
        <td class="control"><asp:Label ID="lblSentEmail" runat="Server"/></td>
        
        <td class="caption">بسته های ارسالی</td>
        <td class="control"><asp:Label ID="lblSentBlock" runat="Server"/></td>
    </tr> 
    <tr>
        <td class="caption">ایمیل های خطا دار</td>
        <td class="control"><asp:Label ID="lblfailedEmail" runat="Server"/></td>
        
        <td class="caption">بسته های خطا دار</td>
        <td class="control"><asp:Label ID="lblFailedBlock" runat="Server"/></td>
    </tr> 
    <tr>
        <td class="caption">آدرس های نا معتبر</td>
        <td class="control"><asp:Label ID="lblInvalidEmail" runat="Server"/></td>
        
        <td class="caption">حداکثر سایز بسته ها</td>
        <td class="control"><asp:Label ID="lblBlockSize" runat="Server"/></td>
    </tr> 
    <tr>
        <td class="caption">گیرندگان</td>
        <td class="control"><asp:Label ID="lblRecipient" runat="server"></asp:Label></td>

        <td></td>
        <td></td>
    </tr>
    <tr runat="server" id="Tr1">                    
        <td class="caption">الصاق فایل ضمیمه</td>                    
        <td class="control"><kfk:FileUploader ID="fup1" runat="server" SaveFolder="~/UserFiles/emailatachment/" ReadOnly="true" /></td>
    </tr>
    <tr runat="server" id="Tr2">                    
        <td class="caption">الصاق فایل ضمیمه</td>                    
        <td class="control"><kfk:FileUploader ID="fup2" runat="server" SaveFolder="~/UserFiles/emailatachment/" ReadOnly="true" /></td>
    </tr>
    <tr runat="server" id="Tr3">                    
        <td class="caption">الصاق فایل ضمیمه</td>                    
        <td class="control"><kfk:FileUploader ID="fup3" runat="server" SaveFolder="~/UserFiles/emailatachment/" ReadOnly="true" /></td>
    </tr>
    <tr runat="server" id="Tr4">                    
        <td class="caption">الصاق فایل ضمیمه</td>                    
        <td class="control"><kfk:FileUploader ID="fup4" runat="server" SaveFolder="~/UserFiles/emailatachment/" ReadOnly="true" /></td>
    </tr>
    <tr runat="server" id="Tr5">                    
        <td class="caption">الصاق فایل ضمیمه</td>                    
        <td class="control"><kfk:FileUploader ID="fup5" runat="server" SaveFolder="~/UserFiles/emailatachment/" ReadOnly="true" /></td>
    </tr>    
</table>
<div class="commands">
    <%--<asp:Button runat="server" ID="btnSave" Text="ثبت" CssClass="large" OnClick="OnSave" CausesValidation="true" />--%>
    <asp:Button runat="server" ID="btnEmailBlock" Text="ریز بسته های ارسالی" CssClass="xlarge" OnClick="btnEmailBlock_Click" CausesValidation="false" />    
    <asp:Button runat="server" ID="btnCancel" Text="بازگشت &raquo;" CssClass="large" OnClick="OnCancel" CausesValidation="false" />    
</div>

