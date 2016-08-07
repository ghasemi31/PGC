<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Agent_BranchPic_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">شعبه</td>
        <td class="control">
            <asp:Label ID="lblBranch" runat="server" ></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">تصویر</td>
        <td class="control"><kfk:FileUploader ID="fupBranchPic" runat="server" SaveFolder="~/userfiles/branch/" required="true"/></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

