<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_Branch_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table class="branchform">
    <tr>
        <td class="caption">عنوان</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtTitle" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>

    <tr>
        <td class="caption">آدرس</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtAddress" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">کد شهر</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtCode" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>


    <tr>
        <td class="caption">شماره تلفن ها
          
        </td>
        <td class="control">
            <kfk:NormalTextBox ID="txtPhoneNumbers" runat="server" TextMode="MultiLine" CssClass="xxlarge" />
            <br />
            <span style="color: #bd0019">هرشماره تلفن را در یک خط وارد کنید<br />
                شماره تلفن را بدون کد وارد کنید
            </span>
        </td>
    </tr>
    <tr>
        <td class="caption">نوع شعبه</td>
        <td class="control">
            <asp:DropDownList ID="branchType" runat="server">
                <asp:ListItem Value="1" Text="شعبه تهران" Selected="True"></asp:ListItem>
                <asp:ListItem Value="2" Text="شعبه شهرستان"></asp:ListItem>
                <asp:ListItem Value="3" Text="شعبه خارج از کشور"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="caption">ساعت سفارش گیری</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtOrdering" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">ساعت سرو غذا</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtServing" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">تعداد صندلی</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtChair" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">هزینه حمل و نقل</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtTransportCost" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">عرض جغرافیایی شعبه</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtlatitude" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">طول جغرافیایی شعبه</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtLongitude" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">اولویت نمایش</td>
        <td class="control">
            <kfk:NumericTextBox ID="txtDispOrder" runat="server" SupportComma="false" SupportLetter="false" TextBoxWidth="212" />
        </td>
    </tr>
    <tr>
        <td class="caption">کلمه کلیدی Url </td>
        <td class="control" style="width: 280px">
            <kfk:NormalTextBox ID="txtUrlKey" runat="server" TextBoxWidth="212" Required="true" />
            /www.pgcizi.com/branch
            <br />
            <span style="color: #bd0019">ترجیحا از فاصله (space) استفاده نکنید , 
                فقط از حروف و اعداد استفاده شود , 
                ترجیحا از حروف لاتین استفاده نمایید , 
                برای جدا کردن کلمات از خط تیره (-) استفاده شود 
            </span>
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
        <td class="caption">تصویر در لیست شعب</td>
        <td class="control">
            <kfk:FileUploader ID="fupThumbListPic" runat="server" SaveFolder="~/userfiles/BranchList/" />
            <ul style="color: #bd0019;">
                <li>فرمت مناسب: png/jpg</li>
                <li>width مناسب: 690 px</li>
                <li>height مناسب: 450 px</li>
            </ul>
        </td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

