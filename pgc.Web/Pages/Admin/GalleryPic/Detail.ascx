<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_GalleryPic_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">گالری</td>
        <td class="control"><kfk:LookupCombo ID="lkpGallery" 
                                            BusinessTypeName="pgc.Business.Lookup.GalleryLookupBusiness"
                                            runat="server" 
                                            Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">توضیحات</td>
        <td class="control"><kfk:NormalTextBox ID="txtDesc" runat="server" Required="true"/></td>
    </tr>
    <tr>
        <td class="caption">اولویت نمایش</td>
        <td class="control"><kfk:NumericTextBox ID="txtDispOrder" runat="server" SupportComma="false" SupportLetter="false" TextBoxWidth="50"/></td>
    </tr>
    <tr>
        <td class="caption">عکس</td>
        <td class="control"><kfk:FileUploader ID="fupGalleryPic" runat="server" SaveFolder="~/userfiles/GalleryPic/" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

