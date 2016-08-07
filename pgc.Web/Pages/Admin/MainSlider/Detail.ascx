<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_RandomImage_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>

    <tr>
        <td class="caption">عنوان</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtTitle" runat="server" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">اولویت نمایش</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtDispOrder" runat="server" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">قابل نمایش </td>
        <td class="control">
            <asp:CheckBox runat="server" ID="chkIsVisible" Text="" CssClass="checkbox" /></td>
    </tr>
    <tr>
        <td class="caption">عکس</td>
        <td class="control">
            <kfk:FileUploader ID="fupPic" runat="server" SaveFolder="~/userfiles/MainSlider/" />
            <ul style="color: #bd0019">
                <li>فرمت مناسب: png/jpg</li>
                <li>width مناسب: 1400 px</li>
                <li>height مناسب: 800 px</li>
            </ul>
        </td>
    </tr>

</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>
