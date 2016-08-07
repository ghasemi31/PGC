<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Agent_BranchReturnOrderNoMoneyNew_Default" %>
<%@ Import Namespace="kFrameWork.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>
<%@ Import Namespace="pgc.Model.Enums" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
<fieldset>
    <legend>درج مرجوعی روز</legend>
    <table cellspacing="10">
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td class="desc" colspan="4"><%=OptionBusiness.GetText(OptionKey.BranchReturnOrderNewDesc) %></td>
        </tr>
        <tr><td>&nbsp;</td></tr> 
        <tr id="closeDesc" runat="server" visible="false">            
            <td class="desc" colspan="4"><%=OptionBusiness.GetText(OptionKey.BranchReturnOrderNewClosed) %></td>
        </tr>        
        <tr style="display:<%=closeDesc.Visible?"":"none" %>" ><td>&nbsp;</td></tr> 
        <tr id="notPendingDesc" runat="server" visible="false">            
            <td colspan="4" class="desc">وضعیت مرجوعی : <%=EnumUtil.GetEnumElementPersianTitle((returnOrder != null) ? (pgc.Model.Enums.BranchLackOrderStatus)returnOrder.Status : (pgc.Model.Enums.BranchLackOrderStatus.Pending))%></td>
        </tr>        
        <tr style="display:<%=notPendingDesc.Visible?"":"none" %>" ><td>&nbsp;</td></tr> 
        <tr>
            <td class="caption">کد مرجوعی</td>
            <td class="control"><%=(returnOrder!=null)?returnOrder.ID.ToString(): "---" %></td>
        </tr>
        <tr>
            <td class="caption">تاریخ</td>
            <td class="control"><%=DateUtil.GetPersianDateShortString(DateTime.Now) %></td>
        </tr>
        <tr>        
            <td class="caption">توضیحات شعبه</td>
            <td class="control"><kfk:NormalTextBox ID="txtDescription" runat="server" TextMode="MultiLine" /></td>
        </tr>                             
    </table>
    <div runat="server" id="detailList" class="detailtbl">            
        <span class="detailistttl">لیست کالاهای مرجوعی</span>
    </div>
    <div class="commands">
        <asp:Button ID="btnRevise" Text="بازنگری و ویرایش مجدد" runat="server" Visible="false" CssClass="scommands" onclick="btnRevise_Click" />
        <asp:Button ID="btnPreview" Text="ثبت مرجوعی" runat="server" CssClass="scommands" onclick="btnPreview_Click" />
        <asp:Button ID="btnAdd" Text="تایید و ثبت نهایی" runat="server" onclick="btnAdd_Click" Visible="false" CssClass="scommands" />
        <asp:Button ID="btnCancle" Text="انصراف" runat="server" onclick="btnCancle_Click" />
    </div>
</fieldset>
</asp:Content>

