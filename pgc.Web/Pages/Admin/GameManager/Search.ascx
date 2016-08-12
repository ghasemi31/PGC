<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_GameManager_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">نام و نام خانوادگی</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtName" />
        </td>

        <td class="caption">جنسیت</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcGender"
                runat="server"
                EnumParameterType="pgc.Model.Enums.Gender"
                AddDefaultItem="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">وضعیت</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcActivityStatus"
                runat="server"
                EnumParameterType="pgc.Model.Enums.UserActivityStatus"
                AddDefaultItem="true" />
        </td>

        <td class="caption">سطح دسترسی</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcAccessLevel"
                runat="server"
                BusinessTypeName="pgc.Business.Lookup.AccessLevelLookupBusiness"
                DependOnParameterName="Role"
                DependOnParameterType="Int32"
                AddDefaultItem="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">ایمیل</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtEmail" />
        </td>
        <td class="caption">شماره تماس</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtTel" Mode="Phone" />
        </td>
    </tr>
    <tr>
        <td class="caption">تلفن همراه</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtMobile" Mode="Phone" />
        </td>
        <td class="caption">آدرس</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtAddress" />
        </td>
    </tr>

    <tr>
        <td class="caption">تاریخ عضویت</td>
        <td class="control">
            <kfk:PersianDateRange runat="server" ID="pdrSignUpPersianDate" />
        </td>
        
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
