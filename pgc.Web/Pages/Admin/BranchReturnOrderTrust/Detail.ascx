<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchReturnOrderTrust_Detail" %>
<%@ Import Namespace="kFrameWork.Util" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="pgc.Model.Enums" %>

<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<input type="hidden" value="" id="orderTitleList" runat="server" clientidmode="Static" />
<table> 
    <tr>
        <td class="caption">شماره مرجوعی</td>
        <td class="control"><%=ReturnOrder.ID %></td> 
        
        <td class="caption">شعبه</td>
        <td class="control"><%=ReturnOrder.Branch.Title%></td>               
    </tr>   
    <tr>
        <td class="caption">تاریخ مرجوعی</td>
        <td class="control"><%=Util.GetPersianDateWithTime(ReturnOrder.RegDate) %></td>

        <td class="caption">وضعیت مرجوعی</td>
        <td class="control"><%=EnumUtil.GetEnumElementPersianTitle((BranchReturnOrderStatus)ReturnOrder.Status) %></td>
    </tr>
    <tr>        
        <td class="caption">توضیحات شعبه</td>
        <%if (ReturnOrder.Status!=(int)pgc.Model.Enums.BranchReturnOrderStatus.Confirmed ){ %>       
            <td class="control"><kfk:NormalTextBox ID="txtBranchDesc" runat="server" TextMode="MultiLine" /></td>
        <%}else{ %>
            <td class="control"><%=ReturnOrder.BranchDescription %></td>
        <%} %>
        
        <td class="caption">توضیحات مدیر</td>
        <%if (ReturnOrder.Status!=(int)pgc.Model.Enums.BranchReturnOrderStatus.Confirmed ){ %>       
            <td class="control"><kfk:NormalTextBox ID="txtAdminDesc" runat="server" TextMode="MultiLine" /></td>
        <%}else{ %>
            <td class="control"><%=ReturnOrder.AdminDescription %></td>
        <%} %>
    </tr>                       
</table>
<div runat="server" id="detailList" class="detailtbl">            
    <span class="detailistttl">لیست کالاهای مرجوعی</span>
</div>
<div class="commands">

    <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchReturnOrderDetailExcel.aspx?id="+ReturnOrder.ID.ToString())%>" target="_blank" class="excelbtn ocommands" >دریافت فایل اکسل نتایج</a>

    <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchReturnOrderDetail.aspx?id="+ReturnOrder.ID.ToString() )%>" target="_blank" class="printbtn ocommands" >چاپ جزئیات</a>
    
    <%--<asp:Button runat="server" ID="btnGetCorrect" Text="اعمال اصلاحات" CssClass="large scommands" OnClick="CorrectionComplete_Click" CausesValidation="true" />--%>
    
    <asp:Button runat="server" ID="btnSaveDescs" Text="حذف" CssClass="large scommands" OnClick="OnDelete" OnClientClick="if (!confirm('درخواست بعد از حذف بطور کامل از سیستم حذف شده و قابلیت برگشت ندارد، همچنین فاکتور مالی مربوطه هم از بین می رود، آیا مطمئنید؟')){return false;}" />

    <%switch ((pgc.Model.Enums.BranchReturnOrderStatus)ReturnOrder.Status)
      {
          case pgc.Model.Enums.BranchReturnOrderStatus.Pending:
          case pgc.Model.Enums.BranchReturnOrderStatus.Canceled:              
          case pgc.Model.Enums.BranchReturnOrderStatus.Confirmed:
              break;
          case pgc.Model.Enums.BranchReturnOrderStatus.Finalized:%>
                
                <asp:Button runat="server" ID="Button5" Text="به جریان انداختن مرجوعی" CssClass="xlarge scommands" onclick="GoToConfirmation_Click" />

              <%break;
          default:
              break;
      } %>
    
    <asp:Button runat="server" ID="btnCancel" Text="بازگشت" CssClass="large" OnClick="OnCancel" CausesValidation="false" Width="60" />

</div>

