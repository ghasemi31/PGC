<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_BranchOrderedTitleNoMoney_Search" %>
<legend>جستجو</legend>
<table>
    <tr>        
        <td class="caption">شعبه</td>                                                                                                  
        <td class="control"><kfk:LookupCombo ID="lkpBranch" runat="server" BusinessTypeName="pgc.Business.Lookup.BranchLookupBusiness" AddDefaultItem="true" /></td>

        <td class="caption">وضعیت کالا</td>                                                     
        <td class="control"><kfk:LookupCombo ID="lkpStatus" runat="server" EnumParameterType="pgc.Model.Enums.BranchOrderedTitleStatus" /></td>
    </tr>
    <tr>
        <td class="caption">گروه کالای سفارشی</td>
        <td class="control">
                <kfk:LookupCombo    ID="lkpGroup" 
                                    runat="server" 
                                    BusinessTypeName="pgc.Business.Lookup.BranchOrderTitleGroupLookupBusiness" 
                                    AddDefaultItem="true" />
        </td>
        
        <td class="caption">تاریخ تحویل کالا</td>                                                                             
        <td class="control"><kfk:PersianDateRange ID="pdrDeliverDate" runat="server" /></td>
        
    </tr>
    <tr>
        <td></td>
        <td></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش تمامی کالاها" CssClass="" OnClick="OnSearchAll" />
</div>
