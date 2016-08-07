<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Lookup.ascx.cs" Inherits="UserControl_Lookup" %>
<table cellpadding="0" cellspacing="0" class="lk">
    <tr>
        <td>
            <div class="lk" onclick="openLookup(    '<%= this.ResolveClientUrl(URL)%>', 
                                                    <%= LookupWidth%>, 
                                                    <%= LookupHeight%>, 
                                                    <%= ColumnIndex%>, 
                                                    '<%= hfResultValue.ClientID%>', 
                                                    '<%= hfResultText.ClientID%>', 
                                                    '<%= txtSearch.ClientID%>');">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="textbox" ReadOnly="true"></asp:TextBox>
                <div class="ico"></div>
            </div>
        </td>
        <td>
            <div    class="reset" 
                    style="display:<%= (txtSearch.Text != "")?"block":"none" %>"
                    onclick="resetLookup('<%= hfResultValue.ClientID%>', 
                                        '<%= hfResultText.ClientID%>', 
                                        '<%= txtSearch.ClientID%>');">
            </div>
        </td>
        <td>
            <asp:PlaceHolder runat="server" ID="plhvalidator"></asp:PlaceHolder>        
        </td>
        <td>
            <div style="display:none">
                <asp:HiddenField ID="hfResultValue" runat="server" Value="0" />
                <asp:HiddenField ID="hfResultText" runat="server" Value="" />
            </div>
        </td>
    </tr>
</table>
<div class="FrameContainer"></div>
<iframe frameborder="0" id="GridFrame" width="100%" height="100%" style="display: none;"></iframe>
