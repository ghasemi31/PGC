<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Pages_Admin_BranchLackOrderNoMoney_Edit" %>
<%@ Import Namespace="kFrameWork.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Business" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="<%= ResolveUrl("~/Scripts/Shared/SetSelectedMenuItem.js") %>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">

<input type="hidden" id="SelectedMenuItem" runat="server" clientidmode="Static" value="/admin/branchlackorder" />

<fieldset>
    <legend>ویرایش کسری</legend>
    <table cellspacing="10">
        <tr>
            <td class="caption">کد کسری</td>
            <td class="control"><%=branchLackOrder.ID %></td>

            <td class="caption">کد درخواست</td>
            <td class="control"><%=branchLackOrder.BranchOrder_ID%></td>
        </tr>
        <tr>
            <td class="caption">تاریخ کسری</td>
            <td class="control"><%=Util.GetPersianDateWithTime(branchLackOrder.RegDate) %></td>                   

            <td class="caption">وضعیت</td>
            <td class="control"><%=EnumUtil.GetEnumElementPersianTitle((BranchLackOrderStatus)branchLackOrder.Status) %></td>
        </tr>        
        <tr>
            <td class="caption">توضیحات شعبه</td>
            <td class="control"><kfk:NormalTextBox ID="txtBranchDescription" runat="server" TextMode="MultiLine" /></td>        

            <td class="caption">توضیحات مدیر</td>
            <td class="control"><kfk:NormalTextBox ID="txtAdminDescription" runat="server" TextMode="MultiLine" /></td>        
        </tr>
    </table>
    <div runat="server" id="detailList" class="detailtbl">            
        <span class="detailistttl">لیست کسورات کالاها</span>
    </div>
    <div class="commands">
        <asp:Button ID="btnRevise" Text="بازنگری و ویرایش مجدد" runat="server" Visible="false" CssClass="scommands" onclick="btnRevise_Click" />
        <asp:Button ID="btnPreview" Text="ثبت تغییرات" runat="server" CssClass="scommands" onclick="btnPreview_Click" />
        <asp:Button ID="btnAdd" Text="ثبت نهایی کسری" runat="server" onclick="btnAdd_Click" Visible="false" CssClass="scommands" />
        <asp:Button ID="btnCancle" Text="انصراف" runat="server" onclick="btnCancle_Click" />
    </div>
</fieldset>
</asp:Content>

