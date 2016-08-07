<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchCredit_Detail" %>

<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">شعبه</td>
        <td class="control"><%=new pgc.Business.BranchBusiness().Retrieve(_Page.SelectedID).Title %></td>
    </tr>
    <tr>
        <td class="caption">مبلغ</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtPrice" runat="server" Required="true" Mode="Numeric_Decimal" CssClass="txtPrice" /> ریال
            <br /><span class="desc">این مبلغ مقدار اعتبار لازم برای درج درخواست توسط شعبه می باشد<br />مثال: مبلغ -3000ریال یعنی شعبه می تواند تا سقف 3000ریال به شعبه مرکزی بدهکار باشد و درخواست ثبت نماید</span>
        </td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

