<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_MainMenu_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>

    <tr>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" Required="true" /></td>

        <td class="caption">آدرس لینک</td>
        <td class="control"><kfk:NormalTextBox ID="txtUrl" runat="server" Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">اولویت نمایش</td>
        <td class="control"><kfk:NormalTextBox ID="txtDispOrder" runat="server" Required="true" /></td>
    </tr>
<%--    <tr>
         <td class="caption">کد Html</td>
        <td class="control"><kfk:HtmlEditor ID="txtHtml" runat="server" /></td>
    </tr>--%>
    <tr>
        <td class="caption">فعال شدن اسلایدر محصولات</td>
        <td class="control"><asp:CheckBox runat="server" ID="chkHtml" Text="" CssClass="checkbox" /></td>

        <td class="caption">باز شدن صفحه در تب جدید</td>
        <td class="control"><asp:CheckBox runat="server" ID="chkIsBlank" Text="" CssClass="checkbox" /></td>
    </tr>
    <tr>
        <td class="caption">قابل نمایش در صفحه اصلی</td>
        <td class="control"><asp:CheckBox runat="server" ID="chkHomPage" Text="" CssClass="checkbox" /></td>

         <td class="caption">قابل نمایش در سایر صفحات</td>
        <td class="control"><asp:CheckBox runat="server" ID="chkOtherPage" Text="" CssClass="checkbox" /></td>
    </tr>

</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

