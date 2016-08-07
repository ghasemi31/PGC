<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchSold_Detail" %>
<%@ Import Namespace="kFrameWork.Util" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="pgc.Model" %>
</fieldset>

<fieldset class="flddetail">
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
    <% BranchSoldPattern pattern = _Page.SearchControl.Pattern; %>
    
    <table class="Table">                
        <tr>
            <td class="caption" style="border:none">شعبه : <%=new BranchBusiness().Retrieve(_Page.SelectedID).Title %></td>

            <%if (pattern.Price.Type!=pgc.Model.RangeType.Nothing){ %>
                <td class="caption"style="border:none">مبلغ : 
                    <%= EnumUtil.GetEnumElementPersianTitle((pgc.Model.RangeType)pattern.Price.Type)%>
                    <%= pattern.Price.HasFirstNumber ? UIUtil.GetCommaSeparatedOf( pattern.Price.FirstNumber ) + " ریال" : ""%>
                    <%= pattern.Price.HasSecondNumber ? " و " + UIUtil.GetCommaSeparatedOf( pattern.Price.SecondNumber ) + " ریال" : ""%>
                </td>
            <%} %>
                    
        </tr>   
        <tr>
            <%if (pattern.SoldPersianDate.SearchMode!=DateRangePattern.SearchType.Nothing){ %>
                <td class="caption"style="border:none">تاریخ : 
                    <%= EnumUtil.GetEnumElementPersianTitle((DateRangePattern.SearchType)pattern.SoldPersianDate.SearchMode)%>
                    <%= pattern.SoldPersianDate.HasDate ? pattern.SoldPersianDate.Date : ""%>
                    <%= pattern.SoldPersianDate.HasFromDate ? pattern.SoldPersianDate.FromDate : ""%>
                    <%= pattern.SoldPersianDate.HasToDate ? " و " + pattern.SoldPersianDate.ToDate : ""%>
                </td>
            <%} %>
                                        
            <%if (BasePattern.IsEnumAssigned(pattern.Type)){ %>
                <td class="caption" style="border:none">نوع گزارش : <%=EnumUtil.GetEnumElementPersianTitle(pattern.Type) %></td>
            <%} %>
        </tr>                     
    </table>

    <div runat="server" id="detailList" class="detailtbl"></div>
</fieldset>
<div class="commands">
    <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchSoldDetailExcel.aspx?id="+_Page.SelectedID.ToString())%>" target="_blank" class="excelbtn ocommands" >دریافت فایل اکسل نتایج</a>

    <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchSoldDetail.aspx?id="+_Page.SelectedID.ToString() )%>" target="_blank" class="printbtn ocommands" >چاپ جزئیات</a>

    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" Visible="false" />

    <asp:Button runat="server" ID="btnCancel" Text="بازگشت" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

