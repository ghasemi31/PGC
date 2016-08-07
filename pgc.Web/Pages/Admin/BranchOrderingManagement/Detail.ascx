<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchOrderingManagement_Detail" %>
<legend>
    <%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>

<table>
    <tr>
        <td class="caption">نام شعبه</td>
        <td class="control"><%=Branch.Title %></td>
    </tr>
</table>
<div id="containerTBL" runat="server" class="detailtbl">
</div>

<div class="commands">
    <asp:Button runat="server" ID="btnRevise" Text="ویرایش مجدد" CssClass="large scommand" onclick="CanclePreview_Click" Visible="false" />
    <asp:Button runat="server" ID="btnPreview" Text="پیش نمایش" CssClass="large scommands" onclick="Preview_Click" />
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large scommands" OnClick="OnSave" CausesValidation="true" Visible="false" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>
