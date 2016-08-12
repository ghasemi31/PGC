<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_Feedback_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">نام و نام خانوادگی</td>
        <td class="control"><asp:Label ID="txtName" runat="server" ></asp:Label></td>
        <%--<kfk:NormalTextBox ID="txtName" runat="server" />--%>
    </tr>
    <tr>
        <td class="caption">پست الکترونیک</td>
        <td class="control"><asp:Label ID="txtEmail" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">مدیر بازی</td>
        <td class="control"><asp:Label ID="lblGameManager" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">تاریخ</td>
        <td class="control"><asp:Label ID="txtDate" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">متن</td>
        <td class="control"><asp:Label ID="txtBody" runat="server"></asp:Label></td>
    </tr>
   <%-- <tr>
        <td class="caption">قابل نمایش</td>
        <td class="control"><asp:CheckBox runat="server" ID="chkIsDisplay" Text="" CssClass="checkbox" /></td>
    </tr>--%>


</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave"  />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" />
</div>

