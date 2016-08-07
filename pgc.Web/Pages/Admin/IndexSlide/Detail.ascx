<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_IndexSlide_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" Required="true" TextMode="MultiLine"/></td>
    </tr>
    <tr>
        <td class="caption">آدرس لینک</td>
        <td class="control"><kfk:NormalTextBox ID="txtNavigateUrl" runat="server" Required="true" TextBoxWidth="200" /></td>
    </tr>
    <tr>
        <td class="caption">اولویت نمایش</td>
        <td class="control"><kfk:NumericTextBox ID="txtDispOrder" runat="server" SupportComma="false" SupportLetter="false" TextBoxWidth="50"/></td>
    </tr>
    <tr>
        <td class="caption">عکس</td>
        <td class="control"><kfk:FileUploader ID="fupPic" runat="server" SaveFolder="~/userfiles/slider/" Required="false" />
           <ul style="color:#bd0019">
                <li>فرمت مناسب: png/gif</li>
                <li>width مناسب: 887 px</li>
                <li>height مناسب: 216 px</li>
                <li><a href="<%=ResolveClientUrl("~/Pages/Guest/pgciziPix/PSDs/Frame-Slider.psd") %>" style=" text-decoration:underline; color:#bd0019">نمومه قالب(فایل psd)</a></li>
            </ul>
        </td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

