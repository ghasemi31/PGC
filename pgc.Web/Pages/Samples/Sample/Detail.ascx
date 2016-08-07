<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_Sample_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">تاریخ شروع</td>
        <td class="control"><kfk:PersianDatePicker ID="dpStartDate" runat="server" Required="true" /></td>

        <td class="caption">تاریخ پایان</td>
        <td class="control"><kfk:PersianDatePicker ID="dpEndDate" runat="server" Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">عنوان جشنواره</td>
        <td class="control"><kfk:FileUploader ID="ImageUrl1" runat="server" SaveFolder="~/userfiles/test/" Required="true"/></td>

        <td class="caption">توضیحات</td>
        <td class="control"><kfk:FileUploader ID="ImageUrl2" runat="server" SaveFolder="~/userfiles/test/" /></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

