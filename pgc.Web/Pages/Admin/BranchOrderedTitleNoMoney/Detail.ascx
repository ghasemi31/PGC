<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchOrderedTitleNoMoney_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>        
        <td class="caption">عنوان کالا</td>
        <td class="control"><%=orderTitle.Title %></td>
    </tr>
    <%--<tr>        
        <td class="caption">قیمت واحد</td>
        <td class="control"><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(orderTitle.Price)%> ریال</td>
    </tr>--%>
</table>
<div runat="server" id="detailList" class="detailtbl">            
    <span class="detailistttl">کالاهای سفارشی</span>
</div>
<div class="commands">
    <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchOrderedTitleNoMoneyDetailExcel.aspx?id="+orderTitle.ID.ToString())%>" target="_blank" class="excelbtn ocommands" >دریافت فایل اکسل نتایج</a>
    
    <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchOrderedTitleNoMoneyDetail.aspx?id="+orderTitle.ID.ToString() )%>" target="_blank" class="printbtn ocommands" >چاپ جزئیات</a>
    
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" Visible="false" />
    
    <asp:Button runat="server" ID="btnCancel" Text="بازگشت" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

