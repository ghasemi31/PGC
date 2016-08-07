<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_AccessLevel_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">نقش</td>
        <td class="control"><kfk:LookupCombo ID="lkcRole" 
                                            runat="server" 
                                            EnumParameterType="pgc.Model.Enums.Role"
                                            EnumParameterTypeAssembly="pgc.Model"
                                            AddDefaultItem="true" /></td>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
