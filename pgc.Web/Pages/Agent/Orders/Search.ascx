<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Agent_Order_Search" %>
<legend>جستجو</legend>
<table>
    <tr>
        <td class="caption">کد اشتراک /کد سفارش</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtNumber" Mode="Numeric" CssClass="small" /></td>

        <td class="caption">نام سفارش دهنده</td>
        <td class="control"><kfk:NormalTextBox runat="server" ID="txtUser" /></td>
    </tr>
    <tr>
        <td class="caption">تاریخ سفارش</td>
        <td class="control"><kfk:PersianDateRange runat="server" ID="pdrOrderPersianDate" /></td>

        <td class="caption">مبلغ سفارش</td>
        <td class="control"><kfk:NumericRange ID="nrAmount" runat="server" /></td>
    </tr>
    <tr>
        <td class="caption">وضعیت سفارش</td>
        <td class="control"><kfk:LookupCombo ID="lkcStatus" 
                                    runat="server" 
                                    EnumParameterType="pgc.Model.Enums.OrderStatus"
                                    AddDefaultItem="true"
                                    CssClass="large"/></td>

        <td class="caption">محصولات</td>
        <td class="control"><kfk:LookupCombo ID="lkcProduct" 
                                            runat="server" 
                                            BusinessTypeName="pgc.Business.Lookup.ProductLookupBusiness"
                                            AddDefaultItem="true"
                                            CssClass="large"/></td>
    </tr>   
    <tr>
        <td class="caption">وضعیت پرداخت</td>
        <td class="control"><kfk:LookupCombo ID="lkcPaymentStatus" 
                                                runat="server" 
                                                EnumParameterType="pgc.Model.Enums.OrderPaymentStatus"
                                                AddDefaultItem="true"                                                                                                
                                                CssClass="large" /></td>    
       
        <td class="caption">رسید دیجیتالی</td>
        <td class="control"><kfk:NormalTextBox ID="txtRefNum" runat="server" CssClass="large" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="" OnClick="OnSearchAll" />
</div>