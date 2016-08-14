<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_GameCenter_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">عنوان / توضیحات</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtDesc" />
        </td>
        
    </tr>
    <tr>
       <td class="caption">استان</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcProvince"
                runat="server"
                BusinessTypeName="pgc.Business.Lookup.ProvinceLookupBusiness"
                AutoPostBack="true"
                DependantControl="lkcCity"
                Required="true" />
        </td>
        <td class="caption">شهرستان</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcCity"
                runat="server"
                BusinessTypeName="pgc.Business.Lookup.CityLookupBusiness"
                DependOnParameterName="Province_ID"
                DependOnParameterType="Int64"
                Required="true" />
        </td>
    </tr>
   
     
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
