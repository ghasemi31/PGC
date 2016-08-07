<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_PrivateNo_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>

    <tr>
        <td class="caption">شماره</td>
        <td class="control"><kfk:NormalTextBox ID="txtNumber" runat="server" Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">وضعیت شماره</td>
        <td class="control"><kfk:LookupCombo EnumParameterType="pgc.Model.Enums.PrivateNoStatus" AddDefaultItem="false" runat="server" ID="lkpStatus" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

