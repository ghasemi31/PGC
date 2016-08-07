<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_Product_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>

    <tr>
        <td class="caption">عنوان محصول</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtTitle" runat="server" TextBoxWidth="212" Required="true" />
        </td>        
    </tr>
    <tr>
         <td class="caption">قیمت محصول</td>
        <td class="control">
            <kfk:NumericTextBox ID="txtPrice" runat="server" TextBoxWidth="212" UnitText="ریال" />
        </td>
    </tr>
    <tr>
        <td class="caption">توضیحات محصول</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtBody" runat="server" Required="true" TextMode="MultiLine" />
        </td>         
    </tr>
    <tr>
        <td class="caption">اولویت نمایش</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtDispOrder" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">عنوان صفحه</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtPageTitle" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">توضیحات صفحه</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtPageDescription" runat="server" TextMode="MultiLine" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">کلید واژه های صفحه</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtPageKeywords" runat="server" TextMode="MultiLine" CssClass="xxlarge" Required="true" />
            <br />
            <span style="color: #bd0019">هر کلمه را با کلید Enter از هم جدا کنید</span>
        </td>
    </tr>
    <tr>        
        <td class="caption">تصویر اول محصول در اسلایدر</td>
        <td class="control">
            <kfk:FileUploader ID="fupSliderProductPic" runat="server" SaveFolder="~/userfiles/product/" />
            <ul style="color: #bd0019">
                <li>فرمت مناسب: png/jpg</li>
                <li>width مناسب: 280 px</li>
                <li>height مناسب: 355 px</li>
            </ul>
        </td>

         <td class="caption">تصویر دوم محصول در اسلایدر</td>
        <td class="control">
            <kfk:FileUploader ID="fupSliderHoverProductPic" runat="server" SaveFolder="~/userfiles/product/" />
            <ul style="color: #bd0019">
                <li>فرمت مناسب: png/jpg</li>
                <li>width مناسب: 280 px</li>
                <li>height مناسب: 355 px</li>
            </ul>
        </td>
    </tr>
    <tr>       
        <td class="caption">تصویر محصول در صفحه جزئیات</td>
        <td class="control">
            <kfk:FileUploader ID="fupProductPic" runat="server" SaveFolder="~/userfiles/product/" />
            <ul style="color: #bd0019">
                <li>فرمت مناسب: png/jpg</li>
                <li>width مناسب: 1920 px</li>
                <li>height مناسب: 840 px</li>
            </ul>
        </td>
    </tr>
    <tr>
        <td class="caption">قابل نمایش در اسلایدر</td>
        <td class="control">
            <asp:CheckBox runat="server" ID="chkDisplayInSlider" Text="" CssClass="checkbox" /></td>

         <td class="caption">محصول جانبیست؟</td>
        <td class="control">
            <asp:CheckBox runat="server" ID="chkAccessories" Text="" CssClass="checkbox" /></td>
    </tr>
    <tr>
         <td class="caption">محصول فعال است؟</td>
        <td class="control">
            <asp:CheckBox runat="server" ID="chkIsActive" Text="" CssClass="checkbox" /></td>
    </tr>
    <tr>
        <td class="caption">مواد اولیه</td>
        <td class="control" colspan="3">
            <asp:CheckBoxList ID="chlMaterials" RepeatColumns="6" runat="server"></asp:CheckBoxList></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

