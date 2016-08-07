<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchManualCharge_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption" style="width:150px;">شعبه</td>
        <td class="control"><kfk:LookupCombo ID="lkpBranch" 
                                            BusinessTypeName="pgc.Business.Lookup.BranchLookupBusiness"
                                            runat="server" 
                                            Required="true" /></td>
    </tr>
    <tr>
        <td class="caption" style="width:150px;">مبلغ</td>
        <td class="control"><kfk:NumericTextBox ID="txtPrice" runat="server" Required="true" />
            ریال</td>
    </tr>
    <tr>
        <td class="caption" style="width:150px;">بدهکاری / بستانکاری شعبه</td>
        <td class="control" colspan="2">
            <asp:RadioButton ID="rdbIndebt" runat="server" GroupName="transactiontype" value="0" />بدهکار
            <asp:RadioButton ID="rdbIncredit" runat="server" GroupName="transactiontype" value="1"/>بستانکار
        </td>
    </tr>
    <tr>
        <td class="caption" style="width:150px;">توضیحات</td>
        <td class="control"><kfk:NormalTextBox ID="txtDesc" runat="server" Required="true" TextMode="MultiLine" TextBoxWidth="400" TextBoxHeight="150"/></td>
    </tr>
    
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

