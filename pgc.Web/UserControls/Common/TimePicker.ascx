<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TimePicker.ascx.cs" Inherits="UserControl_TimePicker" %>
<table style="text-align: right">
    <tr>
        <td>
            <div class="help">
                دقیقه</div>
            <asp:DropDownList ID="Minute" runat="server">
                <asp:ListItem Value="00">00</asp:ListItem>
                <asp:ListItem Value="05">05</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="15">15</asp:ListItem>
                <asp:ListItem Value="20">20</asp:ListItem>
                <asp:ListItem Value="25">25</asp:ListItem>
                <asp:ListItem Value="30">30</asp:ListItem>
                <asp:ListItem Value="35">35</asp:ListItem>
                <asp:ListItem Value="40">40</asp:ListItem>
                <asp:ListItem Value="45">45</asp:ListItem>
                <asp:ListItem Value="50">50</asp:ListItem>
                <asp:ListItem Value="55">55</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <div class="help">
                ساعت</div>
            <asp:DropDownList ID="Hour" runat="server">
                <asp:ListItem Value="00">00</asp:ListItem>
                <asp:ListItem Value="01">01</asp:ListItem>
                <asp:ListItem Value="02">02</asp:ListItem>
                <asp:ListItem Value="03">03</asp:ListItem>
                <asp:ListItem Value="04">04</asp:ListItem>
                <asp:ListItem Value="05">05</asp:ListItem>
                <asp:ListItem Value="06">06</asp:ListItem>
                <asp:ListItem Value="07">07</asp:ListItem>
                <asp:ListItem Value="08">08</asp:ListItem>
                <asp:ListItem Value="09">09</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="11">11</asp:ListItem>
                <asp:ListItem Value="12">12</asp:ListItem>
                <asp:ListItem Value="13">13</asp:ListItem>
                <asp:ListItem Value="14">14</asp:ListItem>
                <asp:ListItem Value="15">15</asp:ListItem>
                <asp:ListItem Value="16">16</asp:ListItem>
                <asp:ListItem Value="17">17</asp:ListItem>
                <asp:ListItem Value="18">18</asp:ListItem>
                <asp:ListItem Value="19">19</asp:ListItem>
                <asp:ListItem Value="20">20</asp:ListItem>
                <asp:ListItem Value="21">21</asp:ListItem>
                <asp:ListItem Value="22">22</asp:ListItem>
                <asp:ListItem Value="23">23</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>