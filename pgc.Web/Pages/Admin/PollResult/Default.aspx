<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Admin_PollResult_Default" %>
<%@ Import Namespace="pgc.Model" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="<%#ResolveClientUrl("~/Styles/form.css")%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
<fieldset>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
 <table class="Pollform">

         <% int count;
            foreach (PollChoise Choise in result.GetPollChoise(PollID))
          {  %>
            <tr>
             <td class="caption"><label id="lblChoise" ><%=Choise.Description%></label></td>

             <% count= result.GetResult(Choise.ID); %>
             <td class="control"><label id="lblCount"><%=count%></label></td>
            </tr>
        <%} %>

</table>
<div class="commands">
    <asp:Button runat="server" ID="btnCancel" Text="بازگشت" CssClass="large" 
        CausesValidation="false" onclick="btnCancel_Click" />
</div>
</fieldset>
</asp:Content>

