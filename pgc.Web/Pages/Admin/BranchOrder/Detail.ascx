<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchOrder_Detail" %>
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
            <td class="caption">وضعیت ارسالی درخواست</td>                                       
            <td class="control"><kfk:LookupCombo ID="lkpShipment" runat="server" BusinessTypeName="pgc.Business.Lookup.BranchOrderShipmentStateLookupBusiness" AddDefaultItem="true" DefaultItemText="---" /></td>
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
        <td class="control"><kfk:NormalTextBox ID="txtBranchDesc" runat="server" TextMode="MultiLine" /></td>
        
        <td class="caption">توضیحات مدیر</td>
        <td class="control"><kfk:NormalTextBox ID="txtAdminDesc" runat="server" TextMode="MultiLine" /></td>
    </tr>                       
</table>
<div runat="server" id="detailList" class="detailtbl">            
    <span class="detailistttl">لیست کالاهای سفارشی</span>
</div>
<div class="commands">
    <%switch ((BranchOrderStatus)Order.Status)
      {
          case pgc.Model.Enums.BranchOrderStatus.Pending:%>

                <asp:Button runat="server" ID="Button3" Text="ابطال" CssClass="scommands" CausesValidation="true" OnClick="Cancelation_Click" OnClientClick="if (!confirm('آیا از ابطال درخواست، اطمینان دارید؟')){return false;}"/>
                <asp:Button runat="server" ID="Button2" Text="اعمال اصلاحیه" CssClass="scommands" OnClick="GoCorrection_Click" />
                <asp:Button runat="server" ID="Button1" Text="تایید" CssClass="scommands" CausesValidation="true" OnClick="Confirmation_Click" />

              <%break;
          case pgc.Model.Enums.BranchOrderStatus.Confirmed:%>

                <asp:Button runat="server" ID="Button4" Text="ابطال" CssClass="scommands" CausesValidation="true" OnClick="Cancelation_Click" OnClientClick="if (!confirm('آیا از ابطال درخواست، اطمینان دارید؟')){return false;}" />
                <asp:Button runat="server" ID="Button7" Text="بستن" CssClass="scommands" CausesValidation="true" onclick="Finalize_Click" OnClientClick="if (!confirm('بعد از نهایی نمودن درخواست امکان هیچ گونه عملیاتی بر روی درخواست و کسری مربوطه مقدور نمی باشد. آیا اطمینان به نهایی نمودن درخواست دارید؟')){return false;}" />

              <%break;
          case pgc.Model.Enums.BranchOrderStatus.Canceled:%>

                <asp:Button runat="server" ID="Button6" Text="اعمال اصلاحیه" CssClass="scommands" CausesValidation="true" onclick="GoCorrection_Click" />

              <%break;
          case pgc.Model.Enums.BranchOrderStatus.Finalized:
              break;
          default:
              break;
      } %>

    <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchOrderDetailExcel.aspx?id="+Order.ID.ToString())%>" target="_blank" class="excelbtn ocommands" >دریافت فایل اکسل نتایج</a>

    <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchOrderDetail.aspx?id="+Order.ID.ToString() )%>" target="_blank" class="printbtn ocommands" >چاپ جزئیات</a>

    <asp:Button runat="server" ID="btnSave" Text="ذخیره توضیح و وضعیت ارسال" Width="160px" OnClick="btnShipment_Click" CausesValidation="true" CssClass="ocommands"/>
    
    <asp:Button runat="server" ID="btnCancel" Text="بازگشت" OnClick="OnCancel" CausesValidation="false" Width="50" />
</div>

