<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_OccuredSystemEvent_Search" %>
<legend>جستجو</legend>
<table>
   <tr>
      <%--  <td class="caption">رخداد مربوطه</td>
        <td class="control"><kfk:Lookup     ID="lkpEvent"
                                            runat="server"
                                            URL="~/Pages/Admin/SystemEvent/Lookup.aspx" 
                                            TitleCellIndex="2"
                                            SupportSearch="true" />
        </td>--%>

        <td class="caption">تاریخ رخداد</td>
        <td class="control"><kfk:PersianDateRange ID="pdrOccuredDate" runat="server" /></td>        
    </tr>  
    <tr>
        <td class="caption">نوع اقدام</td>
        <td class="control"><kfk:LookupCombo ID="lkpActionType" runat="server" AddDefaultItem="true" EnumParameterType="pgc.Model.Enums.EventActionType" /></td>

        <td></td>
        <td></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش تمامی رخدادها" CssClass="" OnClick="OnSearchAll" />
</div>
