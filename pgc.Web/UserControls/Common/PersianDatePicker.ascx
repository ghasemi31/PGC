<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PersianDatePicker.ascx.cs" Inherits="UserControl_PersianDatePicker" %>
<div class="dp" runat="server" id="pnlDP">
    <asp:TextBox    ID="txtPersianDate" 
                    runat="server" 
                    CssClass="textbox">
    </asp:TextBox>
    <div class="ico"></div>
    <asp:PlaceHolder runat="server" ID="plhvalidator"></asp:PlaceHolder>
</div>