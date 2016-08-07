<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_BranchCredit_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">شعبه</td>
        <td class="control"><kfk:LookupCombo ID="lkpBranch" 
                                            runat="server" 
                                            BusinessTypeName="pgc.Business.Lookup.BranchLookupBusiness"
                                            AddDefaultItem="true" /></td>
    </tr>
    <tr>
        <td class="caption">وضعیت</td>                                                                             
        <td class="control"><kfk:LookupCombo ID="lkpStatus" runat="server" AddDefaultItem="true" EnumParameterType="pgc.Model.Enums.BranchCreditStatus" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش تمامی شعب" CssClass="" OnClick="OnSearchAll" />
</div>
