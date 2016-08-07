<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_User_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr runat="server" id="UserID">
        <td class="caption">کد اشتراک</td>
        <td class="control">
            <asp:Label ID="lblUserID" runat="server"></asp:Label></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td class="caption">نام و نام خانوادگی</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtFullName" Required="true" />
        </td>


        <td class="caption">جنسیت</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcGender"
                runat="server"
                EnumParameterType="pgc.Model.Enums.Gender" />
        </td>
    </tr>
    <tr>
        <td class="caption">نقش</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcRole"
                runat="server"
                EnumParameterType="pgc.Model.Enums.Role"
                AutoPostBack="true"
                DependantControl="lkcAccessLevel"
                Required="true"
                OnSelectedIndexChanged="Role_Changed" />
        </td>
        <td class="caption">سطح دسترسی</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcAccessLevel"
                runat="server"
                BusinessTypeName="pgc.Business.Lookup.AccessLevelLookupBusiness"
                DependOnParameterName="Role"
                DependOnParameterType="Int32"
                Required="true" />
        </td>

    </tr>
    <tr runat="server" id="branch">
        <td class="caption">شعبه</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcBranch"
                runat="server"
                BusinessTypeName="pgc.Business.Lookup.BranchLookupBusiness"
                DefaultItemText="--"
                AddDefaultItem="True" />
        </td>
        <td></td>
        <td></td>
    </tr>
    <%--<tr>
        <td class="caption">استان</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcProvince"
                runat="server"
                BusinessTypeName="pgc.Business.Lookup.ProvinceLookupBusiness"
                AutoPostBack="true"
                DependantControl="lkcCity"
                Required="true" />
        </td>
        <td class="caption">شهرستان</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcCity"
                runat="server"
                BusinessTypeName="pgc.Business.Lookup.CityLookupBusiness"
                DependOnParameterName="Province_ID"
                DependOnParameterType="Int64"
                Required="true" />
        </td>
    </tr>--%>
    <tr>
        <%--<td class="caption">نام کاربری</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtUsername" Required="true" />
        </td>--%>
        <td class="caption">ایمیل</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtEmail" Mode="Email" Required="true"/>
        </td>
        <td class="caption">وضعیت</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcActivityStatus"
                runat="server"
                EnumParameterType="pgc.Model.Enums.UserActivityStatus"
                Required="true" />
        </td>
    </tr>
    <tr runat="server" id="PasswordEntranceRow">
        <td class="caption">کلمه عبور</td>
        <td class="control">
            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvPass" ControlToValidate="txtPassword" Text="*" ToolTip="لطفا کلمه عبور را وارد نمائید" CssClass="validator"></asp:RequiredFieldValidator>
        </td>
        <td class="caption">تکرار کلمه عبور</td>
        <td class="control">
            <asp:TextBox runat="server" ID="txtPasswordConfirm" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvPassConfirm" ControlToValidate="txtPasswordConfirm" Text="*" ToolTip="لطفا کلمه عبور را تکرار نمائید" CssClass="validator"></asp:RequiredFieldValidator>
            <asp:CompareValidator runat="server" ID="cpvPass" ControlToCompare="txtPassword" ControlToValidate="txtPasswordConfirm" Text="!" ToolTip="کلمه عبور با تکرار آن یکسان نمی باشد" CssClass="validator"></asp:CompareValidator>
        </td>
    </tr>
    <tr runat="server" id="PasswordResetRow">
        <td class="caption">کلمه عبور</td>
        <td class="control">
            <asp:HyperLink ID="hplResetPwd" runat="server">باز نشانی کلمه عبور</asp:HyperLink>
            <%--<a href="#" class="btn">باز نشانی کلمه عبور</a>--%>
        </td>
    </tr>
    <tr>
        <td class="caption">تاریخ عضویت</td>
        <td class="control">
            <kfk:PersianDatePicker runat="server" ID="pdpSignUpPersianDate" Required="true" />
        </td>
        <td class="caption">کد ملی</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtNationalCode" />
        </td>
    </tr>
    <tr>
        
        <td class="caption">شماره تماس</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtTel" Mode="Phone" />
        </td>
        <td class="caption">تلفن همراه</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtMobile" Mode="Phone" />
        </td>
    </tr>
    <tr>
        <td class="caption">دور نگار</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtFax" Mode="Phone" />
        </td>
        <td class="caption">کد پستی</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtPostalCode" Mode="Phone" />
        </td>
    </tr>
    <tr>     
        <td class="caption">آدرس</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtAddress" TextMode="MultiLine" />
        </td>
    </tr>

</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

