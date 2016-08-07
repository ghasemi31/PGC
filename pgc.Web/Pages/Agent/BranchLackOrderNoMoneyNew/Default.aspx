<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Agent_BranchLackOrderNoMoneyNew_Default" %>
<%@ Import Namespace="kFrameWork.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>
<%@ Import Namespace="pgc.Model.Enums" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
<fieldset>
    <legend>درج کسری درخواست امروز</legend>
    <table cellspacing="10">
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td class="desc" colspan="4"><%=OptionBusiness.GetText(OptionKey.BranchLackOrderNewDesc) %></td>
        </tr>
        <tr><td>&nbsp;</td></tr>        
        <tr id="closeDesc" runat="server" visible="false">
            <td class="desc" colspan="4"><%=OptionBusiness.GetText(OptionKey.BranchLackOrderNewClosed) %></td>
        </tr>        
        <tr style="display:<%=closeDesc.Visible?"":"none" %>" ><td>&nbsp;</td></tr> 
        <tr id="notPendingDesc" runat="server" visible="false">            
            <td colspan="4" class="caption desc">وضعیت کسری : <%=EnumUtil.GetEnumElementPersianTitle((BranchLackOrderStatus)branchLackOrder.Status)%></td>
        </tr>        
        <%--<tr style="display:<%=notPendingDesc.Visible?"":"none" %>" ><td>&nbsp;</td></tr>--%> 
        
        <tr>
            <td class="caption">کد کسری</td>
            <td class="control"><%=(branchLackOrder != null && branchLackOrder.ID > 0) ? branchLackOrder.ID.ToString() : "----"%></td>
        </tr>
        <tr>
            <td class="caption">کد درخواست مربوطه</td>
            <td class="control"><%=branchOrder.ID %></td>
        </tr>
        <tr>
            <td class="caption">تاریخ کسری</td>
            <td class="control"><%= (branchLackOrder.ID>0)?branchLackOrder.OrderedPersianDate : DateUtil.GetPersianDateShortString(DateTime.Now)%></td>        
        </tr>
        <tr>
            <td class="caption">توضیحات</td>
            <td class="control"><kfk:NormalTextBox ID="txtDescription" runat="server" TextMode="MultiLine" /></td>        
        </tr>           
    </table>
    <div runat="server" id="detailList" class="detailtbl">            
        <span class="detailistttl">لیست کسورات کالاها</span>        
    </div>
    <div class="commands">
        <asp:Button ID="btnRevise" Text="بازنگری و ویرایش مجدد" runat="server" Visible="false" CssClass="scommands" onclick="btnRevise_Click" />
        <asp:Button ID="btnAdd" Text="تایید و ثبت نهایی" runat="server" onclick="btnAdd_Click" Visible="false" CssClass="scommands" />
        <asp:Button ID="btnPreview" Text="ثبت کسری" runat="server" CssClass="scommands" onclick="btnPreview_Click" />
        <asp:Button ID="btnCancle" Text="انصراف" runat="server" onclick="btnCancle_Click" />
    </div>
</fieldset>
</asp:Content>

