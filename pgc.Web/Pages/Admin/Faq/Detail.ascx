<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_Faq_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>

    <tr>
        <td class="caption">عنوان سوال</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">خلاصه</td>
        <td class="control"><kfk:NormalTextBox ID="txtSummery" runat="server" TextMode="MultiLine" /></td>
    </tr>
    <tr>
         <td class="caption">متن سوال</td>
        <td class="control"><kfk:HtmlEditor ID="txtBody" runat="server" Required="true" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

