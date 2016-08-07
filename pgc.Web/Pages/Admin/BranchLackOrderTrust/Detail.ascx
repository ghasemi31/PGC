<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchLackOrderTrust_Detail" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="kFrameWork.Util" %>
<%@ Import Namespace="pgc.Business" %>

<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
     <tr>
        <td class="caption">تاریخ کسری</td>
        <td class="control"><%=Util.GetPersianDateWithTime(LackOrder.RegDate) %></td>

        <td class="caption">شعبه</td>
        <td class="control"><%=LackOrder.BranchOrder.Branch.Title%></td>        
    </tr>
    <tr>
        <td class="caption">کد کسری</td>
        <td class="control"><%=LackOrder.ID %></td>

        <td class="caption">کد درخواست</td>
        <td class="control"><%=LackOrder.BranchOrder_ID %></td>
    </tr>
    <tr>
        <td class="caption">وضعیت کسری</td>
        <td class="control"><%=EnumUtil.GetEnumElementPersianTitle((BranchLackOrderStatus)LackOrder.Status) %></td>

        <td class="caption">وضعیت درخواست</td>
        <td class="control"><%=((BranchOrderStatus)LackOrder.BranchOrder.Status==BranchOrderStatus.Finalized)?"<span style='font-weight:bold;'>بسته شده</span>": EnumUtil.GetEnumElementPersianTitle((BranchOrderStatus)LackOrder.BranchOrder.Status) %></td>
    </tr>                                              
    <tr>        
        <td class="caption">توضیحات شعبه</td>
        <td class="control"><%=LackOrder.BranchDescription %></td>
        
        <td class="caption">توضیحات مدیر</td>
        <td class="control"><%=LackOrder.AdminDescription %></td>
    </tr>                       
</table>
<div runat="server" id="detailList" class="detailtbl">            
    <span class="detailistttl">لیست کسورات کالاها</span>
</div>
<div class="commands">

    <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchLackOrderDetailExcel.aspx?id="+LackOrder.ID.ToString())%>" target="_blank" class="excelbtn ocommands" >دریافت فایل اکسل نتایج</a>

    <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchLackOrderDetail.aspx?id="+LackOrder.ID.ToString() )%>" target="_blank" class="printbtn" >چاپ جزئیات</a>

    <asp:Button runat="server" ID="btnSaveDescs" Text="حذف" CssClass="large scommands" OnClick="OnDelete" OnClientClick="if (!confirm('درخواست بعد از حذف بطور کامل از سیستم حذف شده و قابلیت برگشت ندارد، همچنین فاکتور مالی مربوطه هم از بین می رود، آیا مطمئنید؟')){return false;}" />
    
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" Width="60" />
</div>

