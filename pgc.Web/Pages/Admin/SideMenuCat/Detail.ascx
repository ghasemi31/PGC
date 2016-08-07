<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_SideMenuCat_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" Required="true"/></td>
    </tr>
    <tr>
        <td class="caption">آدرس لینک</td>
        <td class="control"><kfk:NormalTextBox ID="txtNavigationUrl" runat="server" Required="true" TextBoxWidth="200" /></td>
    </tr>
    <tr>
        <td class="caption">اولویت نمایش</td>
        <td class="control"><kfk:NumericTextBox ID="txtDispOrder" runat="server" SupportComma="false" SupportLetter="false" TextBoxWidth="50" /></td>
    </tr>
    <tr>
        <td class="caption">قابل نمایش</td>
        <td class="control"><asp:CheckBox runat="server" ID="chkIsVisible" Text="" CssClass="checkbox" /></td>
    </tr>
    <tr>
        <td class="caption">صفحه در Tab جدید باز شود</td>
        <td class="control"><asp:CheckBox runat="server" ID="chkIsBlank" Text="" CssClass="checkbox" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

