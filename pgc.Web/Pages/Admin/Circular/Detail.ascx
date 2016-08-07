<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_Circular_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>

    <tr>
        <td class="caption">عنوان</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtTitle" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">متن بخشنامه</td>
        <td class="control">
            <kfk:HtmlEditor ID="txtBody" runat="server" Required="true" />
        </td>
    </tr>

    <tr>
        <td class="caption">قابل نمایش</td>
        <td class="control">
            <asp:CheckBox runat="server" ID="chkIsVisible" Text="" CssClass="checkbox" />
        </td>
    </tr>
    <tr>
        <td class="caption">قابل نمایش برای کاربران</td>
        <td class="control">
            <asp:CheckBox runat="server" ID="chkIsActiveForUser" Text="" CssClass="checkbox" />
        </td>
    </tr>
    <tr>
        <td class="caption">قابل نمایش برای تمام شعب</td>
        <td class="control">
            <asp:CheckBox runat="server" ID="chkAllBranches" ClientIDMode="Static" Text="" CssClass="checkbox" />
        </td>
    </tr>
     <tr class="branch">
        <td class="caption">شعب</td>
        <td class="control" style="width:80%">
            <asp:CheckBoxList ID="chlBranch" RepeatColumns="4" runat="server"></asp:CheckBoxList></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

