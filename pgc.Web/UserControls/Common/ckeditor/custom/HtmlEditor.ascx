<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HtmlEditor.ascx.cs" Inherits="UserControls_Common_ckeditor_HtmlEditor" %>
<asp:Panel runat="server" ID="pnl">
    <kfk:HKTextBox ID="txt" runat="server" ></kfk:HKTextBox>
    <input  type="image" 
            class="htmleditor_lookup"  
            value=""
            onclick="return openHtmlEditorLookup(
                    '<%=pnl.ClientID %>',
                    '900',
                    '600',
                    '<%=this.Page.ResolveClientUrl("~/UserControls/Common/ckeditor/custom/HtmlLookup.aspx") %>'
                )"/>
    <div id="<%=pnl.ClientID %>_cont" class="framecontainer"></div>
    <iframe frameborder="0" id="<%=pnl.ClientID %>_frame" width="100%" height="100%" style="display: none;"></iframe>
</asp:Panel>