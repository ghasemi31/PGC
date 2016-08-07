<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Agent_BranchOrderNoMoney_Detail" %>
<%@ Import Namespace="kFrameWork.Util" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<input type="hidden" value="" id="orderTitleList" runat="server" clientidmode="Static" />
<table>  
    <tr>
        <td class="caption">کد درخواست</td>
        <td class="control"><%=Order.ID %></td>        
    </tr>
              
    <tr>
        <td class="caption">وضعیت درخواست</td>
        <td class="control"><%=kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.BranchOrderStatus)Order.Status)%></td>

        <%if (Order.ShipmentStatus_ID.HasValue){ %>
            <td class="caption">وضعیت ارسالی</td>
            <td class="control"><%=Order.BranchOrderShipmentState.Title%></td>
        <%} %>
    </tr>    
     <tr> 
        <td class="caption">تاریخ تحویل</td>
        <td class="control"><%=Order.OrderedPersianDate%></td>

        <td class="caption">تاریخ ثبت</td>
        <td class="control"><%=DateUtil.GetPersianDateWithTime(Order.RegDate) %></td>
    </tr>  
    <tr>        
        <td class="caption">توضیحات شعبه</td>
        <td class="control"><%=Order.BranchDescription%></td>
        
        <td class="caption">توضیحات مدیر</td>
        <td class="control"><%=Order.AdminDescription%></td>
    </tr>                       
</table>
<div runat="server" id="detailList" class="detailtbl">            
    <span class="detailistttl">لیست کالاهای سفارشی</span>
</div>

<div class="commands">
    
    <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchOrderNoMoneyDetailExcel.aspx?id="+Order.ID.ToString())%>" target="_blank" class="excelbtn ocommands" >دریافت فایل اکسل نتایج</a>

    <a href="<%=ResolveUrl("~/Pages/Agent/Prints/BranchOrderNoMoneyDetail.aspx?id="+Order.ID.ToString() )%>" target="_blank" class="printbtn ocommands" >چاپ جزئیات</a>        

    <asp:Button runat="server" ID="btnDelete" Text="حذف" CssClass="large scommands" onclick="btnDelete_Click" Visible="false" OnClientClick="if (!confirm('آیا از حذف درخواست، اطمینان دارید؟')){return false;}" />

    <asp:Button runat="server" ID="btnRevise" Text="ویرایش" CssClass="large scommands" onclick="btnSave_Click" Visible="false" />
    
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" OnClick="OnCancel" CausesValidation="false" Width="60" />

</div>


