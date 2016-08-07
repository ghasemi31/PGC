<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Agent_BranchLackOrderNoMoney_Detail" %>
<%@ Import Namespace="kFrameWork.Util" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="pgc.Model.Enums" %>


<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<input type="hidden" value="" id="orderTitleList" runat="server" clientidmode="Static" />
<table> 
    <tr>
        <td class="caption">تاریخ</td>
        <td class="control"><%=Util.GetPersianDateWithTime(LackOrder.RegDate)%></td>
    </tr> 
    <tr>
        <td class="caption">کد کسری</td>
        <td class="control"><%=LackOrder.ID %></td>

        <td class="caption">کد درخواست</td>
        <td class="control"><%=LackOrder.BranchOrder_ID %></td>
    </tr>             
    <tr> 
        <td class="caption">وضعیت کسری</td>
        <td class="control"><%=kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.BranchLackOrderStatus)LackOrder.Status) %></td>
        
        <td class="caption">وضعیت درخواست</td>
        <td class="control"><%=((BranchOrderStatus)LackOrder.BranchOrder.Status==BranchOrderStatus.Finalized)?"<span style='font-weight:bold;'>بسته شده</span>": EnumUtil.GetEnumElementPersianTitle((BranchOrderStatus)LackOrder.BranchOrder.Status) %></td>       
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
    
    <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchLackOrderNoMoneyDetailExcel.aspx?id="+LackOrder.ID.ToString())%>" target="_blank" class="excelbtn ocommands" >دریافت فایل اکسل نتایج</a>

    <a href="<%=ResolveUrl("~/Pages/Agent/Prints/BranchLackOrderNoMoneyDetail.aspx?id="+LackOrder.ID.ToString() )%>" target="_blank" class="printbtn ocommands" >چاپ جزئیات</a>        

    <asp:Button runat="server" ID="btnRevise" Text="ویرایش" CssClass="large scommands" onclick="btnSave_Click" Visible="false" />

    <asp:Button runat="server" ID="btnDelete" Text="حذف" CssClass="large scommands" onclick="btnDelete_Click" Visible="false" OnClientClick="if (!confirm('آیا از حذف کسری، اطمینان دارید؟')){return false;}" />
    
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" OnClick="OnCancel" CausesValidation="false" />

</div>

