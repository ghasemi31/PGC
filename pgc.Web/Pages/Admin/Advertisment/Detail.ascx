<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_Advertisment_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>

    <tr>
        <td class="caption">عنوان </td>
        <td class="control">
            <kfk:NormalTextBox ID="txtTitle" runat="server" TextBoxWidth="212" Required="true"/>
        </td>
    </tr>
    <tr>
        <td class="caption">نوع فایل</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcAdvType"
                runat="server"
                EnumParameterType="pgc.Model.Enums.AdvertismentType" />
        </td>
    </tr>
    <tr>
        <td class="caption">لینک</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtLink" runat="server"  Required="true" />
        </td>
    </tr>

   <tr>
        <td class="caption">اولویت نمایش</td>
        <td class="control">
            <kfk:NumericTextBox ID="txtDispOrder" runat="server" SupportComma="false" SupportLetter="false" TextBoxWidth="212" Required="true" />
        </td>
    </tr>

    <tr>
        <td class="caption">فایل</td>
        <td class="control">
            <kfk:FileUploader ID="fupPic" runat="server" Required="true" SaveFolder="~/userfiles/adv/" />
            <ul style="color: #bd0019">
                <li>فرمت مناسب: gif/swf/png/jpg</li>
                <li>width مناسب: 335x</li>
                <li>height مناسب: 120x</li>
            </ul>
        </td>
    </tr>
   
    

</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />

    
</div>

