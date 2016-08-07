<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_UserComment_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">نام / موضوع / ایمیل</td>
        <td class="control"><kfk:NormalTextBox ID="txtName" runat="server" /></td>
    </tr>
    <tr>
        <td class="caption">عنوان شعبه مورد نظر</td>
        <td class="control"><kfk:NormalTextBox ID="txtBranchTitle" runat="server" /></td>
    </tr>
    <tr>
        <td class="caption">شعبه مورد نظر</td>
        <td class="control"><kfk:LookupCombo ID="lkcBranch" runat="server"
                                             BusinessTypeName="pgc.Business.Lookup.BranchLookupBusiness" 
                                             AddDefaultItem="true"
                                             DefaultItemText="مدیریت"/>
         </td>
    </tr>
    <tr>
        <td class="caption">موضوع</td>
        <td class="control"><kfk:LookupCombo ID="lkcType" runat="server" EnumParameterType="pgc.Model.Enums.UserCommentType" AddDefaultItem="true"/></td>
    </tr>
    <tr>
        <td class="caption">وضعیت</td>
        <td class="control"><kfk:LookupCombo ID="lkcStatus" runat="server" EnumParameterType="pgc.Model.Enums.UserCommentStatus" AddDefaultItem="true" /></td>
    </tr>
    <tr>
        <td class="caption">تاریخ</td>
        <td class="control"><kfk:PersianDateRange ID="pdrUCPersianDate" runat="server" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
