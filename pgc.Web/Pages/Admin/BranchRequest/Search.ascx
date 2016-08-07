<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_BranchRequest_Search" %>
<legend>جستجو</legend>
<table>

    <tr>
        <td class="caption">تاریخ عضویت</td>
        <td class="control"><kfk:PersianDateRange runat="server" ID="pdrBRPersianDate"/></td>

    </tr>
    <tr>
        <td class="caption">وضعیت</td>
        <td class="control"><kfk:LookupCombo ID="lkcStatus" 
                                    runat="server" 
                                    EnumParameterType="pgc.Model.Enums.UserCommentStatus"
                                    AddDefaultItem="true"/></td>

    </tr>
    <tr>
        <td class="caption">اطلاعات هویتی</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtName"/></td>
    </tr>
    <tr>
        <td class="caption">اطلاعات تماس</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtContact"/></td>
    </tr>

    <tr>
        <td class="caption">توضیحات</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtDesc"/></td>

    </tr>

</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>
