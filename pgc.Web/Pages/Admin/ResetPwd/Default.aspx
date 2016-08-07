<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Admin_ResetPwd_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
    <fieldset>
      <legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
        <table class="Pollform">
            <tr>
                <td class="caption">کاربر</td>
                <td class="control"><asp:Label ID="lblName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="caption">نام کاربری</td>
                <td class="control"><asp:Label ID="lblUserName" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server" id="PasswordEntranceRow">
                <td class="caption">کلمه عبور</td>
                <td class="control">
                    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvPass" ControlToValidate="txtPassword" Text="*" ToolTip="لطفا کلمه عبور را وارد نمائید" CssClass="validator"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="caption">تکرار کلمه عبور</td>
                <td class="control">
                    <asp:TextBox runat="server" ID="txtPasswordConfirm" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvPassConfirm" ControlToValidate="txtPasswordConfirm" Text="*" ToolTip="لطفا کلمه عبور را تکرار نمائید" CssClass="validator"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cpvPass" ControlToCompare="txtPassword" ControlToValidate="txtPasswordConfirm" Text="!" ToolTip="کلمه عبور با تکرار آن یکسان نمی باشد" CssClass="validator"></asp:CompareValidator>
                </td>
            </tr>
        </table>
        <div class="commands">
            <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" onclick="btnSave_Click" CausesValidation="true" />
            <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" onclick="btnCancel_Click" CausesValidation="false" />
        </div>
    </fieldset>
</asp:Content>

