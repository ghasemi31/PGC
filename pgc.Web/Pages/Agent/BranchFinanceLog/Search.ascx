<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Agent_BranchFinanceLog_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" /></td>
    </tr>                                                                             
    <tr>
        <td class="caption">نوع تغییر</td>
        <td class="control"><kfk:LookupCombo ID="lkpAction" 
                                            runat="server"                          
                                            EnumParameterType="pgc.Model.Enums.BranchFinanceLogActionType"
                                            AddDefaultItem="true" /></td>

        <td class="caption">تاریخ</td>
        <td class="control"><kfk:PersianDateRange ID="pdrRegDate" runat="server" /></td>
    </tr>
    <tr>                                                                                                          
        <td class="caption">نوع فاکتور</td>                                                                    
        <td class="control"><kfk:LookupCombo ID="lkpLogType" runat="server" EnumParameterType="pgc.Model.Enums.BranchFinanceLogType" AddDefaultItem="true" /></td>

        <td class="caption">کد فاکتور <br /><span>(درخواست،کسری،مرجوی)</span></td>                                   
        <td class="control"><kfk:NormalTextBox Mode="Numeric" ID="txtLogTypeID" runat="server" /></td>
    </tr>       
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش تمامی تغییرات" CssClass="" OnClick="OnSearchAll" />
</div>
