<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_OccuredSystemEvent_Detail"  %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">عنوان رخداد</td>
        <td class="control"><asp:Label ID="lblEventTile" runat="server"></asp:Label></td>

        <td class="caption">تاریخ اقدام</td>
        <td class="control"><asp:Label ID="lblDate" runat="server"></asp:Label></td>
    </tr>    
    <tr>
        <td class="caption">اقدامات</td> 
        <td class="control" colspan="3"><asp:Label ID="lblDescription" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption" id="LinkMailCell" runat="server"><asp:HyperLink ID="linkMail" runat="server"></asp:HyperLink></td>    
    </tr>
    <tr>
        <td class="caption" id="LinkSMSCell" runat="server"><asp:HyperLink ID="linkSMS" runat="server"></asp:HyperLink></td>    
    </tr>
</table>
<div class="commands">
    <asp:Button Visible="false" runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="بازگشت" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

