<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageControl.ascx.cs"
    Inherits="UserControl_MessageControl" %>
<div>
    <textarea runat="server" id="Msg" class="Msg-tarea" onkeypress="SMSKeyPress(this)" onkeyup="SMSKeyPress(this)"></textarea>
</div>
<div>
    <span id="sms-lang" class="sms_lang" ><%= kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle(ReturnMsgType(Msg.InnerText))%> </span>
    <span id="remaining-characters" class="remaining-characters" style="padding-right:30px;" >تعداد کاراکتر باقیمانده <strong><%=ReturnMsgCharCount(Msg.InnerText)%></strong> ( <strong><%=ReturnMsgCount(ReturnMsgCharCount(Msg.InnerText), ReturnMsgType(Msg.InnerText))%></strong>صفحه )</span>    
</div>
