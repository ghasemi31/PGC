<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_SocialIcon_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>

    <tr>
        <td class="caption">نام شبکه اجتماعی</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">آدرس url</td>
        <td class="control"><kfk:NormalTextBox ID="txtUrl" runat="server" Required="true"/></td>
    </tr>
    <tr>
         <td class="caption">اولویت نمایش</td>
        <td class="control"><kfk:NormalTextBox ID="txtDispOrder" runat="server" Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">قابل نمایش</td>
        <td class="control"><asp:CheckBox runat="server" ID="chkIsVisible" Text="" CssClass="checkbox" /></td>
    </tr> 
    <tr>
        <td class="caption">آیکون شبکه اجتماعی</td>
        <td class="control"><kfk:IconPicker ID="icpSocial" runat="server" Iconset="fontawesome" />           
        </td>
    </tr>      
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>
