<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_BranchOrderShipmentState_Search" %>
<legend>جستجو</legend>
<table>    
    <tr>
        <td class="caption">عنوان وضعیت</td>                                                                     
        <td class="control"><kfk:LookupCombo AddDefaultItem="true" ID="lkpState" runat="server" BusinessTypeName="pgc.Business.Lookup.BranchOrderShipmentStateLookupBusiness" /></td>
    </tr>    
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو وضعیت" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش تمامی وضعیت ها" CssClass="xlarge" OnClick="OnSearchAll" />
</div>
