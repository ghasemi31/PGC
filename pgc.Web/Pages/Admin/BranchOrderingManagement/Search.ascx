<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_BranchOrderingManagement_Search" %>
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
        <td class="caption">گروه کالای سفارشی</td>
        <td class="control">
                <kfk:LookupCombo    ID="lkpGroup" 
                                    runat="server" 
                                    BusinessTypeName="pgc.Business.Lookup.BranchOrderTitleGroupLookupBusiness" 
                                    AutoPostBack="true" 
                                    AddDefaultItem="false"
                                    DependantControl="lkpOrderTitle" />
            </td>
    </tr>
    <tr>
            <td class="caption">عنوان کالا</td>
            <td class="control">
                <kfk:LookupCombo    ID="lkpOrderTitle" 
                                    runat="server" 
                                    BusinessTypeName="pgc.Business.Lookup.BranchOrderTitleLookupBusiness" 
                                    AddDefaultItem="true"
                                    DependOnParameterName="Group_ID" 
                                    DependOnParameterType="Int64" />
            </td>
    </tr>
   
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش تمامی شعب" CssClass="" OnClick="OnSearchAll" />
</div>
