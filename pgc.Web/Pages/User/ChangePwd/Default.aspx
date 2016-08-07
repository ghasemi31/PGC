<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_User_ChangePwd_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
<fieldset>
      <legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
        <table>
            <tr >
                <td class="caption">کلمه عبور قبلی</td>
                <td class="control">
                    <asp:TextBox runat="server" ID="txtOldPassword" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvOldPass" ControlToValidate="txtOldPassword" Text="*" ToolTip="لطفا کلمه عبور جدید را وارد نمائید" CssClass="validator"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr runat="server" id="Tr1">
                <td class="caption">کلمه عبور جدید</td>
                <td class="control">
                    <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvNewPass" ControlToValidate="txtNewPassword" Text="*" ToolTip="لطفا کلمه عبور جدید را وارد نمائید" CssClass="validator"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="caption">تکرار کلمه عبور جدید</td>
                <td class="control">
                    <asp:TextBox runat="server" ID="txtPasswordConfirm" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvPassConfirm" ControlToValidate="txtPasswordConfirm" Text="*" ToolTip="لطفا کلمه عبور جدید را تکرار نمائید" CssClass="validator"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cpvPass" ControlToCompare="txtNewPassword" ControlToValidate="txtPasswordConfirm" Text="!" ToolTip="کلمه عبور با تکرار آن یکسان نمی باشد" CssClass="validator"></asp:CompareValidator>
                </td>
            </tr>
        </table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="btnSave_Click" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="btnCancel_Click" CausesValidation="false" />
</div>
</fieldset>
</asp:Content>

