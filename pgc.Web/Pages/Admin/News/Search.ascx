<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_News_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">عنوان و توضیحات</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" /></td>

        <%if (kFrameWork.UI.UserSession.User.AccessLevel.Features.Any(f => f.PanelPages.Any(g => g.RouteName.ToLower().Contains("admin-confirmnews"))))
          { %>
            <td class="caption">وضعیت خبر</td>
            <td class="control"><kfk:LookupCombo AddDefaultItem="true" ID="lkpStatus" runat="server" EnumParameterType="pgc.Model.Enums.NewsStatus" /></td>
        <%} %>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
