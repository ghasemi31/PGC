<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_BranchOrderTitle_Search" %>
<legend>جستجو</legend>
<table>
    <tr>        
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" /></td>
    </tr>
    <tr>                  
        <td class="caption">نام گروه</td>                                               
        <td class="control"><kfk:LookupCombo runat="server" ID="lkpGroup" BusinessTypeName="pgc.Business.Lookup.BranchOrderTitleGroupLookupBusiness" AddDefaultItem="true" /></td>
    </tr>
    <tr>
        <td class="caption">مبلغ</td>
        <td class="control"><kfk:NumericRange ID="nmrPrice" runat="server" /></td>
    </tr>
    <tr>                  
        <td class="caption">وضعیت کالا</td>                                                                             
        <td class="control"><kfk:LookupCombo runat="server" ID="lkpStatus" EnumParameterType="pgc.Model.Enums.BranchOrderTitleStatus" AddDefaultItem="true" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش تمامی کالاها" CssClass="" OnClick="OnSearchAll" />
</div>
