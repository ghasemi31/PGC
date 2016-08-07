<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Admin_BranchOrderByAdmin_Default" %>
<%@ Import Namespace="kFrameWork.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>
<%@ Import Namespace="pgc.Model.Enums" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
<fieldset>
    <legend>درج درخواست برای شعب</legend>
    <table cellspacing="10">
        <tr>
        <td class="caption">نام شعبه</td>
        <td class="control">
            <kfk:LookupCombo runat="server" ID="lkpGroup" CssClass="branch-name" BusinessTypeName="pgc.Business.Lookup.BranchLookupBusiness" />
        </td>
    </tr>      
             
    </table>
    
    <div class="commands">
        <asp:Button ID="btnPreview" Text="ادامه" runat="server" CssClass="scommands" onclick="btnPreview_Click" Width="100" />
        
    </div>
</fieldset>
</asp:Content>

