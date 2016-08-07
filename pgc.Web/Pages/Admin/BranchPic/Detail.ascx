<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchPic_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">شعبه</td>
        <td class="control"><kfk:LookupCombo ID="lkpBranch" 
                                            BusinessTypeName="pgc.Business.Lookup.BranchLookupBusiness"
                                            runat="server" 
                                            Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">تصویر</td>
        <td class="control"><kfk:FileUploader ID="fupBranchPic" runat="server" SaveFolder="~/userfiles/branch/" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

