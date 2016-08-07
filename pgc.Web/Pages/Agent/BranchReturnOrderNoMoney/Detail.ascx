<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Agent_BranchReturnOrderNoMoney_Detail" %>
<%@ Import Namespace="kFrameWork.Util" %>
<%@ Import Namespace="pgc.Business" %>

<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<input type="hidden" value="" id="orderTitleList" runat="server" clientidmode="Static" />
<table>  
    <tr>
        <td class="caption">کد مرجوعی</td>
        <td class="control"><%=ReturnOrder.ID %></td>
    </tr>             
    <tr> 
        <td class="caption">وضعیت مرجوعی</td>
        <td class="control"><%=kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.BranchReturnOrderStatus)ReturnOrder.Status)%></td>

        <td class="caption">تاریخ</td>
        <td class="control"><%=Util.GetPersianDateWithTime(ReturnOrder.RegDate) %></td>
    </tr>
    <tr>        
        <td class="caption">توضیحات شعبه</td>
        <td class="control"><%=ReturnOrder.BranchDescription%></td>
        
        <td class="caption">توضیحات مدیر</td>
        <td class="control"><%=ReturnOrder.AdminDescription%></td>
    </tr>                       
</table>
<div runat="server" id="detailList" class="detailtbl">            
    <span class="detailistttl">لیست کالاهای مرجوعی</span>
</div>

<div class="commands">
    
    <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchReturnOrderNoMoneyDetailExcel.aspx?id="+ReturnOrder.ID.ToString())%>" target="_blank" class="excelbtn ocommands" >دریافت فایل اکسل نتایج</a>

    <a href="<%=ResolveUrl("~/Pages/Agent/Prints/BranchReturnOrderNoMoneyDetail.aspx?id="+ReturnOrder.ID.ToString() )%>" target="_blank" class="printbtn ocommands" >چاپ جزئیات</a>        

    <asp:Button runat="server" ID="btnRevise" Text="ویرایش" CssClass="large scommands" onclick="btnSave_Click" Visible="false" />
    
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" Width="60" />

</div>

