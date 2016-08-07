<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchOrderTitle_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>        
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" TextBoxWidth="158" Required="true" /></td>
    </tr>
    <tr>                  
        <td class="caption">نام گروه</td>                                               
        <td class="control"><kfk:LookupCombo runat="server" ID="lkpGroup" BusinessTypeName="pgc.Business.Lookup.BranchOrderTitleGroupLookupBusiness" /></td>
    </tr>
    <tr>        
        <td class="caption">مبلغ</td>
        <td class="control"><kfk:NumericTextBox ID="nmrPrice" runat="server" TextBoxWidth="158" Required="true" SupportComma="true" UnitText="ریال" /></td>
    </tr>
    <tr>        
        <td class="caption">اولویت نمایش</td>
        <td class="control"><kfk:NormalTextBox ID="txtDispOrder" runat="server" TextBoxWidth="158" Required="true"/></td>
    </tr>
    <tr>        
        <td class="caption">وضعیت</td>
        <td class="control"><kfk:LookupCombo ID="lkpStatus" runat="server" CssClass="state" EnumParameterType="pgc.Model.Enums.BranchOrderTitleStatus" /></td>
    </tr>
    <tr class="all-branch">
        <td class="caption">قابل سفارش برای تمامی شعب</td>
        <td class="control"><asp:CheckBox ID="chbAllBranches" runat="server" /></td>
    </tr>
    <tr class="min-count" style="visibility:hidden">
        <td class="caption">حداقل تعداد سفارش</td>
        <td class="control"><kfk:NormalTextBox ID="txtMinimumCount" runat="server" TextBoxWidth="158" Text="" CssClass="checkbox"/></td>
    </tr>
    <tr>
        <td class="caption">تصویر</td>
        <td class="control"><kfk:FileUploader ID="fupProductPic" runat="server" SaveFolder="~/userfiles/BranchOrder/" /></td>
    </tr>

</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

