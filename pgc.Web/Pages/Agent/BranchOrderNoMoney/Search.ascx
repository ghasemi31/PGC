<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Agent_BranchOrderNoMoney_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">کد درخواست</td>
        <td class="control"><kfk:NormalTextBox ID="txtID" Mode="Numeric" runat="server" /></td>
   
        <td class="caption">وضعیت درخواست</td>                                                   
        <td class="control"><kfk:LookupCombo ID="lkpStatus" runat="server" EnumParameterType="pgc.Model.Enums.BranchOrderStatus" AddDefaultItem="true" /></td>
     </tr>   
    <tr>
        <td class="caption">گروه کالا</td>
        <td class="control">
            <kfk:LookupCombo    ID="lkpGroup" 
                                runat="server" 
                                BusinessTypeName="pgc.Business.Lookup.BranchOrderTitleGroupLookupBusiness" 
                                AutoPostBack="true" 
                                DependantControl="lkpOrderTitle" />
        </td>
        <td class="caption">عنوان کالا</td>
        <td class="control">
            <kfk:LookupCombo    ID="lkpOrderTitle" 
                                runat="server" 
                                AddDefaultItem="true"
                                BusinessTypeName="pgc.Business.Lookup.BranchOrderTitleLookupBusiness" 
                                DependOnParameterName="Group_ID" 
                                DependOnParameterType="Int64" 
                                Required="true"/>
        </td>
    </tr>
    <tr>                  
        <%--<td class="caption">مبلغ</td>
        <td class="control"><kfk:NumericRange ID="nmrPrice" runat="server" /></td>
--%>
        <td class="caption">تاریخ تحویل</td>
        <td class="control"><kfk:PersianDateRange ID="pdrRegDate" runat="server" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش تمامی سفارشات" CssClass="" OnClick="OnSearchAll" />
</div>
