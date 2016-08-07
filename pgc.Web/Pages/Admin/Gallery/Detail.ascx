<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_Gallery_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" Required="true"/></td>
    </tr>
    <tr>
        <td class="caption">اولویت نمایش</td>
        <td class="control"><kfk:NumericTextBox ID="txtDispOrder" runat="server" SupportComma="false" SupportLetter="false" TextBoxWidth="50" /></td>
    </tr>
    <tr>
        <td class="caption">تصویر</td>
        <td class="control"><kfk:FileUploader ID="fupGallery" runat="server" SaveFolder="~/userfiles/gallery/" />
           <ul style="color:#bd0019">
                <li>فرمت مناسب: png/jpg</li>                
            </ul>      
        </td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

