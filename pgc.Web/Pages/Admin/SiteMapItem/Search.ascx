<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_SiteMapItem_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">دسته بندی</td>
        <td class="control"><kfk:LookupCombo ID="lkpSiteMapCat" 
                                            runat="server" 
                                            BusinessTypeName="pgc.Business.Lookup.SiteMapCatLookupBusiness"
                                            AddDefaultItem="true" /></td>
    </tr>
    <tr>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
