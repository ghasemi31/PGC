<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchLackOrder_Detail" %>
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
        <td class="control"><kfk:NormalTextBox ID="txtBranchDesc" runat="server" TextMode="MultiLine" /></td>
        
        <td class="caption">توضیحات مدیر</td>
        <td class="control"><kfk:NormalTextBox ID="txtAdminDesc" runat="server" TextMode="MultiLine" /></td>
    </tr>                       
</table>
<div runat="server" id="detailList" class="detailtbl">            
    <span class="detailistttl">لیست کسورات کالاها</span>
</div>
<div class="commands">
    <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchLackOrderDetailExcel.aspx?id="+LackOrder.ID.ToString())%>" target="_blank" class="excelbtn ocommands" >دریافت فایل اکسل نتایج</a>

    <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchLackOrderDetail.aspx?id="+LackOrder.ID.ToString() )%>" target="_blank" class="printbtn ocommands" >چاپ جزئیات</a>

    <asp:Button runat="server" ID="btnSave" Text="ذخیره توضیحات" CssClass="large ocommands" OnClick="OnSave" CausesValidation="false" Width="90" />
    
    
    <%switch ((BranchLackOrderStatus)LackOrder.Status)
      {
          case BranchLackOrderStatus.Pending:
                if (LackOrder.BranchOrder.Status==(int)BranchOrderStatus.Confirmed){%>

                    <asp:Button runat="server" ID="Button3" Text="ابطال" CssClass="large scommands" CausesValidation="true" OnClick="Cancelation_Click" OnClientClick="if (!confirm('آیا از ابطال کسری، اطمینان دارید؟')){return false;}" />
                    <asp:Button runat="server" ID="Button2" Text="اعمال اصلاحیه" CssClass="large scommands" OnClick="GoCorrection_Click" />
                    <asp:Button runat="server" ID="Button1" Text="تایید" CssClass="large scommands" CausesValidation="true" OnClick="Confirmation_Click" />

              <%}break;
          case BranchLackOrderStatus.Confirmed:
                if (LackOrder.BranchOrder.Status==(int)BranchOrderStatus.Confirmed){%>
                    
                    <asp:Button runat="server" ID="Button4" Text="ابطال" CssClass="large scommands" CausesValidation="true" OnClick="Cancelation_Click" OnClientClick="if (!confirm('آیا از ابطال کسری، اطمینان دارید؟')){return false;}" />

              <%}break;
          case BranchLackOrderStatus.Canceled:
                if (LackOrder.BranchOrder.Status==(int)BranchOrderStatus.Confirmed){%>

                <asp:Button runat="server" ID="Button6" Text="اعمال اصلاحیه" CssClass="large scommands" CausesValidation="true" onclick="GoCorrection_Click" />

              <%}break;
          //case BranchLackOrderStatus.Finalized:
          //    break;
          default:
              break;
      } %>
                                                                                
    <asp:Button runat="server" ID="btnCancel" Text="بازگشت" CssClass="large" OnClick="OnCancel" CausesValidation="false" Width="60" />
</div>

