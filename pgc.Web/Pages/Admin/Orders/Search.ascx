<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_GameOrder_Search" %>
<legend>جستجو</legend>
<table>   
    <tr>
        <td class="caption">کد ثبت نام</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtNumber" Mode="Numeric" CssClass="small" /></td>

        <td class="caption">نام ثبت نام کننده</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtUser" /></td>
    </tr>
    <tr>
        <td class="caption">تاریخ ثبت نام</td>
        <td class="control"><kfk:PersianDateRange runat="server" ID="pdrGameOrderPersianDate" /></td>

        <td class="caption">مبلغ </td>
        <td class="control"><kfk:NumericRange ID="nrAmount" runat="server" /></td>
    </tr>
 
    <tr>
        <td class="caption">وضعیت پرداخت</td>
        <td class="control"><kfk:LookupCombo ID="lkcPaymentStatus" 
                                                runat="server" 
                                                EnumParameterType="pgc.Model.Enums.GameOrderPaymentStatus"
                                                AddDefaultItem="true"                                                                                                
                                                CssClass="large" /></td>
    
        <td class="caption">بازی</td>
        <td class="control"><kfk:LookupCombo ID="lkcGame" 
                                            runat="server" 
                                            BusinessTypeName="pgc.Business.Lookup.GameLookupBusiness"
                                            AddDefaultItem="true"
                                            CssClass="large"/></td>
    </tr>
    <%--<tr>
        <td class="caption">رسید دیجیتالی</td>
        <td class="control"><kfk:NormalTextBox ID="txtRefNum" runat="server" CssClass="large" /></td>
        
        
    </tr>--%>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />    
</div>
