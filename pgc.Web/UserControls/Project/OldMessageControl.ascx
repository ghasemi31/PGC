<%--<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OldMessageControl.ascx.cs" Inherits="UserControls_Project_MessageControl" %>
<div>
    <textarea runat="server" id="Msg" class="Msg-tarea" onkeypress="SMSKeyPress(this)" onkeyup="SMSKeyPress(this)"></textarea>
</div>
<div>
    <span id="sms-lang" ><%=kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle(ReturnMsgType(Msg.InnerText))%> </span>
    <span id="remaining-characters" style="padding-right:130px;" >تعداد کاراکتر باقیمانده <strong><%=ReturnMsgCharCount(Msg.InnerText)%></strong> ( <strong><%=ReturnMsgCount(ReturnMsgCharCount(Msg.InnerText), ReturnMsgType(Msg.InnerText))%></strong>صفحه )</span>    
</div>--%>