<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NumericRange.ascx.cs" Inherits="UserControl_Common_NumericRange" %>
<%@ Register TagName="NumericTextBox" TagPrefix="uc1" Src="~/UserControls/Common/NumericTextBox.ascx" %>
<table cellpadding="1" cellspacing="1" class="numericrange">
    <tr>
        <td>
            <asp:DropDownList ID="cboType" runat="server" Width="98" CssClass="public-input">
                <asp:ListItem Value="0" Text="فرقی نمی کند"></asp:ListItem>
                <asp:ListItem Value="1" Text="برابر با"></asp:ListItem>
                <asp:ListItem Value="2" Text="بیشتر از"></asp:ListItem>
                <asp:ListItem Value="3" Text="کمتر از"></asp:ListItem>
                <asp:ListItem Value="4" Text="ما بین"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <uc1:NumericTextBox ID="ntbFirst" runat="server" TextBoxWidth="50"/>        
        </td>
        <td>
            <label runat="server" id="lblAnd">و</label>        
        </td>
        <td>
            <uc1:NumericTextBox ID="ntbSecond" runat="server" TextBoxWidth="50"/>        
        </td>
    </tr>
</table>