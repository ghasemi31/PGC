<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BankAccount_Detail"  %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitleInvoiceSingleService" runat="server" Required="true" CssClass="large" /></td>       
    </tr>
    <tr>    
        <td class="caption">توضیحات</td>
        <td class="control"><kfk:NormalTextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="large" /></td>
    </tr>   
    <tr>
        <td class="caption"></td>
        <td class="control"><kfk:LookupCombo EnumParameterType="pgc.Model.Enums.OfflineBankAccountStatus" runat="server" ID="lkpStatus" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

