<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PersianDateRange.ascx.cs" Inherits="UserControl_PersianDateRange" %>
<%@ Register TagName="PersianDatePicker" TagPrefix="uc1" Src="~/UserControls/Common/PersianDatePicker.ascx" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td >
            <asp:DropDownList runat="server" ID="SearchModeSelector" Width="61" CssClass="range-combo">
                <asp:ListItem Selected="True" Value="0">--</asp:ListItem>
                <asp:ListItem Value="1">درتاریخ</asp:ListItem>
                <asp:ListItem Value="2">بعد از</asp:ListItem>
                <asp:ListItem Value="3">قبل از</asp:ListItem>
                <asp:ListItem Value="4">مابین</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <div id="FromDatePnl" runat="server" style="padding:0px 2px 0px 2px">
                <uc1:PersianDatePicker ID="txtFromDate" runat="server" />
            </div>
        </td>
        <td>
            <div id="AndPnl" runat="server" style="padding:0px 0px 0px 2px">
                و
            </div>
        </td>
        <td>
            <div id="ToDatePnl" runat="server" style="padding:0px 2px 0px 0px">
                <uc1:PersianDatePicker ID="txtToDate" runat="server" />
            </div>
        </td>
    </tr>
</table>
