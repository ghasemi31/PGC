<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchReturnOrderNoMoney_Detail" %>
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
        <td class="control"><kfk:NormalTextBox ID="txtBranchDesc" runat="server" TextMode="MultiLine" /></td>
        
        <td class="caption">توضیحات مدیر</td>
        <td class="control"><kfk:NormalTextBox ID="txtAdminDesc" runat="server" TextMode="MultiLine" /></td>
    </tr>                       
</table>
<div runat="server" id="detailList" class="detailtbl">            
    <span class="detailistttl">لیست کالاهای مرجوعی</span>
</div>
<div class="commands">

    <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchReturnOrderNoMoneyDetailExcel.aspx?id="+ReturnOrder.ID.ToString())%>" target="_blank" class="excelbtn ocommands" >دریافت فایل اکسل نتایج</a>

    <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchReturnOrderNoMoneyDetail.aspx?id="+ReturnOrder.ID.ToString() )%>" target="_blank" class="printbtn ocommands" >چاپ جزئیات</a>

    <asp:Button runat="server" ID="btnSaveDescs" Text="ذخیره توضیحات" CssClass="large ocommands" OnClick="OnSave" Width="90" />

    <%--<asp:Button runat="server" ID="btnGetCorrect" Text="اعمال اصلاحات" CssClass="large scommands" OnClick="CorrectionComplete_Click" CausesValidation="true" />--%>

    <%switch ((pgc.Model.Enums.BranchReturnOrderStatus)ReturnOrder.Status)
      {
          case pgc.Model.Enums.BranchReturnOrderStatus.Pending:%>

                <asp:Button runat="server" ID="Button2" Text="ابطال" CssClass="large scommands" CausesValidation="true" onclick="Cancelation_Click" OnClientClick="if (!confirm('آیا از ابطال مرجوعی، اطمینان دارید؟')){return false;}" />
                <asp:Button runat="server" ID="Button7" Text="اعمال اصلاحیه" CssClass="large scommands" CausesValidation="true" onclick="GoCorrection_Click" />
                <asp:Button runat="server" ID="Button1" Text="تایید" CssClass="large scommands" CausesValidation="true" onclick="Confirmation_Click" />
          
              <%break;
          case pgc.Model.Enums.BranchReturnOrderStatus.Confirmed:%>
                
                <asp:Button runat="server" ID="Button4" Text="ابطال" CssClass="large scommands" CausesValidation="true" onclick="Cancelation_Click" OnClientClick="if (!confirm('آیا از ابطال مرجوعی، اطمینان دارید؟')){return false;}" />
                <asp:Button runat="server" ID="Button3" Text="بستن" CssClass="large scommands" CausesValidation="true" onclick="Finalize_Click" OnClientClick="if (!confirm('بعد از بسته شدن مرجوعی امکان هیچ گونه عملیاتی مقدور نمی باشد. آیا اطمینان به بسته شدن مرجوعی دارید؟')){return false;}" />

              <%break;
          case pgc.Model.Enums.BranchReturnOrderStatus.Canceled:%>

                <asp:Button runat="server" ID="Button5" Text="اعمال اصلاحیه" CssClass="large scommands" CausesValidation="true" onclick="GoCorrection_Click" />

              <%break;
          case pgc.Model.Enums.BranchReturnOrderStatus.Finalized:
              break;
          default:
              break;
      } %>
    

    <asp:Button runat="server" ID="btnCancel" Text="بازگشت" CssClass="large" OnClick="OnCancel" CausesValidation="false" Width="60" />
</div>

