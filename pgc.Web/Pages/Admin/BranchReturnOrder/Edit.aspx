<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Pages_Admin_BranchReturnOrder_Edit" %>
<%@ Import Namespace="kFrameWork.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Business" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="<%= ResolveUrl("~/Scripts/Shared/SetSelectedMenuItem.js") %>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">

<input type="hidden" id="SelectedMenuItem" runat="server" clientidmode="Static" value="/admin/branchreturnorder" />

<fieldset>
    <legend>ویرایش مرجوعی</legend>
    <table cellspacing="10">  
        <tr>
            <td class="caption">کد مرجوعی</td>
            <td class="control"><%=branchReturnOrder.ID%></td>    
        </tr>      
        <tr> 
            <td class="caption">وضعیت مرجوعی</td>
            <td class="control"><%=kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle(pgc.Model.Enums.BranchLackOrderStatus.Pending) %></td>

            <td class="caption">تاریخ</td>
            <td class="control"><%=Util.GetPersianDateWithTime(branchReturnOrder.RegDate) %></td>
        </tr>
        <tr>        
            <td class="caption">توضیحات شعبه</td>
            <td class="control"><kfk:NormalTextBox ID="txtBranchDescription" runat="server" TextMode="MultiLine" /></td>
        
            <td class="caption">توضیحات مدیر</td>
            <td class="control"><kfk:NormalTextBox ID="txtAdminDescription" runat="server" TextMode="MultiLine" /></td>
        </tr>                             
    </table>
    <div runat="server" id="detailList" class="detailtbl">            
        <span class="detailistttl">لیست کالاهای مرجوعی</span>
    </div>
    <div class="commands">
        <asp:Button ID="btnRevise" Text="بازنگری و ویرایش مجدد" runat="server" Visible="false" CssClass="scommands" onclick="btnRevise_Click" />
        <asp:Button ID="btnPreview" Text="ثبت مرجوعی" runat="server" CssClass="scommands" onclick="btnPreview_Click" />
        <asp:Button ID="btnAdd" Text="ثبت و تایید نهایی" runat="server" onclick="btnAdd_Click" Visible="false" CssClass="scommands" />
        <asp:Button ID="btnCancle" Text="انصراف" runat="server" onclick="btnCancle_Click" />
    </div>
</fieldset>
</asp:Content>

