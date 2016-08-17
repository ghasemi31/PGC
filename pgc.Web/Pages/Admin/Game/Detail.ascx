<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_Game_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table class="Gameform">
    <tr>
        <td class="caption">عنوان</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtTitle" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>

    <tr>
        <td class="caption">نوع بازی</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcGameType"
                runat="server"
                EnumParameterType="pgc.Model.Enums.GameType" />
        </td>
    </tr>

    <tr>
        <td class="caption">نحوه اجرای بازی</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcHowType"
                runat="server"
                EnumParameterType="pgc.Model.Enums.GameHowType" />
        </td>
    </tr>
    <tr>
        <td class="caption">پلتفرم</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtPlatform" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">مدیر بازی</td>
        <td class="control">
            <kfk:LookupCombo ID="lkpManager"
                BusinessTypeName="pgc.Business.Lookup.GameManagerLookupBusiness"
                runat="server" />
        </td>


    </tr>
    <tr>
        <td class="caption">تعداد بازیکن ها</td>
        <td class="control">
            <kfk:NumericTextBox ID="ntbGamerCnt" runat="server" Required="true" SupportComma="false" SupportLetter="false" TextBoxWidth="212" />
        </td>
    </tr>

    <tr>
        <td class="caption">معرفی بازی</td>
        <td class="control">
            <kfk:HtmlEditor ID="txtLaws" runat="server" Required="true" />
        </td>
    </tr>

    <tr>
        <td class="caption">قوانین و مقررات بازی</td>
        <td class="control">
            <kfk:HtmlEditor ID="txtLawsGame" runat="server" Required="true" />
        </td>
    </tr>

    <tr>
        <td class="caption">جایزه نفر اول</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtFirstPresent" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">جایزه نفر دوم</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtSecondPresent" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">جایزه نفر سوم</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtThirdPresent" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">هزینه ثبت نام</td>
        <td class="control">
            <kfk:NumericTextBox ID="ntbCost" runat="server" SupportComma="true" SupportLetter="true" TextBoxWidth="212" Required="true" />
            ریال
        </td>
    </tr>

    <tr>
        <td class="caption">اولویت نمایش</td>
        <td class="control">
            <kfk:NumericTextBox ID="txtDispOrder" runat="server" SupportComma="false" SupportLetter="false" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">کلمه کلیدی Url </td>
        <td class="control" style="width: 280px">
            <kfk:NormalTextBox ID="txtUrlKey" runat="server" TextBoxWidth="212" Required="true" />
            /www.iranpgc.com/gamedetail
           
            <br />
            <span style="color: #bd0019">ترجیحا از فاصله (space) استفاده نکنید , 
                فقط از حروف و اعداد استفاده شود , 
                ترجیحا از حروف لاتین استفاده نمایید , 
                برای جدا کردن کلمات از خط تیره (-) استفاده شود 
            </span>
        </td>
    </tr>

    <tr>
        <td class="caption">تصویر</td>
        <td class="control">
            <kfk:FileUploader ID="fupPic" runat="server" SaveFolder="~/userfiles/Game/" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">لوگو</td>
        <td class="control">
            <kfk:FileUploader ID="fupLogo" runat="server" SaveFolder="~/userfiles/Game/" />
        </td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

