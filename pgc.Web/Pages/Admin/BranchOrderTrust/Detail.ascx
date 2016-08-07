<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchOrderTrust_Detail" %>
<%@ Import Namespace="kFrameWork.Util" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Business" %>

<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>        
        
        <td class="caption">کد درخواست</td>
        <td class="control"><%= Order.ID %></td>

        <td class="caption">شعبه</td>
        <td class="control"><%= Order.Branch.Title%></td>
    </tr>
    
    <tr>
        <td class="caption">وضعیت درخواست</td>
        <td class="control"><%=kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.BranchLackOrderStatus)Order.Status) %></td>
        
        <% if (Order.Status==(int)BranchOrderStatus.Confirmed ||Order.Status==(int)BranchOrderStatus.Finalized){ %>
            <td class="caption">وضعیت ارسالی</td>                                       
            <td class="control"><%=(Order.ShipmentStatus_ID.HasValue)? Order.BranchOrderShipmentState.Title: "----" %></td>
        <%} %>
    </tr>
        <tr>
        <td class="caption">تاریخ تحویل</td>
        <td class="control"><%=DateUtil.GetPersianDateWithTime(DateUtil.GetEnglishDateTime(Order.OrderedPersianDate)) %></td>

        <td class="caption">تاریخ ثبت</td>
        <td class="control"><%=Util.GetPersianDateWithTime(Order.RegDate) %></td>
    </tr>
    <tr>        
        <td class="caption">توضیحات شعبه</td>
        <td class="control"><%=Order.BranchDescription %></td>
        
        <td class="caption">توضیحات مدیر</td>
        <td class="control"><%=Order.AdminDescription %></td>
    </tr>                       
</table>
<div runat="server" id="detailList" class="detailtbl">            
    <span class="detailistttl">لیست کالاهای سفارشی</span>
</div>
<div class="commands">
    <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchOrderDetailExcel.aspx?id="+Order.ID.ToString())%>" target="_blank" class="excelbtn ocommands" >دریافت فایل اکسل نتایج</a>

    <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchOrderDetail.aspx?id="+Order.ID.ToString() )%>" target="_blank" class="printbtn ocommands" >چاپ جزئیات</a>

    <asp:Button runat="server" ID="btnSaveDescs" Text="حذف" CssClass="large scommands" OnClick="OnDelete" OnClientClick="if (!confirm('درخواست بعد از حذف بطور کامل از سیستم حذف شده و قابلیت برگشت ندارد، همچنین فاکتور مالی مربوطه هم از بین می رود، آیا مطمئنید؟')){return false;}" />

    <%switch ((BranchOrderStatus)Order.Status)
      {
          case pgc.Model.Enums.BranchOrderStatus.Pending:
          case pgc.Model.Enums.BranchOrderStatus.Canceled:
          case pgc.Model.Enums.BranchOrderStatus.Confirmed:
              break;
          case pgc.Model.Enums.BranchOrderStatus.Finalized:%>

                <asp:Button runat="server" ID="Button5" Text="به جریان انداختن درخواست" CssClass="xlarge scommands" onclick="GoToConfirmation_Click" />

              <%break;
          default:
              break;
      } %>
    
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" OnClick="OnCancel" CausesValidation="false" />
</div>

