<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Agent_UpdateProfile_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
<fieldset>
      <legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
  <table>
    <tr>
        <td class="caption">تاریخ عضویت</td>
        <td class="control"><asp:Label ID="lblSignUpPersianDate" runat="server"></asp:Label></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td class="caption">نقش</td>
        <td class="control"><asp:Label ID="lblRole" runat="server"></asp:Label></td>
        <td class="caption">سطح دسترسی</td>
        <td class="control"><asp:Label ID="lblAccessLevel" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">نام و نام خانوادگی</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtFullname" Required="true"/></td>
        <td class="caption">ایمیل</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtEmail" Mode="Email"/></td>
       <%-- <td class="caption">نام خانوادگی</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtlName" Required="true"/></td>--%>
    </tr>
   <%-- <tr>
        <td class="caption">استان</td>
        <td class="control"><kfk:LookupCombo ID="lkcProvince" 
                                            runat="server" 
                                            BusinessTypeName="pgc.Business.Lookup.ProvinceLookupBusiness"
                                            AutoPostBack="true"
                                            DependantControl="lkcCity"
                                            Required="true" /></td>
        <td class="caption">شهرستان</td>
        <td class="control"><kfk:LookupCombo ID="lkcCity" 
                                            runat="server" 
                                            BusinessTypeName="pgc.Business.Lookup.CityLookupBusiness"
                                            DependOnParameterName="Province_ID" 
                                            DependOnParameterType="Int64"
                                            Required="True" /></td>
    </tr>--%>

    <tr>
        <td class="caption">جنسیت</td>
        <td class="control"><kfk:LookupCombo ID="lkcGender" 
                                    runat="server" 
                                    EnumParameterType="pgc.Model.Enums.Gender" /></td>
        <td class="caption">کد ملی</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtNationalCode"/></td>
        <%--<td class="caption">نام کاربری</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtUsername" Required="true"/></td>--%>
    </tr>
    <tr>
        <td class="caption">تلفن همراه</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtMobile" Mode="Phone"/></td>
        <td class="caption">شماره تماس</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtTel" Mode="Phone"/></td>
    </tr>
    <tr>
        <td class="caption">دور نگار</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtFax" Mode="Phone"/></td>
        <td class="caption">کد پستی</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtPostalCode" Mode="Phone"/></td>
    </tr>
    <tr>
        <td class="caption">آدرس</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtAddress" TextMode="MultiLine"/></td>
        <td></td>
        <td></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="btnSave_Click" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="btnCancel_Click" CausesValidation="false" />
</div>
</fieldset>
</asp:Content>

